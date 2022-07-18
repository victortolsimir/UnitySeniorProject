using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private class LoadingMonoBehaviour : MonoBehaviour { }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(string scene)
    {
        onLoaderCallback = () => 
        {
            var loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadAsync(scene));
            // SceneManager.LoadScene(scene);
            if (scene == "MainMenu")
            {
                GlobalControl.ManualDestroy();
                QuestController.ManualDestroy();
            }
        };

        //On scene change have cursor be default
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        SceneManager.LoadScene("Loading");
    }

    public static void LoadPreviousFloor()
    {
        LoadByIndex(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void LoadNextFloor()
    {
        LoadByIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private static void LoadByIndex(int sceneIndex)
    {
        onLoaderCallback = () =>
        {
            var loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadByIndexAsync(sceneIndex));
            // SceneManager.LoadScene(sceneIndex);
            if (sceneIndex == 0)
            {
                GlobalControl.ManualDestroy();
            }
        };

        SceneManager.LoadScene("Loading");
    }

    private static IEnumerator LoadAsync(string scene)
    {
        yield return null;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene);

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    private static IEnumerator LoadByIndexAsync(int sceneIndex)
    {
        yield return null;
        loadingAsyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return Mathf.Clamp01(loadingAsyncOperation.progress / 0.9f);
        }
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
