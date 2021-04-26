using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class ThingToBuy : MonoBehaviour
{
    public ShopUpgrade myUpgrade;
    public int currentUpgradeLevel = 1;

    //Probably not going to be used
    public SpriteRenderer myImage;
    public Text DescriptionText;
    public Text CostText;
    public Text LevelText;

    public void SetLevel()
    {
        if (myUpgrade != ShopUpgrade.Heal)
        {
            if (myUpgrade == ShopUpgrade.Hull)
                currentUpgradeLevel = WorldManager.Instance.hullUpgrade;
            else if (myUpgrade == ShopUpgrade.Fins)
                currentUpgradeLevel = WorldManager.Instance.finUpgrade;
            else if (myUpgrade == ShopUpgrade.Light)
                currentUpgradeLevel = WorldManager.Instance.lightUpgrade;

            if (currentUpgradeLevel == 4)
            {
                DescriptionText.text = Environment.NewLine + "Maxed out!!!";
                CostText.text = string.Empty;
                LevelText.text = string.Empty;
            }
            else if (myUpgrade != ShopUpgrade.Exit)
            {
                LevelText.text = currentUpgradeLevel.ToString();
                CostText.text = "Cost: " + WorldManager.Instance.upgradeCosts[currentUpgradeLevel].ToString();
                bool canBuy = WorldManager.Instance.Dollars >= WorldManager.Instance.upgradeCosts[currentUpgradeLevel];
                CostText.color = (canBuy ? Color.white : Color.red);
            }
        }
        else
        {
            bool canBuy = WorldManager.Instance.Dollars > 150;
            bool damaged = (WorldManager.Instance.Health < WorldManager.Instance.MaxHealth);
            CostText.color = (canBuy && damaged ? Color.white : Color.red);

            if (!damaged)
                DescriptionText.text = "At full health!";
            else
                DescriptionText.text = "Heal 50 HP";
        }
    }

    public void TryToBuy()
    {
        if (currentUpgradeLevel >= 4)
            return;
        else if(myUpgrade == ShopUpgrade.Heal && (WorldManager.Instance.Health < WorldManager.Instance.MaxHealth))
        {
            if(WorldManager.Instance.Dollars >= 150)
            {
                UiManager.Instance.UpdateMoney(-150);
                UiManager.Instance.AddHealth(50);
            }
        }
        else if(WorldManager.Instance.Dollars >= WorldManager.Instance.upgradeCosts[currentUpgradeLevel])
        {
            UiManager.Instance.UpdateMoney(-WorldManager.Instance.upgradeCosts[currentUpgradeLevel]);

            WorldManager.Instance.Upgrade(myUpgrade);

            SetLevel();
        }
    }
}

public enum ShopUpgrade
{
    Hull = 0,
    Fins = 1,
    Light = 2,
    Exit = 3,
    Heal = 4
}
