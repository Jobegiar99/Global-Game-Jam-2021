using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIConnector : MonoBehaviour
{
    public RectTransform rt;
    public RectTransform canvasRT;
    public Canvas canvas;
    public Vector2 offset2D;
    public bool onUpdate = false;

    private Camera _cam = null;

    public Camera Cam
    {
        get
        {
            if (_cam == null) _cam = Camera.main;
            return _cam;
        }
    }

    private void Reset()
    {
        GetScripts();
    }

    private void Awake()
    {
        GetScripts();
    }

    protected virtual void GetScripts()
    {
        if (!canvasRT)
        {
            rt = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            canvasRT = canvas.GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (onUpdate) SetPosition();
    }

    public abstract Vector2 AnchoredPosition { get; }

    public virtual void SetPosition()
    {
        Vector2 position = AnchoredPosition + offset2D;
        rt.anchoredPosition = KeepFullyOnScreen(position);
    }

    protected virtual Vector3 KeepFullyOnScreen(Vector3 newPos)
    {
        float minX = (canvasRT.sizeDelta.x - rt.sizeDelta.x) * -0.5f;
        float maxX = (canvasRT.sizeDelta.x - rt.sizeDelta.x) * 0.5f;
        float minY = (canvasRT.sizeDelta.y - rt.sizeDelta.y) * -0.5f;
        float maxY = (canvasRT.sizeDelta.y - rt.sizeDelta.y) * 0.5f;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        return newPos;
    }
}