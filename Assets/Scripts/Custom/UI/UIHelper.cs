using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public static class UIHelper
    {
        public enum Anchor { Left, Right, Up, Down };

        public static bool Overlaps(this RectTransform a, RectTransform b)
        {
            return a.WorldRect().Overlaps(b.WorldRect());
        }

        public static Rect WorldRect(this RectTransform rt)
        {
            var worldCorners = new Vector3[4];
            rt.GetWorldCorners(worldCorners);
            var result = new Rect(
                          worldCorners[0].x,
                          worldCorners[0].y,
                          worldCorners[2].x - worldCorners[0].x,
                          worldCorners[2].y - worldCorners[0].y);
            return result;
        }

        public static void SetAnchorsAndPivot(ref RectTransform rt, Anchor anchor, bool extended = false)
        {
            Vector2 anchorMin = Vector2.zero;
            Vector2 anchorMax = Vector2.zero;
            Vector2 pivot = Vector2.zero;
            switch (anchor)
            {
                case Anchor.Left:
                    if (extended) { anchorMin = new Vector2(0, 0); anchorMax = new Vector2(0, 1); }
                    else { anchorMin = anchorMax = new Vector2(0, 0.5f); }
                    pivot = new Vector2(0, 0.5f);
                    break;

                case Anchor.Right:
                    if (extended) { anchorMin = new Vector2(1, 0); anchorMax = new Vector2(1, 1); }
                    else { anchorMin = anchorMax = new Vector2(1, 0.5f); }
                    pivot = new Vector2(1, 0.5f);
                    break;

                case Anchor.Up:
                    if (extended) { anchorMin = new Vector2(0, 1); anchorMax = new Vector2(1, 1); }
                    else { anchorMin = anchorMax = new Vector2(0.5f, 1); }
                    pivot = new Vector2(0.5f, 1);
                    break;

                case Anchor.Down:
                    if (extended) { anchorMin = Vector2.zero; anchorMax = new Vector2(1f, 0); }
                    else { anchorMin = anchorMax = new Vector2(0.5f, 0); }
                    pivot = new Vector2(0.5f, 0);
                    break;

                default:
                    break;
            }

            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;
            rt.pivot = pivot;
        }

        public static void SelectElement(Button button)
        {
            button.Select();
        }

        public static void EnableCG(CanvasGroup cg, bool value)
        {
            cg.alpha = value ? 1 : 0;
            cg.interactable = value;
            cg.blocksRaycasts = value;
        }

        public static Vector2 CanvasMousePosition(RectTransform canvasRT)
        {
            Vector2 temp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            temp.x *= canvasRT.sizeDelta.x;
            temp.y *= canvasRT.sizeDelta.y;

            temp.x -= canvasRT.sizeDelta.x * canvasRT.pivot.x;
            temp.y -= canvasRT.sizeDelta.y * canvasRT.pivot.y;
            return temp;
        }

        public static Vector2 GetCenterRelativeToCanvas(RectTransform rt, RectTransform canvasRT)
        {
            float xRelativeAnchor = (rt.anchorMax.x - rt.anchorMin.x) * rt.pivot.x + rt.anchorMin.x;
            xRelativeAnchor *= canvasRT.rect.width;
            float yRelativeAnchor = (rt.anchorMax.y - rt.anchorMin.y) * rt.pivot.y + rt.anchorMin.y;
            yRelativeAnchor *= canvasRT.rect.height;

            return new Vector2(xRelativeAnchor, yRelativeAnchor);
        }

        public static Vector2 RelativeAnchorPosition(this RectTransform rt, RectTransform relativeTo, RectTransform canvasRT)
        {
            Vector2 pos = relativeTo.anchoredPosition;
            Vector2 distance = rt.localPosition - relativeTo.localPosition;
            pos += distance;
            return pos;
        }

        public static bool MouseInsideRT(RectTransform rt)
        {
            Vector2 localMousePosition = rt.InverseTransformPoint(Input.mousePosition);
            return rt.rect.Contains(localMousePosition);
        }

        public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }

        public static Vector2 ScreenToCanvas(this RectTransform canvasRT, Vector2 position)
        {
            Vector2 temp = Camera.main.ScreenToViewportPoint(position);

            temp.x *= canvasRT.sizeDelta.x;
            temp.y *= canvasRT.sizeDelta.y;
            temp.x -= canvasRT.sizeDelta.x * canvasRT.pivot.x;
            temp.y -= canvasRT.sizeDelta.y * canvasRT.pivot.y;

            return temp;
        }

        public static Rect RectTransformToScreenSpace(this RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            return new Rect((Vector2)transform.position - (size * 0.5f), size);
        }
    }
}

//1420,-1080