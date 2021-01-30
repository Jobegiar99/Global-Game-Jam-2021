using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class ButtonToURL : MonoBehaviour
    {
        public string url;
        public Button button;

        private void Reset()
        {
            button = GetComponent<Button>();
        }

        private void Awake()
        {
            button.onClick.AddListener(() => Application.OpenURL(url));
        }
    }
}