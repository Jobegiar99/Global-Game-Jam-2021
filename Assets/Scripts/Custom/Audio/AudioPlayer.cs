using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        public ClipInfo clip;
        public bool playOnAwake = false;
        public AudioMngr.Type type;
        public AudioSourceInfo CurrentSource { get; protected set; }

        private void Awake()
        {
            if (playOnAwake) Play();
        }

        public virtual void Play()
        {
            CurrentSource = AudioMngr.Player(type).Play(clip);
        }

        public void Stop()
        {
            if (CurrentSource == null) return;
            AudioMngr.Player(type).FreeSource(CurrentSource, CurrentSource.playId);
            CurrentSource = null;
        }

        private void OnDestroy()
        {
            Stop();
        }
    }
}