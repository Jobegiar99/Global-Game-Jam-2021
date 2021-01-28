using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.UI;
using UnityEngine.UI;
using Utilities.Audio;
using UnityEngine.Audio;

namespace Utilities.Preferences.Settings
{
    public class AudioSettings : MonoBehaviour
    {
        public AudioVolumeSlider masterVolume;
        public AudioVolumeSlider musicVolume;
        public AudioVolumeSlider sfxVolume;
        public AudioVolumeSlider uiVolume;
        public AudioVolumeSlider ambienceVolume;

        public AudioMixer mixer;

        [System.Serializable]
        public class AudioVolumeSlider
        {
            private VolumeSetting volumeSetting;
            public SliderPreferenceSetter slider;

            public void Init(VolumeSetting volumeSetting)
            {
                if (slider.slider == null) return;

                this.volumeSetting = volumeSetting;
                slider.slider.minValue = 0.0001f;
                slider.slider.maxValue = 1f;
                slider.Init(volumeSetting);
                volumeSetting.Init();
                volumeSetting.onChangeValue += val => volumeSetting.ApplyValue(val);
            }
        }

        private void Start()
        {
            masterVolume.Init(PreferencesManager.Values.masterVolume);
            musicVolume.Init(PreferencesManager.Values.musicVolume);
            sfxVolume.Init(PreferencesManager.Values.sfxVolume);
            uiVolume.Init(PreferencesManager.Values.uiVolume);
            ambienceVolume.Init(PreferencesManager.Values.ambienceVolume);
        }
    }
}