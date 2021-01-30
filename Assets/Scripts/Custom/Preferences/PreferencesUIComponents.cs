using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Preferences
{
    [System.Serializable]
    public class SliderPreferenceSetter
    {
        public FloatPreferences preferences;
        public Slider slider;

        public void Init(FloatPreferences preferences)
        {
            this.preferences = preferences;
            slider.value = preferences.Value;
            slider.onValueChanged.AddListener(val =>
            {
                preferences.Value = val;
            });
        }
    }

    [System.Serializable]
    public class DropdownPreferencesSetterInt
    {
        public IntPreferences preferences;
        public TMPro.TMP_Dropdown dropdown;
        public Lean.Localization.LeanLocalizedDropdownTMP localization;

        public void Init(IntPreferences preferences)
        {
            this.preferences = preferences;
            dropdown.value = preferences.Value;
            dropdown.onValueChanged.AddListener(val =>
            {
                preferences.Value = val;
            });
        }
    }

    [System.Serializable]
    public class DropdownPreferencesSetterString
    {
        public StringPreferences preferences;
        public TMPro.TMP_Dropdown dropdown;
        public Lean.Localization.LeanLocalizedDropdownTMP localization;
        public SaveType save;

        public enum SaveType
        {
            Id,
            LocalizedValue
        }

        public void Init(StringPreferences preferences)
        {
            this.preferences = preferences;

            switch (save)
            {
                case SaveType.Id:
                    dropdown.value = localization.Options.FindIndex(el => el.StringTranslationName == preferences.Value);
                    break;

                case SaveType.LocalizedValue:
                    dropdown.value = dropdown.options.FindIndex(el => el.text == preferences.Value);
                    break;
            }

            dropdown.onValueChanged.AddListener(val =>
            {
                switch (save)
                {
                    case SaveType.Id:
                        preferences.Value = localization.Options[val].StringTranslationName;
                        break;

                    case SaveType.LocalizedValue:
                        preferences.Value = dropdown.options[val].text;
                        break;
                }
            });
        }
    }
}