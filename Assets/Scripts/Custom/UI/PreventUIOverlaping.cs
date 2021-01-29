using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.UI;
using System.Linq;

namespace Utilities.UI
{
    public class PreventUIOverlaping : MonoBehaviour
    {
        private List<RectTransform> Elements { get; } = new List<RectTransform>();

        private class Sorter : IComparer<RectTransform>
        {
            public int Compare(RectTransform x, RectTransform y)
            {
                return x.WorldRect().y.CompareTo(y.WorldRect().y);
            }
        }

        private void Awake()
        {
            Sort();
        }

        public void Add(RectTransform el)
        {
            Elements.Add(el);
            Sort();
        }

        public void Remove(RectTransform el)
        {
            Elements.Remove(el);
        }

        private void Sort()
        {
            Elements.Sort(new Sorter());
        }

        private void LateUpdate()
        {
            for (int i = 0; i < Elements.Count - 1; i++)
            {
                if (Elements[i].Overlaps(Elements[i + 1]))
                {
                    Fix(Elements[i], Elements[i + 1]);
                }
            }
        }

        private void Fix(RectTransform a, RectTransform b)
        {
            Vector3 newPos = b.anchoredPosition;
            float distance = b.anchoredPosition.y - a.anchoredPosition.y;
            newPos.y += distance;
            b.anchoredPosition = newPos;
        }
    }
}