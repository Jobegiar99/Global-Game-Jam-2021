using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using Utilities._Input;
using Utilities.UI.Animation;

namespace Utilities.UI.Menu
{
    public class MenuBase : MonoBehaviour, IFocus
    {
        public UnityEngine.UI.Selectable defaultUIElement;
        public UIAnimator animator;
        public string menuName;
        public string menuId;
        public UnityEvent onShow = new UnityEvent();
        public UnityEvent onHide = new UnityEvent();

        private void Reset()
        {
            animator = gameObject.GetComponent<UIAnimator>();
        }

        protected virtual void Awake()
        {
            animator.OnAnimationFinish += (val) =>
            {
                if (val) defaultUIElement?.Select();
            };
        }

        protected virtual void Update()
        {
            if (!IsShowing || !this.HasFocus()) return;
            UpdateActions();
        }

        public virtual void UpdateActions()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.ExecuteNextFrame(() =>
                {
                    ManagePrevious();
                });
            }
        }

        public virtual void ManagePrevious()
        {
            MenuNavigation.Instance.ShowPrevious();
        }

        public virtual void Show()
        {
            animator.Show();
            onShow?.Invoke();
            this.Select();
        }

        public virtual void Hide()
        {
            animator.Hide();
            onHide?.Invoke();
        }

        public virtual void OnSelect()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnDeselect()
        {
            throw new System.NotImplementedException();
        }

        public bool IsShowing { get { return animator.Showing; } }

        public virtual string FocusGroup => InputHelper.MenuFocus;
    }
}