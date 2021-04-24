using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingScreen : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(WorldManager.Instance.FadeScreen(true, null));
    }
}
