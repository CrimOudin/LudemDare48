using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;
    public GameObject FadeWindow;

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
