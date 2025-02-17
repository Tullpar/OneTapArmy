using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierColor : MonoBehaviour
{
    public List<Renderer> Renderers = new List<Renderer>();

    public List<Material> Materials = new List<Material>();

    public Material DeadMaterial;

    public void SetColor(SoldierBrain.UnitTeam team)
    {
        for (int i = 0; i < Renderers.Count; i++)
        {
            Renderers[i].sharedMaterial = Materials[(int)team];
        }
    }

    public void Die()
    {
        for (int i = 0; i < Renderers.Count; i++)
        {
            Renderers[i].sharedMaterial = DeadMaterial;
        }
    }
}
