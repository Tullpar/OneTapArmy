using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradeCard : MonoBehaviour
{
    UnitUpgradeData _upgradeData;

    public List<GameObject> LevelStars = new List<GameObject>();

    public Image Icon;
    public Image NameText;
    public Image Background;

    public GameObject DamageText;
    public GameObject HealthText;
    public GameObject SpeedText;

    public Image DamageValueText;
    public Image HealthValueText;
    public Image SpeedValueText;


    public List<Sprite> DamageValueSprites = new List<Sprite>();
    public List<Sprite> HealthValueSprites = new List<Sprite>();
    public List<Sprite> SpeedValueSprites = new List<Sprite>();

    UpgradePanelUI upgradePanelUI;

    public void Initialize(UnitUpgradeData upgradeData,UpgradePanelUI panelUI)
    {
        upgradePanelUI = panelUI;

        _upgradeData = upgradeData;

        SetCard(upgradeData);
        SetStars(upgradeData.UpgradeLevel -1);
        SetValueSprites(upgradeData.UpgradeLevel - 1);
    }

    void SetCard(UnitUpgradeData upgradeData)
    {
        Icon.sprite = upgradeData.Icon;
        NameText.sprite = upgradeData.NameSprite;
        NameText.SetNativeSize();
        Background.sprite = upgradeData.BackgroundSprite;
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
        if (level == 0)
        {
            DamageText.SetActive(false);
            HealthText.SetActive(false);
            SpeedText.SetActive(false);
            
            DamageValueText.gameObject.SetActive(false);
            HealthValueText.gameObject.SetActive(false);
            SpeedValueText.gameObject.SetActive(false);

        }

        DamageValueText.sprite = DamageValueSprites[level];
        HealthValueText.sprite = HealthValueSprites[level];
        SpeedValueText.sprite = SpeedValueSprites[level];
    }

    public void Select()
    {
        upgradePanelUI.SelectUpgrade(_upgradeData);
    }
}
