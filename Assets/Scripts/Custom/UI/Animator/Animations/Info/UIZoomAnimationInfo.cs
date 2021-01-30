using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Animation
{
    [System.Serializable]
    public class UIZoomAnimationInfo : UIAnimationInfo
    {
        public RectTransform rt;
        public Vector3 max = Vector3.one;
        public Vector3 min = Vector3.zero;
    }
}