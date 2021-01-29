using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMouseConnector : UIConnector
{
    public override Vector2 AnchoredPosition
    {
        get
        {
            Vector2 temp = Cam.ScreenToViewportPoint(Input.mousePosition);

            temp.x *= canvasRT.sizeDelta.x;
            temp.y *= canvasRT.sizeDelta.y;

            temp.x -= canvasRT.sizeDelta.x * canvasRT.pivot.x;
            temp.y -= canvasRT.sizeDelta.y * canvasRT.pivot.y;
            return temp;
        }
    }
}