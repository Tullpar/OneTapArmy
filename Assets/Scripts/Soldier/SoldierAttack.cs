using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{
    protected SoldierBrain Brain;

    protected float _damage = 20f;

    public float AttackRange = 1f;

    private void Start()
    {
        Brain = GetComponent<SoldierBrain>();
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void ListenAttackAnimationEvent()
    {
        Attack();
    }

    public virtual void Attack()
    {
        if (Brain.Target.HasTarget())
        {
            float damage = Brain.Team == SoldierBrain.UnitTeam.Blue ? _damage : _damage * 0.65f;
            Brain.Target.targetTransform.GetComponent<Health>().TakeDamage(damage,Brain.Team,transform);
        }
    }

    public bool IsTargetInRange()
    {
        if (!Brain.Target.HasTarget())
        {
            return false;
        }
        else
        {
            return Vector3.Distance(transform.position, Brain.Target.targetTransform.position) <= AttackRange;
        }
    }


}
