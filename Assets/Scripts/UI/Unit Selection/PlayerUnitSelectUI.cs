using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitSelectUI : MonoBehaviour
{
    public List<PlayerUnitSelectionCard> Cards = new List<PlayerUnitSelectionCard>();

    public UpgradeManager PlayerUpgradeManager;
    public SoldierManager PlayerSoldierManager;

    private void Update()
    {
        SetCards();
    }

    void SetCards()
    {
        for (int i = 0; i < Cards.Count - 1; i++)
        {
            UnitUpgradeData data = PlayerUpgradeManager.UnlockedUnitUpgrades.Find(x=> x.UnitType == Cards[i].UnitType);
            if (data != null)
            {
                Cards[i].gameObject.SetActive(true);
                Cards[i].SetCard(data);
            }
            else
            {
                Cards[i].gameObject.SetActive(false);
            }
        }
        Cards[Cards.Count - 1].SetCard(PlayerUpgradeManager.FortressUpgradeData);
    }


    public void Select(int index)
    {
        DeselectAll();
        Cards[index].Select();
        PlayerSoldierManager.SelectType(index);
    }

    public void SelectAll()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].Select();
        }
        PlayerSoldierManager.SelectAllTypes();
    }

    public void DeselectAll()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].Deselect();
        }
    }

}
