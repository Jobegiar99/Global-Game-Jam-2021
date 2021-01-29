using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI.Animation
{
    [System.Serializable]
    public class UIFillAnimationInfo : UIAnimationInfo

    {
        public float min = 0;
        public float max = 1;
        public Image img;
    }
}