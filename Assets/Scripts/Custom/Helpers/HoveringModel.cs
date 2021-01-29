using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringModel : MonoBehaviour
{
    public float amplitude = 0.7f;
    public float frequency = 0.25f;
    private Vector3 startPos;
    private bool startWaving = false;
    private float startTime = 0f;    // Update is called once per frame

    public void Awake()
    {
        startPos = transform.localPosition;
        this.ExecuteLater(() =>
        {
            startWaving = true;
            startTime = Time.time;
        }, UnityEngine.Random.Range(0f, frequency * 10f));
    }

    private void Update()
    {
        float y = amplitude * Mathf.Sin((Time.time - startTime) * frequency * 2 * Mathf.PI);
        if (startWaving) transform.localPosition = startPos + Vector3.up * y;
    }
}