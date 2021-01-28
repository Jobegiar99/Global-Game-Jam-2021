using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class KeepAsLastChild : MonoBehaviour
    {
        public Transform child;

        private void OnTransformChildrenChanged()
        {
            if (child.GetSiblingIndex() != transform.childCount - 1)
                child.SetSiblingIndex(transform.childCount - 1);
        }
    }
}