using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Animation
{
    public class UIPositionAnimation : UIAnimation
    {
        public UIPositionAnimationInfo Info;
        public override UIAnimationInfo InfoBase => Info;

        private Vector2 target;
        private Vector2 current;
        private float startTime;

        public override void Init()
        {
            base.Init();

            Info.rectTransform.anchoredPosition = Info.initialValue ? Info.finalPosition : Info.startPosition;
        }

        private void Update()
        {
            if (!Animating) return;

            float percentage = Info.customCurve.Evaluate((Time.time - startTime) / Info.duration);
            if (percentage >= 1) { OnAnimationFinish(); return; }
            Info.rectTransform.anchoredPosition = current + percentage * (target - current);
        }

        public override void Animate(bool show)
        {
            base.Animate(show);

            current = Info.rectTransform.anchoredPosition;
            target = Showing ? Info.finalPosition : Info.startPosition;
            if (current == target) { OnAnimationFinish(); return; }
            startTime = Time.time;
        }

        protected override void OnAnimationFinish()
        {
            Info.rectTransform.anchoredPosition = target;
            base.OnAnimationFinish();
        }
    }
}