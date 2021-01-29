using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Animation
{
    public class UIZoomAnimation : UIAnimationTweener
    {
        public override LTDescr Tweener
        {
            get
            {
                Vector3 target = Showing ? Info.max : Info.min;
                return LeanTween.scale(Info.rt, target, InfoBase.duration);
            }
        }

        public UIZoomAnimationInfo Info;
        public override UIAnimationInfo InfoBase => Info;

        public override void Init()
        {
            base.Init();
            Info.rt.transform.localScale = Showing ? Info.max : Info.min;
        }
    }
}