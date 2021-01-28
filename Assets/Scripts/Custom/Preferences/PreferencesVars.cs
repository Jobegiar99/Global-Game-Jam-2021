using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Preferences
{
    [CreateAssetMenu(fileName = "Preferences", menuName = "Utilities/Preferences")]
    public class PreferencesVars : ScriptableObject
    {
        [Header("General")]
        public LanguageSettings language;

        [Header("Audio")] public VolumeSetting masterVolume;
        public VolumeSetting uiVolume;
        public VolumeSetting sfxVolume;
        public VolumeSetting ambienceVolume;
        public VolumeSetting musicVolume;

        [Header("Video")] public ScreenResolutionSetting resolution;
        public LimitFramerateSetting framerate;
        public GraphicsSetting quality;
    }
}