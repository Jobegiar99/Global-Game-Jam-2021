using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Utilities.UI.Animation
{
    public class UIAnimator : MonoBehaviour
    {
        [SerializeField] public List<UIAnimation> animations = new List<UIAnimation>();
        public bool initialValue = false;
        public bool toggle = false;

        public Events.BoolEvent OnStartAnimation { get; set; }
        public Events.BoolEvent OnAnimationFinish { get; set; }
        public bool Showing { get; private set; } = false;
        public bool Animating { get; private set; }
        private bool HasEvents { get; set; } = false;
        private UIAnimation LongestAnimation { get; set; } = null;

        public GameObject objectToDisable = null;

        private void Reset()
        {
            objectToDisable = gameObject;
            animations.AddRange(GetComponentsInChildren<UIAnimation>());
        }

        private void Awake()
        {
            Showing = initialValue;
            animations.ForEach(el => InitAnimation(el));
            Refresh(false);
        }

        public void InitAnimation(UIAnimation anim)
        {
            anim.Init();

            if (LongestAnimation == null || anim.InfoBase.TotalTime > LongestAnimation.InfoBase.TotalTime)
                LongestAnimation = anim;
        }

        public void Refresh(bool waitOneFrame = true)
        {
            if (!waitOneFrame)
                Init();
            else this.ExecuteNextFrame(Init);
        }

        private void Init()
        {
            if (objectToDisable) objectToDisable.SetActive(Showing);
            OnAnimationFinish += (val) =>
            {
                Animating = false;
                if (!val && objectToDisable) objectToDisable.SetActive(false);
            };

            if (!HasEvents && LongestAnimation != null)
                LongestAnimation.InfoBase.OnAnimationFinish += () =>
                {
                    OnAnimationFinish?.Invoke(Showing);
                };
            HasEvents = true;
        }

        public void Toggle()
        {
            if (!Showing) { Show(); } else { Hide(); }
        }

        private void Update()
        {
            if (toggle)
            {
                Toggle();
                toggle = false;
            }
        }

        private void Animate(bool val)
        {
            Animating = true;
            Showing = val;
            if (!gameObject.activeInHierarchy) return;

            animations.ForEach(i =>
            {
                this.ExecuteLater(() => i.Animate(val), i.InfoBase.delay);
            });
            OnStartAnimation?.Invoke(val);
        }

        public void Show()
        {
            if (Showing) return;
            if (objectToDisable) objectToDisable.gameObject.SetActive(true);
            Animate(true);
        }

        public void Hide()
        {
            if (!Showing) return;
            Animate(false);
        }
    }
}