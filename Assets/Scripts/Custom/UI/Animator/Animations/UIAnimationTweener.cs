using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Animation
{
    public abstract class UIAnimationTweener : UIAnimation
    {
        protected int TweenerID { get; set; } = -1;

        public override void Animate(bool show)
        {
            base.Animate(show);
            if (TweenerID != -1) { LeanTween.cancel(TweenerID); TweenerID = -1; }

            var CurrentTweener = Tweener
                .setDelay(InfoBase.delay)
                .setOnComplete(OnAnimationFinish);

            if (InfoBase.easeType == LeanTweenType.animationCurve) CurrentTweener.setEase(InfoBase.customCurve);
            else CurrentTweener.setEase(InfoBase.easeType);
            TweenerID = CurrentTweener.id;
        }

        public abstract LTDescr Tweener { get; }
    }
}