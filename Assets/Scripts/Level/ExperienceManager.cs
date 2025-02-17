using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance;
    void Awake()
    {
        instance = this; 
    }

    public List<LevelManager> Levelmanagers = new List<LevelManager>();


    public void GiveExp(SoldierBrain.UnitTeam team, int amount)
    {
        Levelmanagers[(int)team].GainEXP(amount);
    }
}
