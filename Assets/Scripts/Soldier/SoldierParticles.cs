using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierParticles : MonoBehaviour
{
    public GameObject SelectedParticle;
    public void Select()
    {
        SelectedParticle.SetActive(true);
    }

    public void Deselect()
    {
        SelectedParticle.SetActive(false);
    }
}
