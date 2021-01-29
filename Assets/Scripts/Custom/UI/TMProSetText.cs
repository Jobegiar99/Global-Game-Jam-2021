using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class TMProSetText : MonoBehaviour
    {
        TMPro.TextMeshProUGUI tmpro;
        public bool isFloat = true;

        private void Awake()
        {
            tmpro = GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void SetText(float t)
        {

            tmpro.SetText(isFloat ? t.ToString("F2") : t.ToString());
        }

    }
}