using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Animation
{
    public class UIColorAnimation : UIAnimation
    {
        public UIColorAnimationInfo Info;
        public override UIAnimationInfo InfoBase => Info;
        private Color target;
        private Color current;
        private float startTime;

        public override void Init()
        {
            base.Init();
            Info.graphic.color = Info.initialValue ? Info.to : Info.from;
        }

        private void Update()
        {
            if (!Animating) return;

            float percentage = Info.customCurve.Evaluate((Time.time - startTime) / Info.duration);
            if (percentage >= 1) { OnAnimationFinish(); return; }
            Info.graphic.color = Color.Lerp(current, target, percentage);
        }

        protected override void OnAnimationFinish()
        {
            Info.graphic.color = target;
            base.OnAnimationFinish();
        }

        public override void Animate(bool show)
        {
            base.Animate(show);

            current = Info.graphic.color;
            target = Showing ? Info.to : Info.from;
            if (current == target) { OnAnimationFinish(); return; }
            startTime = Time.time;
        }
    }
}