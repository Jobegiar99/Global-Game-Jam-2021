using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class PulsingAnimation : MonoBehaviour
    {
        public bool Enabled { get; set; } = true;
        public Transform transformToAnimate;
        public float speed = 0.25f;
        public float scale = 1.2f;
        private float t = 0;

        private void Update()
        {
            if (!Enabled)
            {
                if (1 - Mathf.Abs(transform.localScale.x) <= 0.02f)
                    transform.localScale = Vector3.one;
                else
                {
                    var s = transform.localScale.x;
                    s = Mathf.Lerp(s, 1, Time.deltaTime * speed * 10f);
                    transformToAnimate.localScale = Vector3.one * s;
                }
                return;
            }
            t += Time.deltaTime * speed * (2 * Mathf.PI);

            transformToAnimate.localScale = Vector3.one * (1 + Mathf.Sin(t) * (scale - 1));
        }
    }
}