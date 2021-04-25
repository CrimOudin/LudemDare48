using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class ThingToBuy : MonoBehaviour
{
    public ShopUpgrade myUpgrade;
    public int currentUpgradeLevel = 0;

    //Probably not going to be used
    public SpriteRenderer myImage;
    public Text DescriptionText;
    public Text CostText;

    public void SetLevel()
    {
        if (myUpgrade == ShopUpgrade.Hull)
            currentUpgradeLevel = WorldManager.Instance.hullUpgrade;
        else if (myUpgrade == ShopUpgrade.Fins)
            currentUpgradeLevel = WorldManager.Instance.finUpgrade;
        else if (myUpgrade == ShopUpgrade.Light)
            currentUpgradeLevel = WorldManager.Instance.lightUpgrade;

        if (currentUpgradeLevel == 3)
        {
            DescriptionText.text = Environment.NewLine + "Maxed out!!!";
            CostText.text = string.Empty;
        }
        else
        {
            CostText.text = "Cost: " + (currentUpgradeLevel * 1000).ToString();
        }
    }

    public int TryToBuy()
    {
        if (currentUpgradeLevel >= 3)
            return -1;
        else //if(WorldManager.Instance.)
            return currentUpgradeLevel * 1000;
    }
}

public enum ShopUpgrade
{
    Hull = 0,
    Fins = 1,
    Light = 2,
    Exit = 3
}
