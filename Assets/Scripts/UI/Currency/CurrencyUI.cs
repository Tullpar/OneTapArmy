using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI CurrencyText;
    void Start()
    {
        CurrencyText.text = CurrencyManager.Gold.ToString();
    }
}
