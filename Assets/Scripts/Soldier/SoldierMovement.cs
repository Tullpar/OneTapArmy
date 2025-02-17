using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SoldierMovement : MonoBehaviour
{

    SoldierBrain Brain;


    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public NavMeshObstacle obstacle;

    public float MinMoveDistance = 1f;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        Brain = GetComponent<SoldierBrain>();
        agent.speed = Brain.SoldierData.MoveSpeed;
    }

    private void Update()
    {
        Vector3 targetPosition = Brain.Target.GetTarget();

        if (Brain.Target.HasTarget() && Brain.Attack.IsTargetInRange())
        {
            Attack(targetPosition);
        }
        else if (IsOutOfReach(targetPosition))
        {
            MoveTo(targetPosition);
        }
        else
        {
            Idle();
        }
    }


    bool IsOutOfReach(Vector3 targetPosition)
    {
        //return true;
        return Vector3.Distance(targetPosition, transform.position) > 0.25f;
    }

    void MoveTo(Vector3 targetPosition)
    {
        agent.isStopped = false;
        agent.SetDestination(targetPosition);
    }

    void Attack(Vector3 targetPosition)
    {
        agent.isStopped = true;
        Brain.Animation.Attack();
        LookAt(targetPosition);
    }

    void LookAt(Vector3 targetPosition)
    {
        Vector3 targetDirection = targetPosition - transform.position;
        targetDirection.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, targetDirection, Time.deltaTime * 10f);
    }
    void Idle()
    {
        Brain.Animation.Idle();
    }

    public void Die()
    {
        agent.enabled = false;
        obstacle.enabled = false;
    }


    public bool IsMoving()
    {
        return agent.velocity.magnitude > .2f;
    }




}
