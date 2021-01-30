using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI.Animation
{
    [System.Serializable]
    public class UIColorAnimationInfo : UIAnimationInfo
    {
        public Image graphic;
        public Color from;
        public Color to;
    }
}