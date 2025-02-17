using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    public LevelManager PlayerLevelManager;

    public Image FillBar;
    public TextMeshProUGUI ExpText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ChapterText;


    private void OnEnable()
    {
        PlayerLevelManager.EXPGainedEvent += PlayerLevelManager_EXPGainedEvent;
        PlayerLevelManager.LevelUpEvent += PlayerLevelManager_LevelUpEvent;
    }


    private void OnDisable()
    {
        PlayerLevelManager.EXPGainedEvent -= PlayerLevelManager_EXPGainedEvent;
        PlayerLevelManager.LevelUpEvent -= PlayerLevelManager_LevelUpEvent;
    }

    private void Start()
    {
        ChapterText.text = "CHAPTER " +  (SceneManagement.CurrentLevel + 1).ToString();
    }

    private void PlayerLevelManager_EXPGainedEvent()
    {
        ExpText.text = "XP " + PlayerLevelManager.CurrentEXP + "/" + PlayerLevelManager.LevelUpEXP;
        FillBar.fillAmount = (float)PlayerLevelManager.CurrentEXP/ (float)PlayerLevelManager.LevelUpEXP;
    }
    private void PlayerLevelManager_LevelUpEvent()
    {
        LevelText.text = PlayerLevelManager.Level.ToString();
    }


}
