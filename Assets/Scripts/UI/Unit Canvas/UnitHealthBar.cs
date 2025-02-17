using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthBar : MonoBehaviour
{
    public SoldierHealth Health;

    public Image healthBar;

    private void Start()
    {
        Health = GetComponentInParent<SoldierHealth>();
        healthBar.color = TeamColors[(int)GetComponentInParent<SoldierBrain>().Team];
    }

    private void Update()
    {
        healthBar.fillAmount = Health.CurrentHealth / Health.MaxHealth;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }


    public List<Color> TeamColors = new List<Color>();

}
