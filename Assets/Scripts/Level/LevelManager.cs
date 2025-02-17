using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int Level = 0;
    public int CurrentEXP = 0;
    public int LevelUpEXP = 0;

    public event Action EXPGainedEvent;
    public event Action LevelUpEvent;

    private void Start()
    {
        LevelUp();
        GainEXP(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GainEXP(10);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LevelUp();
        }

    }

    public void GainEXP(int amount)
    {
        CurrentEXP += amount;
        if (CurrentEXP >= LevelUpEXP)
        {
            LevelUp();
        }

        EXPGainedEvent?.Invoke();
    }

    public void LevelUp()
    {
        Level++;
        CurrentEXP -= LevelUpEXP;
        LevelUpEXP = Level * 40;

        LevelUpEvent?.Invoke();
    }


}
