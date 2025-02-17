using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform Target;

    public float _speed = 5f;

    public float _damage = 20f;

    SoldierBrain.UnitTeam team;
    Transform _shooter;


    public void Shoot(Health enemyHealth, float damage, SoldierBrain.UnitTeam attackerTeam, Transform shooter)
    {
        _shooter = shooter;
        team = attackerTeam;
        Target = enemyHealth.transform;
        _damage = damage;
        StartCoroutine(MoveCoroutine(enemyHealth));
    }

    IEnumerator MoveCoroutine(Health enemyHealth)
    {
        float t = 0;
        float startingDistance = Vector3.Distance(transform.position, Target.transform.position);

        float duration = startingDistance / _speed;
        Vector3 startPosition = transform.position;
        Vector3 lastPosition = transform.position;
        while (t <= 1)
        {
            t += Time.deltaTime / duration;
            if ((Target == null))
            {
                StopAllCoroutines();
                Destroy(gameObject);
            }
            transform.position = Vector3.Lerp(startPosition, Target.transform.position, t);
            transform.position = new Vector3(transform.position.x,startPosition.y + Mathf.Sin(Mathf.PI * t) * startingDistance / 5f, transform.position.z);

            Vector3 direction = transform.position - lastPosition;
            if (direction != Vector3.zero)
            {
                transform.forward = direction;
            }
            lastPosition = transform.position;

            yield return new WaitForEndOfFrame();
        }

        float damage = _damage;
        if (team != SoldierBrain.UnitTeam.Blue)
        {
            damage = 0.65f * _damage;
        }

        enemyHealth.TakeDamage(_damage,team,_shooter.transform);


        Destroy(gameObject);

    }

}
