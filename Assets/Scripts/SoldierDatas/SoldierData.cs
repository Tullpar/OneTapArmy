using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soldier Data",menuName = "Soldier Datas")]
public class SoldierData : ScriptableObject
{
    public GameObject Prefab;

    public float Health = 100f;
    public float Damage = 20f;
    public float MoveSpeed = 3.5f;

    public float SpawnTime = 3f;
}
