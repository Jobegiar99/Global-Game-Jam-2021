using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class BarUI : MonoBehaviour
    {
        public bool showText = true;
        public TextMeshProUGUI text;
        public Image bar;
        public float max;
        public float value;
        [Tooltip("Just for reference")] public Image background;

        public void SetColor(Color color)
        {
            bar.color = color;
            GetComponent<Image>().color = ColorHelper.ChangeColorBrightness(color, -0.5f);
        }

        protected virtual void Awake()
        {
            SetValue(value);
        }

        protected virtual void UpdateUI()
        {
            if (bar) bar.fillAmount = value / max;
        }

        public virtual void SetValue(float value)
        {
            this.value = value;
            if (showText) text?.SetText(value.ToString(value % 1 == 0 ? "F0" : "F2"));
            else text?.SetText("");
            UpdateUI();
        }

        public void SetValue(float value, float max)
        {
            this.max = max;
            SetValue(value);
        }

        public void OverrideTextValue(float value)
        {
            if (showText) text.SetText(value.ToString(value % 1 == 0 ? "F0" : "F2"));
        }
    }
}