using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenConnector : UIConnector
{
    public Vector2 Position { get; set; }

    public override Vector2 AnchoredPosition
    {
        get
        {
            if (Cam == null) return Vector2.zero;
            Vector2 temp = Cam.ScreenToViewportPoint(Position);

            //Calculate position considering our percentage, using our canvas size
            //So if canvas size is (1100,500), and percentage is (0.5,0.5), current value will be (550,250)
            temp.x *= canvasRT.sizeDelta.x;
            temp.y *= canvasRT.sizeDelta.y;

            //The result is ready, but, this result is correct if canvas recttransform pivot is 0,0 - left lower corner.
            //But in reality its middle (0.5,0.5) by default, so we remove the amount considering cavnas rectransform pivot.
            //We could multiply with constant 0.5, but we will actually read the value, so if custom rect transform is passed(with custom pivot) ,
            //returned value will still be correct.

            temp.x -= canvasRT.sizeDelta.x * canvasRT.pivot.x;
            temp.y -= canvasRT.sizeDelta.y * canvasRT.pivot.y;
            return temp;
        }
    }
}