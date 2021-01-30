using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Math
{
    public static class CustomMath
    {
        public static bool Approximately(float a, float b, float threshold = 0.05f)
        {
            return Mathf.Abs(a - b) <= threshold;
        }

        public static (int, int) GetAspecRatio(float width, float height)
        {
            float aspect = width / height;

            if (Approximately(aspect, 16f / 9f))
                return (16, 9);
            if (Approximately(aspect, 21f / 9f))
                return (21, 9);
            if (Approximately(aspect, 32f / 9f))
                return (32, 9);
            if (Approximately(aspect, 5f / 3f))
                return (5, 3);
            if (Approximately(aspect, 16f / 10f))
                return (16, 10);
            if (Approximately(aspect, 3f / 2f))
                return (3, 2);
            if (Approximately(aspect, 4f / 3f))
                return (4, 3);
            if (Approximately(aspect, 5f / 4f))
                return (5, 4);

            return (0, 0);
        }

        public static Vector3 Perpendicular(this Vector3 v1, Vector3 v2)
        {
            return Vector3.Cross(v1, v2);
        }

        public static Vector3 SwitchUpAxis(Vector3 v)
        {
            return new Vector3(v.x, v.z, v.y);
        }

        public static Vector3 ConvertCoordinates(Vector3 point, Vector3 origin, Vector3 up, Vector3 right)
        {
            Vector3 forward = Perpendicular(up, right);
            Vector3 x = right * point.x;
            Vector3 z = up * point.z;
            Vector3 y = forward * point.y;
            Vector3 final = x + y + z + origin;
            return final;
        }

        public static Vector3 Centroid(List<Vector3> points)
        {
            float x = 0, y = 0, z = 0;
            points.ForEach(i =>
            {
                x += i.x;
                y += i.y;
                z += i.z;
            });

            float n = points.Count;
            return new Vector3(x / n, y / n, z / n);
        }

        public static Vector2 WolrdToCanvas(Vector3 position, RectTransform canvasRT, Vector2 offset = default)
        {
            //For example (0,0) is lower left, middle is (0.5,0.5)
            Vector2 temp = Camera.main.WorldToViewportPoint(position);

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

            return temp + offset;
        }

        public static Vector2 CurvePointParabola(Vector2 a, Vector2 b, float percentage)
        {
            Vector2 res = Vector2.zero;
            Vector2 relativePos = b - a;

            if (a == b) { return a; }

            float desiredX = Mathf.Abs(relativePos.x);
            float desiredY = Mathf.Abs(relativePos.y);

            float multiplier = Mathf.Sqrt((desiredX * desiredX) / desiredY);

            float currentX = desiredX * percentage;
            float currentY = (currentX * currentX) / (multiplier * multiplier);

            res.x = currentX * Mathf.Sign(relativePos.x);
            res.y = currentY * Mathf.Sign(relativePos.y);

            return res + a;
        }

        public static float CalculateAngle(Vector3 from, Vector3 to)
        {
            return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
        }
    }
}