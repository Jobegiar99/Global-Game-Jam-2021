using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class BarUILerp : BarUI
    {
        [Header("Lerp")]
        public float duration = 10;

        private float lerpValue = 0;
        private float oldValue = 0;

        public float CurrentValue => lerpValue;

        protected override void Awake()
        {
            base.Awake();
            lerpValue = oldValue = value;
        }

        private float timer = 0;

        private void Update()
        {
            timer += Time.deltaTime / duration;
            lerpValue = Mathf.Lerp(oldValue, value, timer);
            if (bar) bar.fillAmount = lerpValue / max;
        }

        public override void SetValue(float value)
        {
            timer = 0;
            oldValue = lerpValue;
            base.SetValue(value);
        }

        public void SetValueWithoutLerp(float value, float max)
        {
            SetValue(value, max);
            timer = duration;
            lerpValue = oldValue = value;
            bar.fillAmount = lerpValue / max;
        }

        protected override void UpdateUI()
        {
        }
    }
}