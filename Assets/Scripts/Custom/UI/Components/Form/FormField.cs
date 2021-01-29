using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Utilities.UI
{
    public class FormField : MonoBehaviour
    {
        public Selectable selectable;
        public bool alsoUseEnterForNextSelection = false;

        [HideInInspector] public bool required = false;
        public UnityEvent OnEnter = new UnityEvent();

        protected virtual void Reset()
        {
            selectable = GetComponent<Selectable>();
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && EventSystem.current.currentSelectedGameObject == selectable.gameObject)
            {
                bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                this.ExecuteNextFrame(() =>
                {
                    if (shift) SelectPrevious();
                    else SelectNext();
                });
            }

            if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                 && EventSystem.current.currentSelectedGameObject == selectable.gameObject)
            {
                if (alsoUseEnterForNextSelection) SelectNext();
                OnEnter?.Invoke();
            }
        }

        public void SelectPrevious()
        {
            var selectable = this.selectable.FindSelectableOnLeft();
            if (selectable == null) selectable = this.selectable.FindSelectableOnUp();
            selectable?.Select();
        }

        public void SelectNext()
        {
            var selectable = this.selectable.FindSelectableOnRight();
            if (selectable == null) selectable = this.selectable.FindSelectableOnDown();
            selectable?.Select();
        }
    }
}