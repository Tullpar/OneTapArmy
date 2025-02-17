using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUI : MonoBehaviour
{
    SoldierBrain Brain;
    float height = 0.65f;
    private void Start()
    {
        height = transform.localPosition.y;
        Brain = GetComponentInParent<SoldierBrain>();
    }

    private void Update()
    {
        transform.position = Brain.transform.position + Vector3.up * height;
        transform.forward = -Camera.main.transform.forward;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

}
