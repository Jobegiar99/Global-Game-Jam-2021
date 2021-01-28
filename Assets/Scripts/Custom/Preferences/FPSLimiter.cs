using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    public int limitTo = 30;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = limitTo;
    }
}