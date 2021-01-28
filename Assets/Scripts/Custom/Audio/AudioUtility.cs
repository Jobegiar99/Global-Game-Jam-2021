using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Utilities.UI;

namespace Utilities.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioUtility : MonoBehaviour
    {
        public AudioUtility parentToSync;
        public bool randomLoop = false;
        public Vector2 timeInterval = Vector2.one;
        private bool waiting = false;

        [HideInInspector] public AudioSource source;
        [HideInInspector] public Sound[] soundsToUse;
        [HideInInspector] public Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();

        public void Reset()
        {
            source = GetComponent<AudioSource>();
        }

        public void Awake()
        {
            if (soundsToUse != null && soundsToUse.Length > 0)
                AddSounds(soundsToUse);
        }

        private int oldSoundID = 0;

        private void Update()
        {
            if (randomLoop && !waiting)
            {
                StartCoroutine(PlayAfterTime(soundsToUse[Helper.GetRandomNoReapiting(ref oldSoundID, 0, soundsToUse.Length - 1)].name, Random.Range(timeInterval.x, timeInterval.y)));
            }
        }

        public void PlayRandomSound()
        {
            if (!parentToSync)
                PlayOnShotSound(soundsToUse[Helper.GetRandomNoReapiting(ref oldSoundID, 0, soundsToUse.Length - 1)].name);
            else
                PlayOnShotSound(parentToSync.soundsToUse[Helper.GetRandomNoReapiting(ref parentToSync.oldSoundID, 0, parentToSync.soundsToUse.Length - 1)].name);
        }

        private IEnumerator PlayAfterTime(string soundName, float t)
        {
            waiting = true;
            yield return new WaitForSeconds(t);
            PlayOnShotSound(soundName);
            waiting = false;
        }

        public void PlayOnShotSound(string name)
        {
            Sound sound = parentToSync ? parentToSync.sounds[name] : sounds[name];
            if (sound != null)
            {
                source.volume = sound.volume;
                source.pitch = sound.pitch;
                source.PlayOneShot(sound.audio, sound.volume);
            }
        }

        public void AddSounds(Sound[] sounds)
        {
            foreach (var item in sounds)
            {
                this.sounds.Add(item.name, item);
            }
        }
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip audio;
        [Range(0, 1)] public float volume = 1f;
        [Range(-3, 3)] public float pitch = 1f;
        public bool isFolded;//Para Inspector GUI
    }
}