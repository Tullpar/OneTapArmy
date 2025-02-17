using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData : ScriptableObject
{
    public Sprite Icon;
    public int UpgradeLevel;
    public UpgradeType _UpgradeType;
    
    public enum UpgradeType
    {
        Unit,
        Fortress
    }
}
