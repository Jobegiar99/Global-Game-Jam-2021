using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.InputMngr
{
    public class HoveringInput : MonoBehaviour
    {
        //public GameObject previous;
        public GameObject current;

        public LayerMask hoverMask;
        public Events.GameObjectCompareEvent onHover;
        public Events.GameObjectEvent onSelect;
        public Events.GameObjectEvent onDrop;
        public Events.Event onCancel;

        public bool Active { get; set; } = true;

        private void Update()
        {
            if (!Active) return;

            Ray ray;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject candidate = GetHoverObject(hit.collider);
                if (candidate != null)
                {
                    if (candidate != current)
                    {
                        onHover?.Invoke(current, candidate);
                        current = candidate;
                    }
                }
                else
                {
                    onHover?.Invoke(current, null);
                    current = null;
                }
            }
            else
            {
                onHover?.Invoke(current, null);
                current = null;
            }

            if (Input.GetMouseButtonDown(0))
            {
                onSelect?.Invoke(current);
            }

            if (Input.GetMouseButtonDown(1))
            {
                onCancel?.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                onDrop?.Invoke(current);
            }
        }

        public void ForceUpdate()
        {
            current = null;
        }

        protected virtual GameObject GetHoverObject(Collider collider)
        {
            if (!collider) { return null; }
            if (!Helper.LayerIsInMask(collider.gameObject.layer, hoverMask)) { return null; }
            return collider.gameObject;
        }
    }
}