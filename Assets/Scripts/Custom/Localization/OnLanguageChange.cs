using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;
using UnityEngine.Events;

namespace Utilities._Localization
{
    public class OnLanguageChange : MonoBehaviour
    {
        public UnityEvent onChange;

        protected virtual void Awake()
        {
            LeanLocalization.OnLocalizationChanged += () => { onChange?.Invoke(); };
        }
    }
}