using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(WorldManager.Instance.FadeScreen(true, null));
    }

    public void StartGame()
    {
        WorldManager.Instance.StartNewGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
