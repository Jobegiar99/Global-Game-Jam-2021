using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utilities.Audio;
using UnityEngine.Events;

namespace Utilities.Audio
{
    public class AudioMngr : MonoBehaviour
    {
        public AudioMixer mixer;
        public static AudioMngr Instance { get; private set; }
        [SerializeField] private List<Audio> sources = new List<Audio>();
        public Dictionary<Type, Audio> Dict { get; } = new Dictionary<Type, Audio>();

        public static Audio Player(Type type)
        {
            return Instance.Dict[type];
        }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                sources.ForEach(el =>
                {
                    el.CreateAudioSource(transform);
                    el.Init(this);
                    Dict.Add(el.type, el);
                });
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [System.Serializable]
        public class Audio
        {
            [Header("Info")]
            public string name;

            public Type type;
            public int poolSize = 10;

            [Header("Required")]
            public AudioMixerGroup group;

            private Dictionary<AudioSource, AudioSourceInfo> AllSources = new Dictionary<AudioSource, AudioSourceInfo>();
            private List<AudioSource> inUse = new List<AudioSource>();
            private Queue<AudioSourceInfo> available = new Queue<AudioSourceInfo>();

            [SerializeField, HideInInspector] private List<AudioSource> sources = new List<AudioSource>();
            private AudioMngr mngr;

            public Dictionary<AudioSource, FadeInfo> fades = new Dictionary<AudioSource, FadeInfo>();//to , fadeinfo

            public struct FadeInfo
            {
                public string id;
                public AudioSource from;
                public AudioSource to;
                public IEnumerator coroutine;
            }

            public void Init(AudioMngr mngr)
            {
                this.mngr = mngr;
                sources.ForEach(el =>
                {
                    available.Enqueue(new AudioSourceInfo()
                    {
                        id = System.Guid.NewGuid().ToString(),
                        playId = null,
                        source = el
                    });

                    AllSources.Add(el, available.Peek());
                }

                );
            }

            public void CreateAudioSource(Transform parent)
            {
                var tmp = new List<AudioSource>();
                tmp.AddRange(sources);
                sources.Clear();
                tmp.ForEach(el => Destroy(el.gameObject));

                for (int i = 0; i < poolSize; i++)
                {
                    var go = Utilities.Helper.CreateChild(parent, "_source_" + name + "_" + i.ToString("00"));
                    var source = go.AddComponent<AudioSource>();
                    source.outputAudioMixerGroup = group;
                    sources.Add(source);
                }
            }

            public List<AudioSourceInfo> GetFreeSources(int quantity)
            {
                var ret = new List<AudioSourceInfo>();
                for (int i = 0; i < quantity; i++)
                {
                    if (available.Count == 0) break;
                    ret.Add(available.Dequeue());
                }

                return ret;
            }

            public void PlayOneShot(AudioClip clip, float volumeScale)
            {
                sources[0].PlayOneShot(clip, volumeScale);
            }

            public AudioSourceInfo Play(ClipInfo info)
            {
                var result = GetFreeSources(1);
                //Si no hay liberar las que esten en uso para utilizarla
                if (result.Count == 0)
                {
                    //Liberar necesarias aunque esten en uso (sonidos viejos)
                    for (int i = 0; i < 1; i++)
                    {
                        if (i >= inUse.Count) break;
                        var s = AllSources[inUse[i]];
                        FreeSource(s, s.playId);
                    }
                    result = GetFreeSources(1);
                }
                //Segunda comprobacion
                if (result.Count == 0) return null;

                var sourceInfo = result[0];
                var playId = System.Guid.NewGuid().ToString();
                sourceInfo.playId = playId;
                var source = sourceInfo.source;
                source.volume = info.volume;
                source.pitch = info.pitch;
                source.clip = info.clip;
                source.loop = info.loop;

                if (!info.loop)
                {
                    AddStopEvent(source, info.delay, () => { FreeSource(sourceInfo, playId); });
                    inUse.Add(source);
                }

                if (info.delay == 0) source.Play(); else source.PlayDelayed(info.delay);
                return sourceInfo;
            }

            public AudioSourceInfo GetSource(AudioSource source)
            {
                if (!AllSources.ContainsKey(source)) return null;
                return AllSources[source];
            }

            public AudioSource Blend(AudioSource from, ClipInfo to, float duration = 1f, bool keepOldTime = false)
            {
                AudioSource destination = Play(to)?.source;
                if (keepOldTime) destination.time = Helper.FullCircular(from.time, to.clip.length);
                destination.volume = 0;

                var coroutine = CrossFadeTo(from, to, destination, duration);
                var blendId = System.Guid.NewGuid().ToString();
                fades.Add(destination, new FadeInfo()
                {
                    id = blendId,
                    from = from,
                    to = destination,
                    coroutine = coroutine
                });

                if (fades.ContainsKey(from))
                {
                    var blend = fades[from];
                    Instance.StopCoroutine(blend.coroutine);
                    Instance.StartCoroutine(FadeOut(blend.from, duration));
                    fades.Remove(from);
                }
                Instance.StartCoroutine(coroutine);
                return destination;
            }

            public void FreeSource(AudioSourceInfo sourceInfo, string playId)
            {
                if (sourceInfo.playId != playId) { return; }
                var source = sourceInfo.source;
                sourceInfo.playId = null;
                if (source == null) return;
                source.Stop();
                available.Enqueue(sourceInfo);
                inUse.RemoveAll(el => el == source);
            }

            public void AddStopEvent(AudioSource source, float delay, UnityAction action)
            {
                MonoExtensions.ExecuteLater(mngr, action, source.clip.length + 0.1f + delay);
            }

            private IEnumerator CrossFadeTo(AudioSource from, ClipInfo to, AudioSource destination, float duration)
            {
                var fromVolume = from.volume;

                return Fade((percentage) =>
                {
                    destination.volume = Mathf.Sqrt(percentage) * to.volume;
                    from.volume = Mathf.Sqrt(1 - percentage) * fromVolume;
                },
                () =>
                {
                    destination.volume = to.volume;
                    from.volume = 0;
                    var source = AllSources[from];
                    FreeSource(source, source.playId);
                    fades.Remove(destination);
                }
                , duration);
            }

            private IEnumerator FadeOut(AudioSource from, float duration)
            {
                return Fade((percentage) =>
                {
                    from.volume = Mathf.Sqrt(1 - percentage);
                },
                () =>
                {
                    var source = AllSources[from];
                    FreeSource(source, source.playId);
                }
                , duration);
            }

            private IEnumerator Fade(System.Action<float> animate, System.Action onDone, float duration)
            {
                var startTime = Time.time;
                var finalTime = Time.time + duration;

                while (Time.time < finalTime)
                {
                    var percentage = (Time.time - startTime) / duration;
                    animate?.Invoke(percentage);
                    yield return null;
                }
                onDone?.Invoke();
            }
        }

        public enum Type { Music, SFX, UI, Ambience }

        public static float FloatToDB(float val)
        {
            return (Mathf.Log10(val) + 1) * 20;
            //return Mathf.Pow(-((val / -80f) - 1f), 2f);
        }
    }

    [System.Serializable]
    public class ClipInfo
    {
        public AudioClip clip;
        public float volume = 1f;
        public float pitch = 1f;
        public bool loop = false;
        public float delay = 0f;
    }

    [System.Serializable]
    public class AudioSourceInfo
    {
        public string id;
        public bool IsPlaying => playId != null;
        public AudioSource source;
        public string playId;
    }
}