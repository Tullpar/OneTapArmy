using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner PlayerSpawner;

    CastleBrain CastleBrain;

    public SoldierManager SoldierManager;


    public Transform SpawnPoint;

    public Transform FreshSpawnPoint;

    public UpgradeManager UpgradeManager;

    public List<GameObject> UnitPrefabs = new List<GameObject>();



    public List<SoldierBrain> SpawnedSoldiers = new List<SoldierBrain>();

    public List<SoldierBrain> FreshSpawns = new List<SoldierBrain>();

    public List<bool> IsSpawnable = new List<bool>();




    public Dictionary<SoldierBrain.UnitType, List<SoldierBrain>> SoldiersByType = new Dictionary<SoldierBrain.UnitType, List<SoldierBrain>>();

    public Dictionary<SoldierBrain.UnitType, float> UnitSpawnTimers = new Dictionary<SoldierBrain.UnitType, float>();


    public event Action<float,SoldierBrain.UnitType> SpawnTimerUpdatedPercentageEvent;
    public event Action<SoldierBrain.UnitType> UnitSpawnedEvent;



    private void Awake()
    {
        for (int i = 0; i < SoldierBrain.UnitTypeCount; i++)
        {
            SoldierBrain.UnitType type = (SoldierBrain.UnitType)i;
            SoldiersByType.Add(type, new List<SoldierBrain>());
            UnitSpawnTimers.Add(type, -1f);
        }
    }

    private void Start()
    {
        CastleBrain = GetComponentInParent<CastleBrain>();

        if (CastleBrain.IsPlayer)
        {
            PlayerSpawner = this;
        }
    }

    private void Update()
    {
        ClearDeadSoldiers();

        for (int i = 0; i < SoldierBrain.UnitTypeCount; i++)
        {
            UnitUpgradeData unitData = UpgradeManager.UnlockedUnitUpgrades.Find(x => x.UnitType == (SoldierBrain.UnitType)i);
            if (unitData != null)
            {
                SoldierBrain.UnitType type = (SoldierBrain.UnitType)i;
                if (UnitSpawnTimers.ContainsKey(type))
                {
                    if (CastleBrain.IsPlayer)
                    {
                        UnitSpawnTimers[type] -= Time.deltaTime * CastleBrain.UpgradeManager.FortressUpgradeData.SpawnRate;
                    }
                    else
                    {
                        UnitSpawnTimers[type] -= Time.deltaTime * CastleBrain.UpgradeManager.FortressUpgradeData.SpawnRate * .85f;
                    }

                    if (UnitSpawnTimers[type] <= 0)
                    {
                        SpawnUnit(unitData);
                        //SpawnUnit(type);
                        UnitSpawnTimers[type] = unitData.SpawnTime;
                    }
                    float spawnPercentage = UnitSpawnTimers[type] / unitData.SpawnTime;
                    SpawnTimerUpdatedPercentageEvent?.Invoke(spawnPercentage, type);
                }
            }
            else
            {
                continue;
            }
            //if (!IsSpawnable[i])
            //{
            //    continue;
            //}
            //SoldierBrain.UnitType type = (SoldierBrain.UnitType)i;
            //if (UnitSpawnTimers.ContainsKey(type))
            //{
            //    UnitSpawnTimers[type] -= Time.deltaTime;
            //    if (UnitSpawnTimers[type] <= 0)
            //    {
            //        SpawnUnit(type);
            //        UnitSpawnTimers[type] = 7f;
            //    }
            //}
        }
    }


    public void SpawnUnit(SoldierBrain brain,int count)
    {
        for (int i = 0; i < count;i++)
        {
            GameObject spawnedUnit = Instantiate(brain.SoldierData.Prefab, brain.transform.position + brain.transform.right * 0.75f * i, brain.transform.rotation);
            SoldierBrain unitBrain = spawnedUnit.GetComponent<SoldierBrain>();
            SpawnedSoldiers.Add(unitBrain);
            SoldiersByType[brain.unitType].Add(unitBrain);
            unitBrain.SetTeam(CastleBrain.Team);
        }

    }

    public void SpawnUnit(UnitUpgradeData unitUpgradeData)
    {
        SpawnUnit(unitUpgradeData, SpawnPoint.position);
        //GameObject spawnedUnit = Instantiate(unitUpgradeData.SoldierData.Prefab, SpawnPoint.position, SpawnPoint.rotation);
        //SoldierBrain unitBrain = spawnedUnit.GetComponent<SoldierBrain>();
        //SpawnedSoldiers.Add(unitBrain);
        //SoldiersByType[unitUpgradeData.UnitType].Add(unitBrain);
        //unitBrain.SetTeam(Team);

        //UnitSpawnedEvent?.Invoke(unitUpgradeData.UnitType);

    }




    public void SpawnUnit(UnitUpgradeData unitUpgradeData, Vector3 transform)
    {
        GameObject spawnedUnit = Instantiate(unitUpgradeData.SoldierData.Prefab, SpawnPoint.position, SpawnPoint.rotation);
        SoldierBrain unitBrain = spawnedUnit.GetComponent<SoldierBrain>();
        
        SpawnedSoldiers.Add(unitBrain);
        FreshSpawns.Add(unitBrain);

        ArrangeFreshSpawns();

        SoldiersByType[unitUpgradeData.UnitType].Add(unitBrain);
        unitBrain.SetTeam(CastleBrain.Team);
        UnitSpawnedEvent?.Invoke(unitUpgradeData.UnitType);
    }


    public void SpawnUnit(SoldierBrain.UnitType type)
    {
        GameObject spawnedUnit = Instantiate(UnitPrefabs[(int)type], SpawnPoint.position, SpawnPoint.rotation);
        SoldierBrain unitBrain = spawnedUnit.GetComponent<SoldierBrain>();
        SpawnedSoldiers.Add(unitBrain);
        SoldiersByType[type].Add(unitBrain);
        unitBrain.SetTeam(CastleBrain.Team);

    }

    public void ArrangeFreshSpawns()
    {
        SoldierManager.SendSpawnPosition(FreshSpawns, FreshSpawnPoint.position);
    }

    public void RemoveFromFreshSpawn(SoldierBrain unit)
    {
        FreshSpawns.Remove(unit);
    }

    public List<SoldierBrain> GetUnitsByType(SoldierBrain.UnitType type)
    {
        return SoldiersByType[type];
    }

    void ClearDeadSoldiers()
    {
        SpawnedSoldiers.RemoveAll(x => x == null || x.IsDead);
        FreshSpawns.RemoveAll(x=> x == null || x.IsDead);

        for (int i = 0; i < SoldiersByType.Keys.Count; i++)
        {
            SoldiersByType[(SoldierBrain.UnitType)i].RemoveAll(x => x == null || x.IsDead);
        }

    }

}
