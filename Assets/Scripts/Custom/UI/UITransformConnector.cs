using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransformConnector : UIConnector
{
    public Transform connectTo;
    public Vector3 offset3D;
    [HideInInspector] public Vector2 defaultOffset;

    public override Vector2 AnchoredPosition
    {
        get
        {
            if (Cam == null || connectTo == null) return Vector2.zero;

            Vector2 temp = Cam.WorldToViewportPoint(connectTo.position + offset3D);
            temp.x *= canvasRT.sizeDelta.x;
            temp.y *= canvasRT.sizeDelta.y;
            temp.x -= canvasRT.sizeDelta.x * canvasRT.pivot.x;
            temp.y -= canvasRT.sizeDelta.y * canvasRT.pivot.y;
            return temp;
        }
    }
}