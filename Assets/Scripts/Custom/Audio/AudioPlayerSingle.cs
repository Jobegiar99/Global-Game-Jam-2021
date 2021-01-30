using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class AudioPlayerSingle : AudioPlayer
    {
        public override void Play()
        {
            if (CurrentSource == null || !CurrentSource.source.isPlaying)
            {
                base.Play();
                AudioMngr.Player(type).AddStopEvent(CurrentSource.source, 0.01f, () => { CurrentSource = null; });
            }
        }
    }
}