using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventJointStretching : MonoBehaviour
{
    public float threshold = 0.1f;
    public Transform parent;
    private Vector3 lastValidPosition;

    private void Awake()
    {
        lastValidPosition = transform.localPosition;
        threshold = Vector3.Distance(transform.position, parent.position);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, parent.position) > threshold)
        {
            transform.localPosition = lastValidPosition;
        }
        else
        {
            lastValidPosition = transform.localPosition;
        }
    }
}