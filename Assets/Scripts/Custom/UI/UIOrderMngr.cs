using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class UIOrderMngr : MonoBehaviour
    {
        public List<UIOrder> orders = new List<UIOrder>();
        [Tooltip("-1 for auto ")] public int customFrontValue = -1;

        private void Awake()
        {
            Execute();
        }

        public void Execute()
        {
            orders.Clear();
            orders.AddRange(GetComponentsInChildren<UIOrder>());

            orders.ForEach(el =>
            {
                el.Awake();
                el.defaultValue = el.canvas.transform.parent.GetSiblingIndex();
                el.frontValue = customFrontValue == -1 ? orders.Count + 1 : customFrontValue;
                el.SetDefault();
            });
        }
    }
}