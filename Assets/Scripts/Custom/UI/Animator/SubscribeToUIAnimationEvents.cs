using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.UI.Animation
{
    public class SubscribeToUIAnimationEvents : MonoBehaviour
    {
        public UIAnimator anim;
        public UnityEvent onStartShow;
        public UnityEvent onStartHide;

        private void Reset()
        {
            anim = GetComponent<UIAnimator>();
        }

        private void Awake()
        {
            anim.OnStartAnimation += (val) =>
            {
                if (val) { onStartShow?.Invoke(); }
                else { onStartHide?.Invoke(); }
            };
        }
    }
}