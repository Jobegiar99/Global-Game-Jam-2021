using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class UILinearLayout : MonoBehaviour
    {
        [SerializeField] private List<LayoutItem> items = new List<LayoutItem>();

        public Vector2 direction = Vector2.right;
        private float oldSpacing = -1;
        public float spacing = 0;

        public RectTransform toFrontRT;
        public bool toFront = false;

        public System.Action<float, float> OnSpacingChange { get; set; }

        private void Awake()
        {
            oldSpacing = spacing;
            UpdateLayout();
            OnSpacingChange += (old, @new) => UpdateLayout();
        }

        private void Update()
        {
            if (spacing != oldSpacing)
            {
                OnSpacingChange(oldSpacing, spacing);
                oldSpacing = spacing;
            }

            if (toFront)
            {
                BringToFront(toFrontRT);
                toFront = false;
            }
        }

        public void AddItem(RectTransform rt)
        {
            var item = new LayoutItem()
            {
                rt = rt,
                position = items.Count
            };

            items.Add(item);
            UpdateLayout();
        }

        public void AddItem(RectTransform rt, int position)
        {
            var item = new LayoutItem()
            {
                rt = rt,
                position = position,
            };

            items.Add(item);
            UpdateLayout();
        }

        public void RemoveItem(RectTransform rt)
        {
            items.RemoveAll(item => item.rt == rt);

            for (int i = 0; i < items.Count; i++)
                items[i] = new LayoutItem() { position = i, rt = items[i].rt };

            UpdateLayout();
        }

        public void BringToFront(RectTransform rt)
        {
            rt.SetSiblingIndex(ItemsCount);
        }

        public void UpdateLayout()
        {
            items.ForEach(item =>
            {
                item.rt.anchoredPosition = direction * item.position * spacing;
            });
        }

        public int ItemsCount => items.Count;
    }

    [System.Serializable]
    public struct LayoutItem
    {
        public RectTransform rt;
        public int position;
    }
}