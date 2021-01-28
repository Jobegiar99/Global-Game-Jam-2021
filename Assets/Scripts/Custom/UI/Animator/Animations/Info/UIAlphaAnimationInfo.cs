using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI.Animation
{
    [System.Serializable]
    public class UIAlphaAnimationInfo : UIAnimationInfo
    {
        public CanvasGroup cg;
        public float minAlpha = 0;
        public float maxAlpha = 1;
        public bool changeInteractableStatus = true;
        public bool alwaysInteractable = false;
    }
}