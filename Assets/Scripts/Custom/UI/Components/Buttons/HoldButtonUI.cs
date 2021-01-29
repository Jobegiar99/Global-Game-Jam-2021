using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Utilities.UI
{
    public class HoldButtonUI : Selectable
    {
        public Image fillImage;
        public float holdTime = 1f;
        private bool holding = false;
        public float holdPercentage = 0;
        public UnityEvent onHoldStart;
        public UnityEvent onHoldFinish;
        [HideInInspector] public UnityEvent onHoldCancel;

        private bool oldHolding = false;

        public void Update()
        {
            if (holding && interactable)
            {
                if (oldHolding != holding) onHoldStart?.Invoke();
                holdPercentage += Time.deltaTime / holdTime;
                fillImage.fillAmount = holdPercentage;
            }

            if (oldHolding != holding) { oldHolding = holding; }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            if (!holding) return;
            holding = false;
            holdPercentage = 0;
            onHoldCancel?.Invoke();
            fillImage.fillAmount = holdPercentage;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            holding = true;
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (holding == false) return;
            if (holdPercentage >= 1)
            {
                onHoldFinish?.Invoke();
            }
            else
            {
                onHoldCancel?.Invoke();
            }

            holdPercentage = 0;
            fillImage.fillAmount = holdPercentage;
            holding = false;

            base.OnPointerUp(eventData);
        }
    }
}