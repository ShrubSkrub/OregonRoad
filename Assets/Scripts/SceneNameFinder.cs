using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneNameFinder
{
    public static string GetNextSceneName()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            return GetSceneNameByBuildIndex(nextSceneIndex);
        }

        return string.Empty;
    }

    public static string GetSceneNameByBuildIndex(int buildIndex)
    {
        return GetSceneNameFromScenePath(SceneUtility.GetScenePathByBuildIndex(buildIndex));
    }

    private static string GetSceneNameFromScenePath(string scenePath)
    {
        //taking name of scene from it's path in assets
        int sceneNameStart = scenePath.LastIndexOf("/") + 1;
        int sceneNameEnd = scenePath.LastIndexOf(".");
        int sceneNameLength = sceneNameEnd - sceneNameStart;
        return scenePath.Substring(sceneNameStart, sceneNameLength);
    }
}
