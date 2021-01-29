using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Preferences;
using UnityEngine.UI;

namespace Utilities.Preferences.Settings
{
    public class VideoSettings : MonoBehaviour
    {
        [System.Serializable]
        public class ResolutionDropdown
        {
            private ScreenResolutionSetting preferences;
            public DropdownPreferencesSetterInt dropdown;

            public void Init(ScreenResolutionSetting preferences)
            {
                this.preferences = preferences;
                dropdown.dropdown.ClearOptions();
                List<string> options = new List<string>();
                foreach (var item in preferences.Resolutions)
                {
                    var aspect = Math.CustomMath.GetAspecRatio(item.width, item.height);
                    string aspectRatio = $"{aspect.Item1}:{aspect.Item2}";
                    if (aspect.Item1 != 0 && aspect.Item2 != 0)
                        options.Add($"{item.width} x {item.height} ({aspectRatio}) , {item.refreshRate}Hz");
                    else
                        options.Add($"{item.width} x {item.height} , {item.refreshRate}Hz");
                }

                dropdown.dropdown.AddOptions(options);
                dropdown.Init(preferences);
                preferences.Init();
                preferences.onChangeValue += val => preferences.ApplyValue(val);
            }
        }

        [System.Serializable]
        public class LimitFramerate
        {
            private LimitFramerateSetting preferences;
            public DropdownPreferencesSetterInt dropdown;

            public void Init(LimitFramerateSetting preferences)
            {
                this.preferences = preferences;
                dropdown.localization.ClearOptions();
                List<string> options = new List<string>();
                options.Add("settings.video.framerate.240");
                options.Add("settings.video.framerate.120");
                options.Add("settings.video.framerate.60");
                options.Add("settings.video.framerate.30");
                options.Add("settings.video.framerate.unlimited");
                dropdown.localization.AddOptions(options);
                dropdown.Init(preferences);
                preferences.Init();
                preferences.onChangeValue += val => preferences.ApplyValue(val);
            }
        }

        [System.Serializable]
        public class Quality
        {
            private GraphicsSetting preferences;
            public DropdownPreferencesSetterInt dropdown;

            public void Init(GraphicsSetting preferences)
            {
                this.preferences = preferences;
                dropdown.localization.ClearOptions();
                List<string> options = new List<string>();

                foreach (var item in QualitySettings.names)
                {
                    options.Add($"settings.video.quality.{item}");
                }

                dropdown.localization.AddOptions(options);
                dropdown.Init(preferences);
                preferences.Init();
                preferences.onChangeValue += val => preferences.ApplyValue(val);
            }
        }

        public ResolutionDropdown resolution;
        public LimitFramerate framerate;
        public Quality quality;

        private void Start()
        {
            resolution.Init(PreferencesManager.Values.resolution);
            quality.Init(PreferencesManager.Values.quality);
            framerate.Init(PreferencesManager.Values.framerate);
        }
    }
}