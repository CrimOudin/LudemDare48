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

    public GameObject StartMarker;
    public GameObject DepthMarker;
    public Transform HighestPosition;
    public Transform LowestPosition;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (WorldManager.Instance != null)
            {
                SubtractHealth(0);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    internal void UpdateMoney(int value)
	{
        WorldManager.Instance.Dollars += value;
        MoneyText.text = WorldManager.Instance.Dollars.ToString();
	}


    internal void AddHealth(int value)
    {
        WorldManager.Instance.Health = Math.Min(WorldManager.Instance.Health + value, WorldManager.Instance.MaxHealth);
        HealthText.text = WorldManager.Instance.Health.ToString();
    }
    internal void SubtractHealth(int value)
    {
        if (value > 0)
        {
            WorldManager.Instance.Health -= value;
            HealthText.text = WorldManager.Instance.Health.ToString();
            if (WorldManager.Instance.Health <= 0)
            {
                WorldManager.Instance.GameOver();
            }
			else
			{
                StartCoroutine(WorldManager.Instance.PlayerHit());
            }
        }
    }


    internal void UpdateDepthMarker(float playerY, float lowestY)
    {
        float totalDistance = LowestPosition.position.y - StartMarker.transform.position.y;
        DepthMarker.transform.SetPosition(y: StartMarker.transform.position.y + (totalDistance * playerY / lowestY));
    }

    internal void UpdateStartMarker(int startLevel)
    {
        Vector2 diff = LowestPosition.position - HighestPosition.position;
        StartMarker.transform.SetPosition(y: HighestPosition.position.y + (diff.y * startLevel * 0.1f));
        DepthMarker.transform.SetPosition(y: StartMarker.transform.position.y);
    }
}
