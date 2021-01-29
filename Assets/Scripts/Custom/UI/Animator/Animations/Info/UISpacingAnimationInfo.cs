using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI.Animation
{
    [System.Serializable]
    public class UISpacingAnimationInfo : UIAnimationInfo

    {
        public float min = 0;
        public float max = 10;
        public HorizontalOrVerticalLayoutGroup lg;
        public List<RectTransform> layouts;
    }
}