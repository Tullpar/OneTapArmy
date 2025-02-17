using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBrain : MonoBehaviour
{
    public UpgradeManager UpgradeManager;

    CastleHealth CastleHealth;

    public bool IsPlayer;

    public SoldierBrain.UnitTeam Team;
    public bool IsDead;

    private void OnEnable()
    {
        CastleHealth = GetComponent<CastleHealth>();

        UpgradeManager.UpgradeUnlockedEvent += ListenCastleUpgrade;
    }
    private void OnDisable()
    {
        UpgradeManager.UpgradeUnlockedEvent -= ListenCastleUpgrade;
    }


    private void Start()
    {
        CastleHealth = GetComponent<CastleHealth>();
    }

    void ListenCastleUpgrade(UpgradeData upgradeData)
    {
        if (upgradeData is FortressUpgradeData)
        {
            FortressUpgradeData fortressUpgradeData = upgradeData as FortressUpgradeData;
            CastleHealth.SetMaxHealth(fortressUpgradeData.Health);
        }
    }

    public void Die()
    {
        if (!IsDead)
        {
            IsDead = true;
        }
        gameObject.SetActive(false);
    }
}
