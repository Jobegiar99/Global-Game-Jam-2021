using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
