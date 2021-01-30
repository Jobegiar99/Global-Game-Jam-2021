using UnityEngine;
using Utilities.Audio;

namespace Utilities.UI.Audio
{
    public abstract class UIAudio : MonoBehaviour
    {
        protected AudioSourceInfo CurrentSource;

        public enum ButtonTypes
        {
            Default, Tab, Custom
        }

        public virtual void Play(ClipInfo clip)
        {
            if (clip == null) return;
            CurrentSource = AudioMngr.Player(AudioMngr.Type.UI).Play(clip);
        }
    }
}