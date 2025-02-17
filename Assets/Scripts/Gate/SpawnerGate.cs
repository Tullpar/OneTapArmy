using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerGate : MonoBehaviour
{
    public int count;
    public TextMeshProUGUI countText;

    private void OnEnable()
    {
        count = Random.Range(1, 4);
        countText.text = "+" + count.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SoldierBrain brain))
        {
            UnitSpawner.PlayerSpawner.SpawnUnit(brain, count);
            Destroy(gameObject);
        }
    }
}
