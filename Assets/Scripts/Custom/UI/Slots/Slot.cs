using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.UI
{
    [System.Serializable]
    public abstract class Slot<T> : MonoBehaviour where T : SlotElement
    {
        public SlotsMngr<T> Mngr => Group.Mngr;
        public SlotGroup<T> Group;
        public string Id { get; private set; }
        public string GroupId => Group.GroupId;
        public bool IsActive => Group.IsActive;
        public bool Interchangeable => Group.Interchangeable;
        public T Element { get; private set; }

        public ElementEvent OnRemove { get; set; } = new ElementEvent();
        public ElementEvent OnAdd { get; set; } = new ElementEvent();
        public CompareElementEvent OnUpdate { get; set; } = new CompareElementEvent();
        public bool ExternalChecks { get; private set; } = true;

        public System.Action<Slot<T>> OnExtraChecks;

        public virtual bool InUse { get { return Element != null; } }

        public virtual void Init(SlotGroup<T> mngr)
        {
            Id = System.Guid.NewGuid().ToString();
            Group = mngr;
        }

        public virtual void Add(T obj)
        {
            Element = obj;
            OnAdd?.Invoke(Element);
        }

        public virtual void Change(T obj)
        {
            T old = Element;
            Element = obj;
            OnUpdate?.Invoke(old, Element);
        }

        public virtual void Remove()
        {
            T oldElement = Element;
            Element = null;
            OnRemove?.Invoke(oldElement);
        }

        public virtual void RemoveWithoutNotify()
        {
            Element = null;
        }

        public virtual bool CanAddFrom(Slot<T> from)
        {
            ExternalChecks = true;
            OnExtraChecks?.Invoke(from);
            if (from.Id == Id || !from.InUse || !ExternalChecks) return false;

            if (!Interchangeable && from.GroupId == GroupId) return false;

            return true;
        }

        public void UpdateExternalChecks(bool newValue)
        {
            ExternalChecks = ExternalChecks && newValue;
        }

        [System.Serializable] public class ElementEvent : UnityEvent<T> { }

        [System.Serializable] public class CompareElementEvent : UnityEvent<T, T> { }

        [System.Serializable] public class SlotEvent : UnityEvent<Slot<T>> { }
    }
}