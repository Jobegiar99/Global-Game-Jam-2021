using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Physic
{
    public class AttachRigidbodyToTransform : MonoBehaviour
    {
        public Rigidbody rb;
        public Transform target;

        public bool removeParentOnStart = false;

        private Quaternion startRotation;
        private Vector3 startPosition;

        private void Awake()
        {
            startRotation = rb.transform.localRotation;
            startPosition = rb.transform.localPosition;
            if (removeParentOnStart) rb.transform.SetParent(null, true);
        }

        private void FixedUpdate()
        {
            rb.MovePosition(target.TransformPoint(startPosition));
            rb.MoveRotation(target.transform.rotation * startRotation);
        }
    }
}