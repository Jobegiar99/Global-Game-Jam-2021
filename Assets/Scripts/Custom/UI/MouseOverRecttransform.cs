using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class MouseOverRecttransform : PointOverRectTransform
    {
        public override void Update()
        {
            Point = Input.mousePosition;
            base.Update();
        }
    }
}