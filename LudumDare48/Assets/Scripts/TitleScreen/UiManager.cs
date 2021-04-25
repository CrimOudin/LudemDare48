using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    public Text MoneyText;
    public Text HealthText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    internal void UpdateMoney(int value)
	{
        WorldManager.Instance.Dollars += value;
        MoneyText.text = WorldManager.Instance.Dollars.ToString();
	}


    internal void AddHealth(int value)
    {
        WorldManager.Instance.Health += Math.Min(WorldManager.Instance.Health + value, WorldManager.Instance.MaxHealth);
        HealthText.text = WorldManager.Instance.Health.ToString();
    }
    internal void SubtractHealth(int value)
    {
        WorldManager.Instance.Health -= value;
        HealthText.text = WorldManager.Instance.Health.ToString();
    }
}
