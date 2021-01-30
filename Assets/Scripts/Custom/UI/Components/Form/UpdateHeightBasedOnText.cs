using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Utilities.UI
{
    public class UpdateHeightBasedOnText : MonoBehaviour
    {
        public FormInputField field;
        public TextMeshProUGUI text;
        public RectTransform rt;
        public float textHeight = 30;
        private float startHeight;

        private void Reset()
        {
            rt = GetComponent<RectTransform>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            startHeight = rt.sizeDelta.y;
            field.OnChangeState += () =>
            {
                //   Debug.Log("height");
                if (string.IsNullOrEmpty(text.text))
                {
                    Vector2 size = rt.sizeDelta; size.y = startHeight;
                    rt.sizeDelta = size;
                }
                else
                {
                    Vector2 size = rt.sizeDelta; size.y = startHeight + textHeight;
                    rt.sizeDelta = size;
                }
            };
        }
    }
}