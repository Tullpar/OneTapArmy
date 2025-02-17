using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelUI : MonoBehaviour
{
    public UpgradeManager upgradeManager;

    public GameObject Panel;
    public GameObject UnitCardPrefab;
    public GameObject FortressCardPrefab;

    public Transform CardsParent;

    List<GameObject> cards = new List<GameObject>();

    private void OnEnable()
    {
        upgradeManager.UpgradesSelectedEvent += SetPanel;
    }

    private void OnDisable()
    {
        upgradeManager.UpgradesSelectedEvent -= SetPanel;
    }

    public void SetPanel(List<UpgradeData> upgradeDatas)
    {
        Panel.SetActive(true);

        Time.timeScale = 0;

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            if (upgradeDatas[i] is FortressUpgradeData)
            {
                GameObject fortressCard = Instantiate(FortressCardPrefab, CardsParent);
                fortressCard.GetComponent<FortressUpgradeCard>().Initialize(upgradeDatas[i] as FortressUpgradeData,this);
                cards.Add(fortressCard);
            }
            else if (upgradeDatas[i] is UnitUpgradeData)
            {
                GameObject unitCard = Instantiate(UnitCardPrefab, CardsParent);
                unitCard.GetComponent<UnitUpgradeCard>().Initialize(upgradeDatas[i] as UnitUpgradeData,this);
                cards.Add(unitCard);
            }
        }
    }

    void ResetPanel()
    {
        Time.timeScale = 1;

        foreach (GameObject card in cards)
        {
            Destroy(card);
        }
        Panel.SetActive(false);
    }
    public void SelectUpgrade(UpgradeData upgradeData)
    {
        upgradeManager.UnlockUpgrade(upgradeData);
        ResetPanel();
    }
}
