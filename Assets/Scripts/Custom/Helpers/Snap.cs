using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Snap : MonoBehaviour
    {
        public Vector3 offset = Vector3.up * 0.07f;

        public Vector3 startOffset = Vector3.up * 2f;
        public Vector3 direction = -Vector3.up;
        public float distance = 10f;

        public Vector3 LastPosition { get; private set; } = -1000f * Vector3.up;
        public Vector3 LastRealPosition { get; private set; }

        private void LateUpdate()
        {
            if (LastPosition == transform.position) return;

            Ray ray = new Ray(transform.position + startOffset, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                LastRealPosition = transform.position;
                transform.position = hit.point + offset;
                LastPosition = transform.position;
            }
        }
    }
}