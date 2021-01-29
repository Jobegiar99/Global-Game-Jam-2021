using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Preferences;
using UnityEngine.UI;

namespace Utilities.Preferences.Settings
{
    public class GeneralSettings : MonoBehaviour
    {
        [System.Serializable]
        public class LanguageDropdown
        {
            private LanguageSettings preferences;
            public DropdownPreferencesSetterString dropdown;

            public void Init(LanguageSettings preferences)
            {
                this.preferences = preferences;

                dropdown.localization.Options.Clear();
                List<string> options = new List<string>();
                foreach (var item in preferences.Languages)
                {
                    options.Add($"{item}");
                }
                dropdown.localization.AddOptions(options);
                dropdown.Init(preferences);
                preferences.Init();
                preferences.onChangeValue += val =>
                {
                    preferences.ApplyValue(val);
                };
            }
        }

        public LanguageDropdown language;

        private void Start()
        {
            language.Init(PreferencesManager.Values.language);
        }
    }
}