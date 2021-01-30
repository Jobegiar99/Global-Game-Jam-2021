using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.General
{
    public class ResetPosition : MonoBehaviour
    {
        [SerializeField] private PositionType positionType;
        [SerializeField] private Transform otherTransform;
        [SerializeField] private bool isLocalPostion;
        [SerializeField] private Vector3 position;

        private void Awake()
        {
            if (positionType == PositionType.StartPosition)
                position = isLocalPostion ? transform.localPosition : transform.position;
            if (positionType == PositionType.FromOtherTransform)
                position = otherTransform.InverseTransformPoint(transform.position);
        }

        private void LateUpdate()
        {
            Vector3 pos = position;
            if (positionType == PositionType.FromOtherTransform)
                pos = otherTransform.TransformPoint(position);

            if (isLocalPostion) transform.localPosition = pos;
            else transform.position = pos;
        }

        public enum PositionType { Custom, StartPosition, FromOtherTransform }
    }
}