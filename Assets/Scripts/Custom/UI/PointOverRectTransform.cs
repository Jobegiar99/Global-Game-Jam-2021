using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    public class PointOverRectTransform : MonoBehaviour
    {
        [SerializeField] private RectTransform rt;
        [SerializeField] private bool m_onUpdate = false;

        public bool IsOver { get; private set; } = false;
        public Vector2 Point { get; set; }
        private bool IsOverTrigger { get; set; } = false;
        public UnityEngine.Events.UnityEvent OnOver;
        public UnityEngine.Events.UnityEvent OnExit;

        private void Reset()
        {
            rt = gameObject.GetAddComponent<RectTransform>();
        }

        private void OnDisable()
        {
            IsOver = false;
        }

        public bool PointIsOver(Vector2 point)
        {
            if (rt == null) return false;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, point, null, out Vector2 lp);
            return rt.rect.Contains(lp);
        }

        public virtual void Update()
        {
            if (m_onUpdate) return;

            IsOver = PointIsOver(Point);

            if (IsOverTrigger != IsOver)
            {
                if (IsOver) OnOver?.Invoke(); else OnExit?.Invoke();
                IsOverTrigger = IsOver;
            }
        }
    }
}