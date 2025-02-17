using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleColor : MonoBehaviour
{
    CastleBrain CastleBrain;

    public List<Renderer> Renderers = new List<Renderer>();

    public List<Material> Materials = new List<Material>();

    private void Start()
    {
        CastleBrain = GetComponentInParent<CastleBrain>();
        SetColor(CastleBrain.Team);
    }

    void SetColor(SoldierBrain.UnitTeam team)
    {
        for (int i = 0; i < Renderers.Count; i++)
        {
            Renderers[i].sharedMaterial = Materials[(int)team];
        }
    }
}
