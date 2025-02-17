using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnitSelectionCard : MonoBehaviour
{
    public List<GameObject> Stars = new List<GameObject>();
    public Image Icon;
    public Image FillIcon;
    
    public Image SelectionImage;


    public SoldierBrain.UnitType UnitType;

    public UnitSpawner PlayerUnitSpawner;

    public bool IsSelectAllCard;
    private void OnEnable()
    {
        if (!IsSelectAllCard)
        {
            PlayerUnitSpawner.SpawnTimerUpdatedPercentageEvent += SetFillAmount;
        }
    }

    private void OnDisable()
    {
        if (!IsSelectAllCard)
        {
            PlayerUnitSpawner.SpawnTimerUpdatedPercentageEvent -= SetFillAmount;
        }
    }

    public void SetCard(UnitUpgradeData data)
    {
        Icon.sprite = data.Icon;
        FillIcon.sprite = data.Icon;

        for (int i = 0; i < Stars.Count; i++) 
        {
            if (i < data.UpgradeLevel)
            {
                Stars[i].SetActive(true);
            }
            else
            {
                Stars[i].SetActive(false);
            }   
        }
    }

    public void SetCard(FortressUpgradeData data)
    {
        Icon.sprite = data.Icon;
        FillIcon.sprite = data.Icon;

        for (int i = 0; i < Stars.Count; i++)
        {
            if (i < data.UpgradeLevel)
            {
                Stars[i].SetActive(true);
            }
            else
            {
                Stars[i].SetActive(false);
            }
        }
    }

    public void SetFillAmount(float percentage, SoldierBrain.UnitType unitType)
    {
        if (UnitType == unitType) 
        {
            FillIcon.fillAmount = 1 - percentage;
        } 
    }

    public void Select()
    {
        SelectionImage.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        SelectionImage.gameObject.SetActive(false);
    }
    

}
