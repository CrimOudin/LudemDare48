using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
	private void Awake()
	{
        StartCoroutine(WorldManager.Instance.FadeScreen(true, null));
    }
	public void Retry()
    {
        WorldManager.Instance.StartNewGame();
    }
    public void MainMenu()
    {
        StartCoroutine(WorldManager.Instance.FadeScreen(false, () => 
        { 
            SceneManager.LoadScene(1);
            Destroy(UiManager.Instance.gameObject);
            WorldManager.Instance.ResetPlayerValues();
        }));
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
