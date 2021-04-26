using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public WorldManager manager;
    // Start is called before the first frame update
    void Awake()
    {
        //manager.Initialize();
        StartCoroutine(DelayThenFadeInThenWaitThenFadeOut());
    }

    private IEnumerator DelayThenFadeInThenWaitThenFadeOut()
    {
        yield return new WaitForSeconds(0.5f);
        yield return WorldManager.Instance.FadeScreen(true, null);
        yield return new WaitForSeconds(1f);
        yield return WorldManager.Instance.FadeScreen(false, () => { SceneManager.LoadScene(1); });
        yield break;
    }
}
