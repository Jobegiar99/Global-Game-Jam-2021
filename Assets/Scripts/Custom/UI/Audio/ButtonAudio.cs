using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Utilities.Audio;

namespace Utilities.UI.Audio
{
    public class ButtonAudio : UIAudio
    {
        public Button button;
        public ButtonTypes type;

        [Header("Only if type=custom")]
        public ClipInfo customAudioClick;

        public ClipInfo customAudioSelect;

        private void Reset()
        {
            button = GetComponent<Button>();
        }

        private void Awake()
        {
            if (!button) Reset();
            button.onClick.AddListener(() =>
            {
                PlayClick();
            });

            if (button is ButtonEvents)
            {
                var btn = button as ButtonEvents;
                btn.onSelect.AddListener(() =>
                {
                    PlaySelect();
                });
            }
        }

        public void PlaySelect()
        {
            switch (type)
            {
                case ButtonTypes.Default:
                    Play(UIAudioVars.Instance.buttonDefaultSelect);
                    break;

                case ButtonTypes.Tab:
                    break;

                case ButtonTypes.Custom:
                    if (customAudioSelect.clip != null) Play(customAudioSelect);
                    break;

                default:
                    break;
            }
        }

        public void PlayClick()
        {
            switch (type)
            {
                case ButtonTypes.Default:
                    Play(UIAudioVars.Instance.buttonDefaultClick);
                    break;

                case ButtonTypes.Tab:
                    Play(UIAudioVars.Instance.buttonTabClick);
                    break;

                case ButtonTypes.Custom:
                    Play(customAudioClick);
                    break;

                default:
                    break;
            }
        }
    }
}