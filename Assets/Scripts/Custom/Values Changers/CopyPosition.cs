using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    public Transform toCopy;
    public bool copy = true;

    private void FixedUpdate()
    {
        if (copy)
            transform.position = toCopy.position;
    }
}
