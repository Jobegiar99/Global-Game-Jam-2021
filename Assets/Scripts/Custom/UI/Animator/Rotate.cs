using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 10;
    public Vector3 axis = Vector3.up;

    private void Update()
    {
        transform.Rotate(axis * Time.deltaTime * speed);
    }
}