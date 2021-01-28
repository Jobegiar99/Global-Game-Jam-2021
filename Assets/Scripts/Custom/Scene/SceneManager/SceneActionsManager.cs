using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneActionsManager : MonoBehaviour
{
    [HideInInspector]
    public static SceneActionsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); } else if (Instance != this) { Destroy(this); }
    }

    public void LoadScene(string name, System.Action onDone)
    {
        StartCoroutine(_LoadScene(name, onDone));
    }

    private IEnumerator _LoadScene(string name, System.Action onDone)
    {
        yield return null;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        onDone?.Invoke();
    }
}