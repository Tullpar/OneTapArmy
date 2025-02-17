using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthBarUI : MonoBehaviour
{
    Health CastleHealth;

    public Image HealthBar;
    public TextMeshProUGUI HealthText;

    private void Start()
    {
        CastleHealth = GetComponentInParent<Health>();
    }

    private void Update()
    {
        HealthBar.fillAmount = CastleHealth.CurrentHealth / CastleHealth.MaxHealth;
        HealthText.text = CastleHealth.CurrentHealth.ToString();
    }
}
