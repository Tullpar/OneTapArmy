using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Data Set")]
public class UpgradeDataSet : ScriptableObject
{
    public List<UpgradeData> UpgradeDatas = new List<UpgradeData>();
    public int MinUpgradeLevel = 0;
}
