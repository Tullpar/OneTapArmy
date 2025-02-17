using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject LosePanel;

    public void Win()
    {
        WinPanel.SetActive(true);
    }

    public void Lose()
    {
        LosePanel.SetActive(true);
    }

    public void Continue()
    {
        CurrencyManager.AddGold(50);
        SceneManagement.LoadNextLevel();
    }

    public void Rewarded()
    {
        CurrencyManager.AddGold(100);
        SceneManagement.LoadNextLevel();
    }

    public void Retry()
    {
        SceneManagement.LoadCurrentLevel();
    }
}
