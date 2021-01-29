using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Preferences
{
    public class PreferencesSettings : MonoBehaviour
    {
    }

    [System.Serializable]
    public class VolumeSetting : FloatPreferences
    {
        public UnityEngine.Audio.AudioMixer mixer;
        public string mixerParameter;

        public override void ApplyValue(object value)
        {
            base.ApplyValue(value);
            float val = (float)value;

            mixer.SetFloat(mixerParameter, Audio.AudioMngr.FloatToDB(val));
        }
    }

    [System.Serializable]
    public class ScreenResolutionSetting : IntPreferences
    {
        [System.NonSerialized] private Resolution[] _resolutions = null;

        public Resolution[] Resolutions
        {
            get
            {
                if (_resolutions == null)
                {
                    var tmp = new List<Resolution>();
                    tmp.AddRange(Screen.resolutions);
                    tmp.Reverse();
                    _resolutions = tmp.ToArray();
                }
                return _resolutions;
            }
        }

        public override void ApplyValue(object value)
        {
            base.ApplyValue(value);
            int val = (int)value;
            if (val >= Resolutions.Length) val = defaultValue;
            var r = Resolutions[val];
            Screen.SetResolution(r.width, r.height, true);
        }
    }

    [System.Serializable]
    public class LimitFramerateSetting : IntPreferences
    {
        public override object _DefaultValue => 0;

        public override void ApplyValue(object value)
        {
            base.ApplyValue(value);
            int val = (int)value;
            var framerate = -1;
            if (val == 0) { framerate = 240; }
            else if (val == 1) { framerate = 120; }
            else if (val == 2) { framerate = 60; }
            else if (val == 3) { framerate = 30; }
            else if (val == 4) { framerate = -1; }
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = framerate;
        }
    }

    [System.Serializable]
    public class GraphicsSetting : IntPreferences
    {
        public override object _DefaultValue
        {
            get
            {
                return QualitySettings.names.Length - 1;
            }
        }

        public override void ApplyValue(object value)
        {
            base.ApplyValue(value);
            int val = (int)value;

            QualitySettings.SetQualityLevel(val);
        }
    }

    [System.Serializable]
    public class LanguageSettings : StringPreferences
    {
        [System.NonSerialized] private string[] _languages = null;

        public override object _DefaultValue
        {
            get
            {
                if (string.IsNullOrEmpty(defaultValue)) { defaultValue = Lean.Localization.LeanLocalization.CurrentLanguage; }
                return defaultValue;
            }
        }

        public string[] Languages
        {
            get
            {
                if (_languages == null)
                {
                    List<string> langs = new List<string>();
                    Lean.Localization.LeanLocalization.Instances[0].Languages.ForEach(lang =>
                    {
                        langs.Add(lang.Name);
                    });
                    _languages = langs.ToArray();
                }
                return _languages;
            }
        }

        public override void ApplyValue(object value)
        {
            base.ApplyValue(value);
            string val = (string)value;
            if (string.IsNullOrEmpty(val)) { Value = Lean.Localization.LeanLocalization.CurrentLanguage; }
            Lean.Localization.LeanLocalization.CurrentLanguage = val;
        }
    }
}