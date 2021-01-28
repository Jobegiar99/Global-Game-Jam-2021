using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class InstantiateOnStart : MonoBehaviour {
    public float delay = 0;
    public  GameObject prefab;
    private void Awake()
    {
        StartCoroutine(_Instantiate(delay));
        
    }

    IEnumerator _Instantiate(float t)
    {
        yield return new WaitForSeconds(t);
        Helper.CreateChild(transform, prefab.name,prefab);
    }
}
