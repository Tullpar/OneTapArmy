using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleUI : MonoBehaviour
{
    public List<GameObject> Stars = new List<GameObject>();
    CastleBrain Brain;

    private void Start()
    {
        Brain = GetComponentInParent<CastleBrain>();
    }

    private void Update()
    {
        SetStars(Brain.UpgradeManager.FortressUpgradeData.UpgradeLevel);
    }

    public void SetStars(int level)
    {
        for (int i = 0; i < Stars.Count; i++)
        {
            if (i < level)
            {
                Stars[i].SetActive(true);
            }
            else
            {
                Stars[i].SetActive(false);
            }
        }
    }
}
