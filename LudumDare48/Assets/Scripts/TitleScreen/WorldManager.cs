using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;
    public GameObject FadeWindow;

    public List<EnemyDepthInfo> enemyInfo = new List<EnemyDepthInfo>();
    public List<TreasureDepthInfo> treasureInfo = new List<TreasureDepthInfo>();

    //Doesn't exist on initial load so have the player assign itself when it loads in.
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public DivingScreen mainGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
        mainGame.GetNextZone(upOrDown);
    }


    /*************************************************************************************************
     * Functions for the Main Menu
     * **********************************************************************************************/
    public void StartNewGame()
    {
        StartCoroutine(Instance.FadeScreen(false, () => { SceneManager.LoadScene(3); }));
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