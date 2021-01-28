using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Animation
{
    public class UISpacingAnimation : UIAnimation
    {
        public UISpacingAnimationInfo Info;
        public override UIAnimationInfo InfoBase => Info;

        private float target;
        private float currentSpacing = 0;
        private float startTime;

        public override void Init()
        {
            base.Init();
            Info.lg.spacing = Info.initialValue ? Info.max : Info.min;
            currentSpacing = Info.lg.spacing;
            Info.layouts.ForEach(el =>
            {
                UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(el);
            });
        }

        private void Update()
        {
            if (!Animating) return;

            float percentage = Info.customCurve.Evaluate((Time.time - startTime) / Info.duration);
            if (percentage >= 1) { OnAnimationFinish(); return; }

            Info.lg.spacing = currentSpacing + percentage * (target - currentSpacing);
            Info.layouts.ForEach(el =>
            {
                UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(el);
            });
        }

        protected override void OnAnimationFinish()
        {
            Info.lg.spacing = (target);
            base.OnAnimationFinish();
            //Info.OnAnimationFinish?.Invoke();
            Info.layouts.ForEach(el =>
            {
                UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(el);
            });
        }

        public override void Animate(bool show)
        {
            base.Animate(show);

            currentSpacing = Info.lg.spacing;
            target = Showing ? Info.max : Info.min;
            if (currentSpacing == target) { OnAnimationFinish(); return; }
            startTime = Time.time;
        }
    }
}