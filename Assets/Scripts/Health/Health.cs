using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public bool IsDead = false;
    public void SetMaxHealth(float maxHealth)
    {
        float diff = maxHealth - MaxHealth;
        MaxHealth = maxHealth;
        //CurrentHealth = maxHealth;
        Heal(diff);
    }
    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
    }
    public virtual void TakeDamage(float damage,SoldierBrain.UnitTeam attackerTeam,Transform attackerTransform)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        if (CurrentHealth <= 0)
        {
            Die(attackerTeam);
        }
    }
    public virtual void Die(SoldierBrain.UnitTeam attackerTeam)
    {
        if (!IsDead)
        {
            ExperienceManager.instance.GiveExp(attackerTeam, 5);
            IsDead = true;
            //Destroy(gameObject, 2f);
        }
    }
}
