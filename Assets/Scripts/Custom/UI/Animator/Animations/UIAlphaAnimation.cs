using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Animation
{
    public class UIAlphaAnimation : UIAnimationTweener
    {
        public override LTDescr Tweener
        {
            get
            {
                float value = Showing ? Info.maxAlpha : Info.minAlpha;
                return LeanTween.alphaCanvas(Info.cg, value, Info.duration);
            }
        }

        public UIAlphaAnimationInfo Info;
        public override UIAnimationInfo InfoBase => Info;

        protected override void Reset()
        {
            base.Reset();
            Info.cg = gameObject.GetAddComponent<CanvasGroup>();
        }

        public override void Init()
        {
            base.Init();
            Info.cg.alpha = Info.initialValue ? Info.maxAlpha : Info.minAlpha;
            SetInteraction();
        }

        protected override void OnAnimationFinish()
        {
            SetInteraction();
            base.OnAnimationFinish();
        }

        public void SetInteraction()
        {
            if (Info.alwaysInteractable) { Info.cg.blocksRaycasts = Info.cg.interactable = true; return; }
            if (Info.changeInteractableStatus) { Info.cg.blocksRaycasts = Info.cg.interactable = Showing; return; }
        }
    }
}