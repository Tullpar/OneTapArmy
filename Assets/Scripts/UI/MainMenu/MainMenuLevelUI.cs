using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLevelUI : MonoBehaviour
{
    public Color ActiveColor;
    public Color LockedColor;
    public Color PassedColor;

    public Image Img;
    public TextMeshProUGUI LevelText;
    public void SetUI(int index)
    {
        index = index + 1;
        LevelText.text = index.ToString();
    }

    public void SetPassed()
    {
        Img.color = PassedColor;
    }

    public void SetLocked()
    {
        Img.color = LockedColor;
    }

    public void SetActive()
    {
        Img.color = ActiveColor;
    }
}
