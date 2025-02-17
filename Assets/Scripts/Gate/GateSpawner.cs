using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    public GameObject GatePrefab;
    float SpawnTime;

    private void Start()
    {
        SpawnTime = Random.Range(15f, 30f);
    }

    private void Update()
    {
        SpawnTime -= Time.deltaTime;
        if (SpawnTime <= 0)
        {
            SpawnTime = Random.Range(30f, 60f);
            Vector3 spawnPositionMin = new Vector3(-3.35f, 0.9409993f, -6.21f);
            Vector3 spawnPositionMax = new Vector3(3.41f, 0.9409993f, -3.71f);
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnPositionMin.x, spawnPositionMax.x),
                Random.Range(spawnPositionMin.y, spawnPositionMax.y),
                Random.Range(spawnPositionMin.z, spawnPositionMax.z)
                );


            GameObject spawnedGate = Instantiate(GatePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
