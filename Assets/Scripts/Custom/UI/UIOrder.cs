using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class UIOrder : MonoBehaviour
    {
        public RectTransform rt;
        public Canvas canvas;
        public int defaultValue = 0;
        public int frontValue = 1;
        public bool alreadyInit = false;

        public void Awake()
        {
            if (alreadyInit) return;
            canvas = Helper.GetAddComponent<Canvas>(rt.gameObject);
            canvas.overrideSorting = true;
            Helper.GetAddComponent<GraphicRaycaster>(rt.gameObject);
            alreadyInit = true;
        }

        public void BrintToFront()
        {
            canvas.sortingOrder = frontValue;
        }

        public void SetDefault()
        {
            canvas.sortingOrder = defaultValue;
        }


        public void BringToFrontOnShow(bool val)
        {
            if (val) BrintToFront();
            else SetDefault();
        }


    }
}