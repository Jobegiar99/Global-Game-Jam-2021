using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Utilities.UI
{
    public class ButtonEvents : Button
    {
        public UnityEvent onSelect;
        public UnityEvent onDeselect;
        public UnityEvent onPointerDown;
        public UnityEvent onPointerUp;

        public bool selectOnHover = true;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            onSelect?.Invoke();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            onDeselect?.Invoke();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (!interactable) return;

            onPointerDown?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (!interactable) return;

            onPointerUp?.Invoke();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (selectOnHover) Select();
        }
    }
}