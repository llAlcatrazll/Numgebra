using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Assign the scene name in the Inspector

    [SerializeField] private int leveltransformIndex;
    private AsyncOperation preloadOperation;


    public void NavigateScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            if (sceneToLoad == "Level_Selector")
            {
                // call settargetlevel here and pass the leveltransformIndex to the levelloader arraindex = leveltransformindex
                // LevelLoader.Instance.SetTargetLevel(leveltransformIndex);
                LevelLoader.Instance.defaultlevelIndex = leveltransformIndex;
                Debug.Log($"Switching to scene {leveltransformIndex}");
            }
            StartCoroutine(LoadSceneAsync(sceneToLoad));
        }
        else
        {
            Debug.LogWarning("Scene name is empty! Assign a scene name in the Inspector.");
        }
    }

    public void BastaNavigateScene()
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    public IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
        Debug.Log("Scene loaded successfully!");
        LevelLoader.Instance.SetTargetLevel(leveltransformIndex);
        Debug.Log($"Switching to level {leveltransformIndex}");
    }

    // Navigate to the next scene automatically
    public void NavigateNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            string nextSceneName = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
            nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextSceneName); // Extract scene name
            StartCoroutine(LoadSceneAsync(nextSceneName));
        }
        else
        {
            Debug.LogWarning("No next scene found! Make sure all scenes are added in Build Settings.");
        }
    }

    // Preload a scene (manually assigned in Inspector)
    public void PreloadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            if (preloadOperation == null)
            {
                preloadOperation = SceneManager.LoadSceneAsync(sceneToLoad);
                preloadOperation.allowSceneActivation = false;
            }
        }
        else
        {
            Debug.LogWarning("Scene name is empty! Assign a scene name in the Inspector.");
        }
    }

    // Activate the preloaded scene
    public void ActivatePreloadedScene()
    {
        if (preloadOperation != null)
        {
            preloadOperation.allowSceneActivation = true;
        }
        else
        {
            Debug.LogWarning("No preloaded scene available to activate.");
        }
    }

    // Unload the current scene after transitioning
    public void UnloadCurrentScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
// Load a scene (manually assigned in Inspector)
// public void NavigateScene()
// {
//     if (!string.IsNullOrEmpty(sceneToLoad) && sceneToLoad != LevelSelector)
//     {
//         StartCoroutine(LoadSceneAsync(sceneToLoad));
//     }
//     if (sceneToLoad == LevelSelector)
//     {

//     }
//     else
//     {
//         Debug.LogWarning("Scene name is empty! Assign a scene name in the Inspector.");
//     }
// }
