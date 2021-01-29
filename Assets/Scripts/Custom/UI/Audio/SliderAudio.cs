using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Utilities.UI.Audio
{
    public class SliderAudio : UIAudio
    {
        public Slider slider;
        public Utilities.Audio.ClipInfo sound;

        private void Reset()
        {
            slider = gameObject.GetComponent<Slider>();
        }

        private void Awake()
        {
            slider.onValueChanged.AddListener((val) =>
            {
                //    Play(sound);
            });
        }
    }
}