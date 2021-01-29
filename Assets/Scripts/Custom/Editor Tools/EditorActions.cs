using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

namespace Utilities.EditorTools
{
    public static class EditorActions
    {
        public static GameObject CreateChildAsPrefab(this Transform parent, string name, GameObject prefab)
        {
            GameObject child = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            child.transform.SetParent(parent);
            child.transform.localPosition = Vector3.zero;
            child.transform.localEulerAngles = Vector3.zero;
            child.transform.localScale = Vector3.one;
            return child;
        }
    }
}

#endif