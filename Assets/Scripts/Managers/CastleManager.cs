using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    public static CastleManager instance;
    private void Awake()
    {
        instance = this;
    }

    public CastleBrain PlayerCastle;
    public List<CastleBrain> EnemyCastles = new List<CastleBrain>();

    private void Update()
    {

        if (IsAllEnemiesDead())
        {
            GameManager.Instance.Win();
        }
        if (IsPlayerDead())
        {
            GameManager.Instance.Lose();
        }
    }

    public bool IsPlayerDead()
    {
        if (PlayerCastle != null)
        {
            if (PlayerCastle.IsDead)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsAllEnemiesDead()
    {
        for (int i = 0; i < EnemyCastles.Count; i++)
        {
            if (EnemyCastles[i] != null)
            {
                if (!EnemyCastles[i].IsDead && EnemyCastles[i].gameObject.activeSelf)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
