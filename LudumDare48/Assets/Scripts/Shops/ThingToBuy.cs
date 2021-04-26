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
                CostText.text = "Cost: " + (currentUpgradeLevel * 1000).ToString();
                bool canBuy = WorldManager.Instance.Dollars >= (currentUpgradeLevel * 1000);
                CostText.color = (canBuy ? Color.white : Color.red);
            }
        }
        else
        {
            bool canBuy = WorldManager.Instance.Dollars > 150;
            CostText.color = (canBuy ? Color.white : Color.red);
        }
    }

    public void TryToBuy()
    {
        if (currentUpgradeLevel >= 4)
            return;
        else if(myUpgrade == ShopUpgrade.Heal)
        {
            if(WorldManager.Instance.Dollars >= 150)
            {
                UiManager.Instance.UpdateMoney(-150);
                UiManager.Instance.AddHealth(50);
            }
        }
        else if(WorldManager.Instance.Dollars >= currentUpgradeLevel * 1000)
        {
            UiManager.Instance.UpdateMoney(-(currentUpgradeLevel * 1000));

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
