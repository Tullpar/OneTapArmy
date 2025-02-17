using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimation : MonoBehaviour
{
    public List<Animator> Animators = new List<Animator>();

    SoldierBrain _brain;

    private void OnEnable()
    {
        _brain = GetComponent<SoldierBrain>();
    }

    void Update()
    {
        foreach (Animator animator in Animators)
        {
            animator.SetBool("IsRunning", _brain.Movement.IsMoving());
        }
    }

    public void Die()
    {
        foreach (Animator animator in Animators)
        {
            animator.SetTrigger("Die");
        }
    }

    public void Attack()
    {
        foreach (Animator animator in Animators)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void Idle()
    {
        foreach (Animator animator in Animators)
        {
            animator.ResetTrigger("Attack");
        }
    }
}
