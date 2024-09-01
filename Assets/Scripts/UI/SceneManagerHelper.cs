using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerHelper
{
    private static int previousSceneIndex = -1;

    public static void SetPreviousScene(int sceneIndex)
    {
        Debug.Log($"Setting previous scene index to: {sceneIndex}");
        previousSceneIndex = sceneIndex;
    }

    public static void LoadPreviousScene()
    {
        Debug.Log($"Loading previous scene index: {previousSceneIndex}");
        if (previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.LogWarning("No previous scene index set.");
        }
    }
}
