using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

public class EditorSceneLoader : MonoBehaviour
{
    public string sceneName;

    private void Awake()
    {
        /*

        foreach (var item in SceneActionsManager.Instance.scenes.Values)
        {
            if (item.name == sceneName)
            {
                SceneActionsManager.Instance.openScenes.Add(item);
                break;
            }
        }
        */
    }
}

#endif