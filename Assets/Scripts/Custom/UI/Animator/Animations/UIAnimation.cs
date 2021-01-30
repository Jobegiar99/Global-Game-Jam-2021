using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Animation
{
    public abstract class UIAnimation : MonoBehaviour
    {
        public bool m_Invert;
        public bool initOnAwake = true;
        public bool Showing { get; protected set; } = false;
        public bool Animating { get; protected set; } = false;
        public abstract UIAnimationInfo InfoBase { get; }

        protected virtual void Reset()
        {
            var anim = GetComponent<UIAnimator>();
            if (anim) anim.animations.Add(this);
        }

        private void OnDestroy()
        {
            var anim = GetComponent<UIAnimator>();
            if (anim) anim.animations.Remove(this);
        }

        public void Awake()
        {
            if (initOnAwake) Init();
        }

        public virtual void Init()
        {
            Showing = InfoBase.initialValue;
        }

        public virtual void Animate(bool show)
        {
            if (Showing == show) return;
            Animating = true;
            Showing = show;
        }

        protected virtual void OnAnimationFinish()
        {
            Animating = false;
            InfoBase.OnAnimationFinish?.Invoke();
        }
    }
}