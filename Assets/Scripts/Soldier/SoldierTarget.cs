using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierTarget : MonoBehaviour
{
    SoldierBrain Brain;

    public Transform targetTransform;
    public Vector3 targetPosition;

    public float TargetRadius = 2f;

    void OnEnable()
    {
        Brain = GetComponent<SoldierBrain>();
        StartCoroutine(ScanEnemyCoroutine());
    }
    Transform ScanForEnemy()
    {
        if (targetTransform != null)
        {
            if (targetTransform.TryGetComponent(out SoldierHealth enemyHealth))
            {
                if (!enemyHealth.IsDead)
                {
                    return targetTransform;
                }
            }
        }

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, TargetRadius, Vector3.up);

        SoldierBrain closestEnemy = null;
        float closestDistance = 9999f;

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent(out SoldierBrain enemy))
                {
                    if (enemy.Team != Brain.Team)
                    {
                        if (!enemy.IsDead)
                        {
                            float distance = Vector3.Distance(transform.position, enemy.transform.position);
                            if (distance < closestDistance)
                            {
                                closestDistance = distance;
                                closestEnemy = enemy;
                            }

                            //return enemy.transform;
                        }
                    }
                }
            }
        }
        if (closestEnemy != null)
        {
            return closestEnemy.transform;
        }

        RaycastHit[] castleHits = Physics.SphereCastAll(transform.position, TargetRadius, Vector3.up, 20f, LayerMask.GetMask("Castle"));


        if (castleHits.Length > 0)
        {
            for (int i = 0; i < castleHits.Length; i++)
            {

                if (castleHits[i].collider.TryGetComponent(out CastleBrain castle))
                {
                    if (castle.Team != Brain.Team)
                    {
                        if (!castle.IsDead)
                        {
                            return castle.transform;
                        }
                    }
                }
            }
        }


        return null;
        
    }

    public bool HasTarget()
    {
        return targetTransform != null;
    }

    public void SetTarget(Transform target)
    {
        if (targetTransform == null)
        {
            targetTransform = target;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
    }

    public Vector3 GetTarget()
    {
        if (targetTransform)
        {
            return targetTransform.position;
        }
        else
        {
            return targetPosition;
        }
    }
    IEnumerator ScanEnemyCoroutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.3f);
            targetTransform = ScanForEnemy();
        }
    }
}
