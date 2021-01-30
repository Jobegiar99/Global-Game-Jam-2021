using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class EaseInOutAnimation : MonoBehaviour
    {
        public AnimationCurve defaultCurve;
        public bool checkInstance=false;

        //Obtener instancia
        [HideInInspector] public static EaseInOutAnimation ins { get { if (!_ins) _ins = FindObjectOfType<EaseInOutAnimation>(); return _ins; } }
        static EaseInOutAnimation _ins = null;


        public void Animate(bool val, CanvasGroup cg, float duration, AnimationCurve curve = null)
        {
            if (curve == null)
                curve = defaultCurve;


            StartCoroutine(Anim(duration, val, cg, curve));


        }


        IEnumerator Anim(float duration, bool enable, CanvasGroup cg, AnimationCurve fadeCurve)
        {

            float currentAlpha = cg.alpha;

            if (currentAlpha == (enable ? 1 : 0))
                yield break;

            float startTime = Time.time;
            float durationTime = Time.time + duration;

            while (Time.time <= durationTime)
            {
                float curveValue = fadeCurve.Evaluate((Time.time - startTime) / duration);
                cg.alpha = enable ? curveValue : 1 - curveValue;
                yield return null;
            }
            cg.alpha = enable ? 1 : 0;
            cg.interactable = cg.blocksRaycasts = enable;
        }


    }

}