using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    public class ShowTooltipEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public RectTransform rt;
        public string text;

        private void Reset()
        {
            rt = GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Tooltip.Instance.Show(text, rt);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tooltip.Instance.Hide();
        }
    }
}