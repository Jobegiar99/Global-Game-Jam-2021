using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    public abstract class SlotUI<T> : Slot<T>,
        IBeginDragHandler,
        IEndDragHandler,
        IDragHandler,
        IDropHandler,
        IPointerClickHandler,
        IPointerEnterHandler,
        IPointerExitHandler,
        ISlotInputPC
        where T : SlotElement
    {
        public static Slot<T> DraggingSlot => SlotGroup<T>.DraggingSlot;
        public UnityEvent OnBeginDragEvent { get; } = new UnityEvent();
        public UnityEvent OnEndDragEvent { get; } = new UnityEvent();
        public UnityEvent OnDragEvent { get; } = new UnityEvent();
        public UnityEvent OnDropEvent { get; } = new UnityEvent();
        public UnityEvent OnClickEvent { get; } = new UnityEvent();
        public UnityEvent OnHoverEvent { get; } = new UnityEvent();
        public UnityEvent OnUnhoverEvent { get; } = new UnityEvent();

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsActive || !InUse) return;
            BeginDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!IsActive || !InUse) return;
            Drag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!IsActive) return;
            EndDrag();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (!IsActive) return;
            Drop();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsActive) return;
            Hover();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsActive) return;
            UnHover();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsActive) return;
            if (!wasDragging) Click();
            wasDragging = false;
        }

        private bool wasDragging = false;

        public virtual void BeginDrag()
        {
            Mngr.DraggingSlot = this;
            wasDragging = true;
            OnBeginDragEvent?.Invoke();
        }

        public virtual void EndDrag()
        {
            Mngr.DraggingSlot = null;
            OnEndDragEvent?.Invoke();
        }

        public virtual void Drag()
        {
            OnDragEvent?.Invoke();
        }

        public virtual void Drop()
        {
            if (DraggingSlot != null) Mngr.Move(DraggingSlot, this);

            OnDropEvent?.Invoke();
        }

        public virtual void Click()
        {
            OnClickEvent?.Invoke();
        }

        public virtual void Hover()
        {
            OnHoverEvent?.Invoke();
        }

        public virtual void UnHover()
        {
            OnUnhoverEvent?.Invoke();
        }
    }
}