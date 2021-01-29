using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.UI
{
    public class SlotsMngr<T> : MonoBehaviour where T : SlotElement
    {
        public static SlotsMngr<T> Instance { get; private set; }
        public bool Dragging => DraggingSlot != null;
        public Slot<T> DraggingSlot { get; set; } = null;
        public MoveEvent OnMove { get; } = new MoveEvent();

        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }

        public virtual bool Move(Slot<T> from, Slot<T> to)
        {
            if (from == null || to == null || !from.InUse) return false;
            if (!to.CanAddFrom(from)) return false;
            if (to.InUse && !from.CanAddFrom(to)) return false;
            T temp = to.Element;
            OnMove?.Invoke(from, to);

            if (temp != null)
            {
                to.Change(from.Element);
                from.Change(temp);
            }
            else
            {
                to.Add(from.Element);
                from.Remove();
            }

            return true;
        }

        [System.Serializable] public class MoveEvent : UnityEvent<Slot<T>, Slot<T>> { }
    }
}