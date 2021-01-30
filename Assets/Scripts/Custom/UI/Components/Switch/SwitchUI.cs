using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class SwitchUI : MonoBehaviour
    {
        public Graphic knob;
        public Graphic background;
        public Color tColor = Color.cyan;
        public Color fColor = Color.white;
        public bool val;

        private void Awake()
        {
            SetState(val);
        }

        public void SetState(bool value)
        {
            background.color = knob.color = value ? tColor : fColor;
            knob.rectTransform.anchoredPosition = !value ? Vector2.zero : Vector2.right * knob.rectTransform.rect.width;
            val = value;
        }
    }
}