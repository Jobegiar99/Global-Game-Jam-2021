using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Utilities.Preferences
{
    public class PreferencesManager : MonoBehaviour
    {
        public PreferencesVars values;

        public static PreferencesVars Values { get => Instance.values; }
        public static PreferencesManager Instance = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        public void Start()
        {
            this.ExecuteNextFrame(LoadPreferences);//LoadPreferences();
        }

        /*
                private void OnLevelWasLoaded(int level)
                {
                    values.masterVolume.ApplyValue(values.masterVolume.Value);
                }
        */

        public void LoadPreferences()
        {
            /*
            values.language.ApplyValue(values.language.Value);
            values.masterVolume.ApplyValue(values.masterVolume.Value);
            values.musicVolume.ApplyValue(values.musicVolume.Value);
            values.ambienceVolume.ApplyValue(values.ambienceVolume.Value);
            values.sfxVolume.ApplyValue(values.sfxVolume.Value);
            values.uiVolume.ApplyValue(values.uiVolume.Value);
            values.resolution.ApplyValue(values.resolution.Value);
            values.quality.ApplyValue(values.quality.Value);*/
            values.framerate.ApplyValue(values.framerate.Value);
        }
    }
}