using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Utilities.UI
{
    public abstract class SlotGroup<T> : MonoBehaviour where T : SlotElement
    {
        public SlotsMngr<T> Mngr => SlotsMngr<T>.Instance;
        public bool IsActive { get; set; } = true;
        public string GroupId { get; private set; }

        protected virtual bool AutoGetSlots => true;
        public bool Interchangeable => interchangeable;
        [SerializeField] private bool interchangeable;

        public List<Slot<T>> slots;
        public bool Dragging => DraggingSlot != null;
        public static Slot<T> DraggingSlot => SlotsMngr<T>.Instance.DraggingSlot;

        public SlotsMngr<T>.MoveEvent OnMoveWithin { get; } = new SlotsMngr<T>.MoveEvent();
        public SlotsMngr<T>.MoveEvent OnMoveOut { get; } = new SlotsMngr<T>.MoveEvent();
        public SlotsMngr<T>.MoveEvent OnMoveIn { get; } = new SlotsMngr<T>.MoveEvent();

        public virtual void Awake()
        {
            if (AutoGetSlots) slots = new List<Slot<T>>(GetComponentsInChildren<Slot<T>>());
            GroupId = System.Guid.NewGuid().ToString();

            slots.ForEach(s =>
            {
                s.Init(this);
            });
        }

        public virtual void Start()
        {
            Mngr.OnMove?.AddListener((from, to) =>
            {
                if (from.GroupId != GroupId && to.GroupId != GroupId) return;
                if (from.GroupId == GroupId && to.GroupId == GroupId) { OnMoveWithin?.Invoke(from, to); return; }
                if (from.GroupId == GroupId) { OnMoveOut?.Invoke(from, to); return; }
                if (to.GroupId == GroupId) { OnMoveIn?.Invoke(to, from); }
            });
        }
    }
}