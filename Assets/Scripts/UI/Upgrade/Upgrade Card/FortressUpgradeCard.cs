using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortressUpgradeCard : MonoBehaviour
{
    FortressUpgradeData _upgradeData;

    public List<GameObject> LevelStars = new List<GameObject>();

    public Image Icon;

    public GameObject HealthText;
    public GameObject SpawnRateText;

    public GameObject AddFortressText;

    public Image HealthValueText;
    public Image SpawnRateValueText;



    public List<Sprite> HealthValueSprites = new List<Sprite>();
    public List<Sprite> SpawnRateValueSprites = new List<Sprite>();

    UpgradePanelUI upgradePanelUI;

    public void Initialize(FortressUpgradeData upgradeData,UpgradePanelUI panelUI)
    {
        upgradePanelUI = panelUI;
        _upgradeData = upgradeData;

        Icon.sprite = upgradeData.Icon;
        SetStars(upgradeData.UpgradeLevel -1);
        SetValueSprites(upgradeData.UpgradeLevel - 1);
    }


    void SetStars(int level)
    {
        for (int i = 0; i < LevelStars.Count; i++)
        {
            if (i <= level)
            {
                LevelStars[i].SetActive(true);
            }
            else
            {
                LevelStars[i].SetActive(false);
            }
        }
    }

    void SetValueSprites(int level)
    {
        if (level == 4)
        {
            HealthText.SetActive(false);
            SpawnRateText.SetActive(false);

            HealthValueText.gameObject.SetActive(false);
            SpawnRateValueText.gameObject.SetActive(false);

            AddFortressText.SetActive(true);
        }

        HealthValueText.sprite = HealthValueSprites[level];
        SpawnRateValueText.sprite = SpawnRateValueSprites[level];


    }

    public void Select()
    {
        upgradePanelUI.SelectUpgrade(_upgradeData);   
    }
}
