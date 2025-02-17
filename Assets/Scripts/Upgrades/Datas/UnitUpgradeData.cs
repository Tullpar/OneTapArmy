using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Upgrade Data",menuName = "Upgrades/Unit Upgrade Data")]
public class UnitUpgradeData : UpgradeData
{
    public SoldierBrain.UnitType UnitType;
    public Sprite NameSprite;
    public Sprite BackgroundSprite;
    public float SpawnTime = 7f;
    public SoldierData SoldierData;
}
