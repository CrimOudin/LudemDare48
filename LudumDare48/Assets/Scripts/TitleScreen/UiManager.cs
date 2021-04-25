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

    private Player _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance._player = GameObject.FindGameObjectsWithTag("Player").First()?.GetComponent<Player>();
            DontDestroyOnLoad(gameObject);
        }
    }

    internal void UpdateMoney(int value)
	{
        _player.Dollars += value;
        MoneyText.text = _player.Dollars.ToString();
	}
}
