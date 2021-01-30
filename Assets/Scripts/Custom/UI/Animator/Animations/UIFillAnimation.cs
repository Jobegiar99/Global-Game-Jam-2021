using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI.Animation
{
    public class UIFillAnimation : UIAnimation
    {
        public UIFillAnimationInfo Info;
        public override UIAnimationInfo InfoBase => Info;

        private float target;
        private float current = 0;
        private float startTime;

        public override void Init()
        {
            base.Init();
            Info.img.fillAmount = Info.initialValue ? Info.max : Info.min;
            current = Info.img.fillAmount;
        }

        private void Update()
        {
            if (!Animating) return;

            float percentage = Info.customCurve.Evaluate((Time.time - startTime) / Info.duration);
            if (percentage >= 1) { OnAnimationFinish(); return; }

            Info.img.fillAmount = current + percentage * (target - current);
        }

        protected override void OnAnimationFinish()
        {
            Info.img.fillAmount = (target);
            base.OnAnimationFinish();
        }

        public override void Animate(bool show)
        {
            base.Animate(show);

            current = Info.img.fillAmount;
            target = Showing ? Info.max : Info.min;
            if (current == target) { OnAnimationFinish(); return; }
            startTime = Time.time;
        }
    }
}