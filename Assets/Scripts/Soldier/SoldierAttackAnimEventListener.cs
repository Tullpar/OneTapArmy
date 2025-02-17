using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackAnimEventListener : MonoBehaviour
{
    public SoldierAttack SoldierAttack;
    public void ListenAttackAnimationEvent()
    {
        SoldierAttack.Attack();
    }
}
