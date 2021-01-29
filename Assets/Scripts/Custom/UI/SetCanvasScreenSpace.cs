using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class SetCanvasScreenSpace : MonoBehaviour
{
    [SerializeField] private Canvas m_canvas;

    private void Reset()
    {
        m_canvas = gameObject.GetAddComponent<Canvas>();
    }

    private void Awake()
    {
        m_canvas.renderMode = RenderMode.ScreenSpaceCamera;
        m_canvas.worldCamera = Camera.main;
    }
}