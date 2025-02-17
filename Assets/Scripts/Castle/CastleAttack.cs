using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleAttack : MonoBehaviour
{
    public float TargetRadius = 3f;
    public CastleBrain CastleBrain;

    public float FireRate = 1.0f;
    public float Damage = 20f;


    public GameObject ArrowPrefab;
    public Transform ArrowSpawnPosition;


    float firingTimer;
    private void Update()
    {
        firingTimer -= Time.deltaTime;

        if (firingTimer <= 0)
        {
            Transform target = SearchForEnemies();
            if (target != null)
            {
                ShootArrow(target);
                firingTimer = 1 / FireRate;
            }
        }

    }

    void ShootArrow(Transform target)
    {
        GameObject arrowGO = Instantiate(ArrowPrefab, ArrowSpawnPosition.transform.position, ArrowSpawnPosition.rotation);
        Arrow arrow = arrowGO.GetComponent<Arrow>();


        Health targetHealth = target.GetComponent<Health>();

        arrow.Shoot(targetHealth, Damage, CastleBrain.Team, transform);
    }

    public Transform SearchForEnemies()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, TargetRadius, Vector3.up);

        SoldierBrain closestEnemy = null;
        float closestDistance = 9999f;

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent(out SoldierBrain enemy))
                {
                    if (enemy.Team != CastleBrain.Team)
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
        return null;
    }
}
