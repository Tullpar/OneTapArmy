using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    CastleBrain CastleBrain;

    public LevelManager LevelManager;

    

    public List<UnitUpgradeData> UnlockedUnitUpgrades = new List<UnitUpgradeData>();
    public FortressUpgradeData FortressUpgradeData;

    public List<UnitUpgradeDataSet> UnitDataSets = new List<UnitUpgradeDataSet>();
    public UpgradeDataSet FortressDataSet;

    public event Action<List<UpgradeData>> UpgradesSelectedEvent;
    public event Action<UpgradeData> UpgradeUnlockedEvent;

    private void Start()
    {
        CastleBrain = GetComponentInParent<CastleBrain>();
        UnlockUpgrade(FortressDataSet.UpgradeDatas[0]);
    }

    private void OnEnable()
    {
        CastleBrain = GetComponentInParent<CastleBrain>();

        if (CastleBrain.IsPlayer)
        {
            LevelManager.LevelUpEvent += OnLevelUpPlayer;
        }
        else
        {
            LevelManager.LevelUpEvent += OnLevelUpAI;
        }
    }

    private void OnDisable()
    {
        if (CastleBrain.IsPlayer)
        {
            LevelManager.LevelUpEvent -= OnLevelUpPlayer;
        }
        else
        {
            LevelManager.LevelUpEvent -= OnLevelUpAI;
        }
    }

    public void OnLevelUpAI()
    {
        int randomIndex = UnityEngine.Random.Range(0, 3);
        List<UpgradeData> selectedUpgrades = SelectUpgrades();
        UnlockUpgrade(selectedUpgrades[randomIndex]);
    }

    public void OnLevelUpPlayer()
    {
        List<UpgradeData> selectedUpgrades = SelectUpgrades();
        UpgradesSelectedEvent?.Invoke(selectedUpgrades);
    }



    public List<UpgradeData> SelectUpgrades()
    {
        List<UpgradeDataSet> availableUpgrades = FindAvailableUpgrades();

        List<UpgradeDataSet> selectedDataSets = new List<UpgradeDataSet>();
        for (int i = 0; i < 3; i++)
        {
            int index = UnityEngine.Random.Range(0, availableUpgrades.Count);
            selectedDataSets.Add(availableUpgrades[index]);
            availableUpgrades.RemoveAt(index);
        }

        List<UpgradeData> selectedDatas = new List<UpgradeData>();

        for (int i = 0; i < selectedDataSets.Count; i++)
        {
            if (selectedDataSets[i] is UnitUpgradeDataSet)
            {
                SoldierBrain.UnitType unitType = (selectedDataSets[i] as UnitUpgradeDataSet).UnitType;

                UnitUpgradeData currentUnitUpgrade = UnlockedUnitUpgrades.Find(x => x.UnitType == unitType);
                UpgradeData selectedData = null;
                if (currentUnitUpgrade != null)
                {
                    selectedData = selectedDataSets[i].UpgradeDatas[currentUnitUpgrade.UpgradeLevel];
                }
                else
                {
                    selectedData = selectedDataSets[i].UpgradeDatas[0];
                }
                selectedDatas.Add(selectedData);
            }
            else
            {
                UpgradeData selectedData = FortressDataSet.UpgradeDatas[FortressUpgradeData.UpgradeLevel];
                selectedDatas.Add(selectedData);
            }

        }

        return selectedDatas;

    }


    List<UpgradeDataSet> FindAvailableUpgrades()
    {
        List<UpgradeDataSet> AvailableUpgrades = new List<UpgradeDataSet>();

        if (!IsMaxLevel(FortressDataSet) && PlayerHasEnoughLevels(FortressDataSet))
        {
            AvailableUpgrades.Add(FortressDataSet);
        }
        for (int i = 0; i < UnitDataSets.Count; i++)
        {
            if (!IsMaxLevel(UnitDataSets[i]) && PlayerHasEnoughLevels(UnitDataSets[i]))
            {
                AvailableUpgrades.Add(UnitDataSets[i]);
            }
        }
        return AvailableUpgrades;
    }



    //bool PlayerHasEnoughLevels(UnitUpgradeDataSet dataSet)
    //{
    //    return dataSet.MinUpgradeLevel <= LevelManager.Level;
    //}
    bool PlayerHasEnoughLevels(UpgradeDataSet dataSet)
    {
        return dataSet.MinUpgradeLevel <= LevelManager.Level;
    }

    bool IsMaxLevel(UnitUpgradeDataSet dataSet)
    {
        return (UnlockedUnitUpgrades.Find(x => x.UnitType == dataSet.UnitType && x.UpgradeLevel == 5));        
    }

    bool IsMaxLevel(UpgradeDataSet dataSet)
    {
        return FortressUpgradeData.UpgradeLevel >= 5;
    }

    public void UnlockUpgrade(UpgradeData upgradeData)
    {
        if (upgradeData is UnitUpgradeData)
        {
            UnitUpgradeData unitUpgradeData = upgradeData as UnitUpgradeData;
            UnitUpgradeData currentData = UnlockedUnitUpgrades.Find(x => x.UnitType == unitUpgradeData.UnitType);
            if ((currentData == null))
            {
                UnlockedUnitUpgrades.Add(unitUpgradeData);
            }
            else
            {
                UnlockedUnitUpgrades.Remove(currentData);
                UnlockedUnitUpgrades.Add(unitUpgradeData);
            }
        }
        else if (upgradeData is FortressUpgradeData)
        {
            FortressUpgradeData fortressUpgradeData = upgradeData as FortressUpgradeData;
            FortressUpgradeData = fortressUpgradeData;
        }
        UpgradeUnlockedEvent?.Invoke(upgradeData);
    }
}