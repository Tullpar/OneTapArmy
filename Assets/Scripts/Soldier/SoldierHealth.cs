using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHealth : Health
{
    SoldierBrain Brain;

    private void OnEnable()
    {
        Brain = GetComponent<SoldierBrain>();
    }


    public override void TakeDamage(float damage, SoldierBrain.UnitTeam attackerTeam,Transform attackerTransform)
    {
        Brain.Target.SetTarget(attackerTransform);
        base.TakeDamage(damage, attackerTeam, attackerTransform);
    }



    public override void Die(SoldierBrain.UnitTeam attackerTeam)
    {
        if (!IsDead)
        {
            Brain.Die();
            Destroy(gameObject, 2f);
        }
        base.Die(attackerTeam);
        
    }

}
