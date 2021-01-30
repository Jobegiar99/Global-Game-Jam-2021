using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Utilities.UI.Animation
{
    [System.Serializable]
    public abstract class UIAnimationInfo
    {
        public float duration = 0.25f;
        public float delay = 0f;
        public bool initialValue = false;
        public Events.Event OnAnimationFinish { get; set; }
        public LeanTweenType easeType;
        public AnimationCurve customCurve;

        public float TotalTime => duration + delay;
    }
}