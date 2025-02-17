using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : SoldierAttack
{
    public Transform ArrowSpawnPosition;
    public GameObject ArrowPrefab;

    public override void Attack()
    {
        if (Brain.Target.HasTarget())
        {
            GameObject arrowGO = Instantiate(ArrowPrefab, ArrowSpawnPosition.transform.position, ArrowSpawnPosition.rotation);
            Arrow arrow = arrowGO.GetComponent<Arrow>();


            Health target = Brain.Target.targetTransform.GetComponent<Health>();

            arrow.Shoot(target,_damage,Brain.Team,transform);
        }
    }
}
