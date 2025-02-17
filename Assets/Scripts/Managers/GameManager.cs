using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public UIManager UIManager;

    public bool IsLevelEnded;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Win();
        }
    }

    public void Win()
    {
        if (!IsLevelEnded)
        {
            IsLevelEnded = true;

            UIManager.Win();
            Time.timeScale = 0;
        }
    }

    public void Lose()
    {
        if (!IsLevelEnded)
        {
            IsLevelEnded = true;

            UIManager.Lose();
            Time.timeScale = 0;
        }
        
    }
}
