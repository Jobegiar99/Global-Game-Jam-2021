using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class ForceCrashTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9)) Utils.ForceCrash(ForcedCrashCategory.Abort);
        if (Input.GetKeyDown(KeyCode.F10)) Utils.ForceCrash(ForcedCrashCategory.AccessViolation);
        if (Input.GetKeyDown(KeyCode.F11)) Utils.ForceCrash(ForcedCrashCategory.FatalError);
        if (Input.GetKeyDown(KeyCode.F12)) Utils.ForceCrash(ForcedCrashCategory.PureVirtualFunction);                    
    }
}
