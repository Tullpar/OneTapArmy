using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLevelList : MonoBehaviour
{
    public Transform Parent;
    public GameObject LevelUIPrefab;

    private void Start()
    {
        int levelCount =SceneManagement.CurrentLevel;
        int currentLevel = SceneManagement.CurrentLevel;
        levelCount = Mathf.Max(levelCount, 7);

        for (int i = 0; i < levelCount + 3; i++)
        {
            GameObject temp = Instantiate(LevelUIPrefab, Parent);
            MainMenuLevelUI ui = temp.GetComponent<MainMenuLevelUI>();
            ui.SetUI(i);
            if (i < currentLevel - 1)
            {
                ui.SetPassed();
            }
            else if (i == currentLevel - 1)
            {
                ui.SetActive();
            }
            else
            {
                ui.SetLocked();
            }
        }

    }

    public void Play()
    {
        SceneManagement.LoadCurrentLevel();
    }
}
