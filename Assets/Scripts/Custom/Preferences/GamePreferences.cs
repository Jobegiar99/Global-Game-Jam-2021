using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Preferences
{
    public class GamePreferences : MonoBehaviour
    {
    }

    [System.Serializable]
    public class IntPreferences : Preferences
    {
        public int defaultValue;

        public int Value { get { return (int)_Value; } set { _Value = value; } }

        public override object _Value
        {
            get => PlayerPrefs.GetInt(name, (int)_DefaultValue);
            set { PlayerPrefs.SetInt(name, (int)value); onChangeValue?.Invoke(value); }
        }

        public override object _DefaultValue => defaultValue;
    }

    [System.Serializable]
    public class StringPreferences : Preferences
    {
        public string defaultValue;

        public string Value { get { return (string)_Value; } set { _Value = value; } }

        public override object _Value
        {
            get => PlayerPrefs.GetString(name, (string)_DefaultValue);
            set { PlayerPrefs.SetString(name, (string)value); onChangeValue?.Invoke(value); }
        }

        public override object _DefaultValue => defaultValue;
    }

    [System.Serializable]
    public class FloatPreferences : Preferences
    {
        public float defaultValue;

        public float Value { get { return (float)_Value; } set { _Value = value; } }

        public override object _Value
        {
            get => PlayerPrefs.GetFloat(name, (float)_DefaultValue);
            set { PlayerPrefs.SetFloat(name, (float)value); onChangeValue?.Invoke(value); }
        }

        public override object _DefaultValue => defaultValue;
    }

    public abstract class Preferences
    {
        public string name;
        public abstract object _DefaultValue { get; }
        public abstract object _Value { get; set; }
        public Events.Event onInit;
        public Events.ObjectEvent onChangeValue;

        public virtual void Init()
        {
            onInit?.Invoke();
        }

        public virtual void ApplyValue(object value)
        {
        }
    }
}