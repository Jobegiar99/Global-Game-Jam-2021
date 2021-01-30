using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI.Animation
{
    [System.Serializable]
    public class UIPositionAnimationInfo : UIAnimationInfo
    {
        public RectTransform rectTransform;
        public Vector2 startPosition;
        public Vector2 finalPosition;
    }
}