using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    UnitUpgradeData _upgradeData;

    public List<GameObject> LevelStars = new List<GameObject>();

    public Image Icon;
    public Image DamageText;
    public Image HealthText;
    public Image SpeedText;

    public void Initialize(UnitUpgradeData upgradeData)
    {
        _upgradeData = upgradeData;

        Icon.sprite = upgradeData.Icon;
        SetStars(upgradeData.UpgradeLevel);
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



}
