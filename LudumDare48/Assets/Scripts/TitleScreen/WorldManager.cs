using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
	public static WorldManager Instance;
	public GameObject FadeWindow;
	public Light LightSource;


    public List<EnemyDepthInfo> enemyInfo = new List<EnemyDepthInfo>();
    public List<EnemyDepthInfo> floorEnemies = new List<EnemyDepthInfo>();
    public List<TreasureDepthInfo> treasureInfo = new List<TreasureDepthInfo>();

	//Doesn't exist on initial load so have the player assign itself when it loads in.
	[HideInInspector]
	public Player player;
	[HideInInspector]
	public DivingScreen mainGame;

	[HideInInspector]
	public int hullUpgrade = 1;
	[HideInInspector]
	public int lightUpgrade = 1;
	[HideInInspector]
	public int finUpgrade = 1;

	internal int Dollars = 0;
	internal int MaxHealth = 100;
	internal int Health = 100;

	private int lowestFloor = 0;
	
	public List<LightInfo> lightLevelsPerUpgrade = new List<LightInfo>();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			if (transform.parent != null)
			{
				DontDestroyOnLoad(transform.parent.gameObject);
			}
			else
			{
				DontDestroyOnLoad(gameObject);
			}
			hullUpgrade = 1;
			lightUpgrade = 1;
			finUpgrade = 1;
			//Can delete.  For testing
			UiManager.Instance?.UpdateMoney(7250);
		}
	}

	internal IEnumerator PlayerHit()
	{
		player.Invulnerable = true;
		var flashCount = 10;
		for (int i = 0; i < flashCount; i++)
		{
			var color = player.SpriteRenderer.color;
			if(color.a == .2f)
			{
				color.a = 1f;
				player.SpriteRenderer.color = color;
			}
			else
			{
				color.a = .2f;
				player.SpriteRenderer.color = color;
			}
			yield return new WaitForSeconds(.2f);
		}
		player.Invulnerable = false;
	}

	internal void ResetPlayerValues()
	{
		Dollars = 0;
		MaxHealth = 100;
		Health = 100;
		hullUpgrade = 1;
		lightUpgrade = 1;
		finUpgrade = 1;
		UiManager.Instance?.UpdateMoney(0);
		UiManager.Instance?.AddHealth(0);
	}

	/*************************************************************************************************
     * Functions for the game over
     * **********************************************************************************************/
	internal void GameOver()
	{
		LightSource.SetLightLevel(-1);
		LightSource.canUpdate = false;
		StartCoroutine(Instance.FadeScreen(false, () => { SceneManager.LoadScene(5); }));
	}

	/*************************************************************************************************
     * Functions for the main game
     * **********************************************************************************************/
	public void SetPlayerSectionBounds(Vector2 xy, Vector2 size)
	{
		player.SetCurrentAreaRect(new Rect() { height = size.y, width = size.x, x = xy.x, y = xy.y });
	}

    public void GetNextZone(int upOrDown)
    {
        if (upOrDown < 0 && mainGame.currentZone == 0)
        {
            player.hasControl = false;
            LightSource.canUpdate = false;
            StartCoroutine(Instance.FadeScreen(false, () => { SceneManager.LoadScene(4); }));
        }
        else
        {
            mainGame.GetNextZone(upOrDown);
            int test1 = mainGame.currentZone;
            LightInfo test2 = lightLevelsPerUpgrade[lightUpgrade - 1];

            LightSource.SetLightLevel(lightLevelsPerUpgrade[lightUpgrade - 1].lightLevelPerFloor[mainGame.currentZone]);
        }
    }

	public void Upgrade(ShopUpgrade type)
	{
		if (type == ShopUpgrade.Hull)
		{
			hullUpgrade++;
			MaxHealth += 100;
			UiManager.Instance.AddHealth(100);
		}
		else if (type == ShopUpgrade.Fins)
			finUpgrade++;
		else if (type == ShopUpgrade.Light)
			lightUpgrade++;
	}


	/*************************************************************************************************
     * Functions for the Main Menu
     * **********************************************************************************************/
	public void StartNewGame()
	{
		StartCoroutine(Instance.FadeScreen(false, () => 
		{
			SceneManager.LoadScene(3);
			ResetPlayerValues();
		}));
	}



	/*************************************************************************************************
     * Functions for the Title Screen
     * **********************************************************************************************/

	//public void Initialize()
	//{
	//    if (Instance == null)
	//    {
	//        Instance = this;
	//        DontDestroyOnLoad(gameObject);
	//    }
	//}

	public IEnumerator FadeScreen(bool fadeIn, Action endAction)
	{
		float timer = 0;
		while (timer < 1f)
		{
			timer += Time.deltaTime;
			if (timer >= 1f)
				timer = 1f;

			float percentDone = 1 - ((1f - timer) / 1f);

			FadeWindow.SetColor(a: (fadeIn ? 1 - percentDone : percentDone));

			if (timer == 1f)
			{
				endAction?.Invoke();
				yield break;
			}
			yield return new WaitForEndOfFrame();
		}
	}



}

[Serializable]
public class EnemyDepthInfo
{
	public List<EnemyInfo> myEnemies;
}

[Serializable]
public class EnemyInfo
{
	public GameObject enemyPrefab;
	public float percentSpawnChance;
}

[Serializable]
public class TreasureDepthInfo
{
	public List<TreasureInfo> myTreasures;
}

[Serializable]
public class TreasureInfo
{
    public GameObject treasurePrefab;
    public float percentSpawnChance;
}

[Serializable]
public class LightInfo
{
    public List<int> lightLevelPerFloor;
}