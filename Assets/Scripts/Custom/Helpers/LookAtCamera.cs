using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform target;
    public int forwardMultiplier = -1;

    // Use this for initialization
    private void Awake()
    {
        if (!target)
            target = Camera.main.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(target);
        transform.forward *= forwardMultiplier;
    }
}