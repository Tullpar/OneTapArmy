using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyManager
{
    public static int Gold
    {
        get
        {
            return PlayerPrefs.GetInt("Gold", 0);
        }
    }

    public static void AddGold(int amount)
    {
        PlayerPrefs.SetInt("Gold", Gold + amount);
    }
}
