using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class BarUIStepper : BarUI
    {
        public Image[] fill;

        protected override void UpdateUI()
        {
            int count = (int)value;
            for (int i = 0; i < fill.Length; i++)
            {
                fill[i].gameObject.SetActive(i < count);
            }
        }
    }
}