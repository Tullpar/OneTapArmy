using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagement
{
    public static int CurrentLevel
    {
        get
        {
            return PlayerPrefs.GetInt("CurrentLevel", 0);
        }
    }

    public static void LoadNextLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel",CurrentLevel + 1);

        Time.timeScale = 1;

        int sceneToLoad = (CurrentLevel % (SceneManager.sceneCountInBuildSettings - 1)) + 1;
        SceneManager.LoadScene(sceneToLoad);
    }

    public static void LoadCurrentLevel()
    {
        Time.timeScale = 1;
        int sceneToLoad = (CurrentLevel % (SceneManager.sceneCountInBuildSettings - 1)) + 1;
        SceneManager.LoadScene(sceneToLoad);
    }



}
