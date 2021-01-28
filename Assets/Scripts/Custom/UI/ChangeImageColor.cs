using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class ChangeImageColor : MonoBehaviour
    {
        public Image img;

        private void Reset()
        {
            img = Helper.GetAddComponent<Image>(gameObject);
        }

        public void Set(Color color)
        {
            img.color = color;
        }

        public void Set(string hex)
        {
            Set(ColorHelper.ColorFromHex(hex));
        }
    }
}