using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : Health
{
    CastleBrain Brain;
    private void Start()
    {
        Brain = GetComponent<CastleBrain>();
    }
    public override void Die(SoldierBrain.UnitTeam attackerTeam)
    {
        if (!IsDead)
        {
            Brain.Die();
        }
        base.Die(attackerTeam);
    }
}
