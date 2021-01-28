using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using Utilities._Input;

namespace Utilities.UI.Video
{
    [System.Serializable]
    public class VideoInfo
    {
        public string titleId;
        public VideoClip video;
        public Sprite thumbnail;
        public float volume = 0.25f;
    }

    [RequireComponent(typeof(VideoPlayer))]
    public class VideoPlayerUI : MonoBehaviour, IFocus
    {
        [SerializeField] private RawImage _videoImage;
        [SerializeField] private VideoPlayer _videoPlayer;
        public VideoPlayer VideoPlayer => _videoPlayer;
        public RawImage VideoImage => _videoImage;
        public VideoClip CurrentClip { get; private set; } = null;

        public bool deselectPreviousElement = true;
        public string FocusGroup => "video_player";

        public UnityEvent OnPlay;
        public UnityEvent OnStop;
        public UnityEvent OnPause;
        public Slider slider;

        public bool autoStop = false;
        public bool disableRawImage = true;
        private float previousVolume;
        private string previousFocus;
        private bool isPlaying;

        public void Reset()
        {
            _videoPlayer = gameObject.GetAddComponent<VideoPlayer>();
        }

        private void Awake()
        {
            VideoPlayer.loopPointReached += (source) =>
            {
                if (autoStop && !source.isLooping)
                {
                    Stop();
                }
            };

            slider.onValueChanged.AddListener((value) =>
            {
                if (isPlaying)
                {
                    VideoPlayer.frame = (long)(VideoPlayer.frameCount * value);
                };
            });
        }

        public void Play(VideoClip clip, float volume = 1f)
        {
            if (CurrentClip == null)
            {
                Utilities.Audio.AudioMngr.Instance.mixer.GetFloat("Vol_Master", out previousVolume);
                previousFocus = InputManager.Instance.CurrentFocus;

                if (deselectPreviousElement)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }

            CurrentClip = clip;
            VideoPlayer.clip = clip;
            VideoPlayer.Play();
            VideoPlayer.SetDirectAudioVolume(0, Preferences.PreferencesManager.Instance.values.masterVolume.Value * volume);

            Utilities.Audio.AudioMngr.Instance.mixer.SetFloat("Vol_Master", -80.00f);

            if (disableRawImage) VideoImage.enabled = true;
            OnPlay.Invoke();
            this.Select();
            isPlaying = true;
        }

        public void Pause()
        {
            VideoPlayer.Pause();
            OnPause.Invoke();
        }

        public void Stop()
        {
            CurrentClip = null;
            VideoPlayer.clip = null;
            VideoPlayer.Stop();
            OnStop.Invoke();
            if (disableRawImage) VideoImage.enabled = false;
            Utilities.Audio.AudioMngr.Instance.mixer.SetFloat("Vol_Master", previousVolume);
            InputManager.Select(previousFocus);
            isPlaying = false;
        }

        public VideoClip clip;

        public void Update()
        {
            if (isPlaying && VideoPlayer.frame == (long)VideoPlayer.frameCount - 2)
            {
                if (autoStop) { Stop(); }
            }

            slider.SetValueWithoutNotify(VideoPlayer.frame / (float)VideoPlayer.frameCount);

            if (!this.HasFocus()) return;

            if (Input.GetKeyDown(KeyCode.Escape))
                this.ExecuteNextFrame(Stop);

            if (CurrentClip == null) return;

            if (Input.GetKeyDown(KeyCode.Space))
                if (VideoPlayer.isPlaying) { VideoPlayer.Pause(); }
                else { VideoPlayer.Play(); }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) { AddTime(-5f); }
            if (Input.GetKeyDown(KeyCode.RightArrow)) { AddTime(5f); }
        }

        public void AddTime(float extra)
        {
            double time = VideoPlayer.time;
            time += extra;
            if (time <= 0f) time = 0f;
            if (time >= VideoPlayer.length) time = VideoPlayer.length;
            VideoPlayer.time = time;
        }

        private void OnDestroy()
        {
            Stop();
        }

        private void OnApplicationQuit()
        {
            Stop();
        }

        public void OnSelect()
        {
        }

        public void OnDeselect()
        {
        }
    }
}