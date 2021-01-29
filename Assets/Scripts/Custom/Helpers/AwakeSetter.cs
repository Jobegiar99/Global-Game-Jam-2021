using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AwakeSetter : MonoBehaviour
{
    public UnityEvent onAwake;
    public int frameDelay = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (frameDelay == 0)
            onAwake?.Invoke();
        else
            this.ExecuteNextFrame(() => onAwake?.Invoke(), frameDelay);
    }
}
