using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class SetCanvasAsCamera : MonoBehaviour
    {
        public Canvas canvas;
        public Camera cam;

        private void Reset()
        {
            cam = gameObject.GetAddComponent<Camera>();
        }

        private void Awake()
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = cam;
        }
    }
}