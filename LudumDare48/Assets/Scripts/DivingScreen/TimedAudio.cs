using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAudio : MonoBehaviour
{
    public float interval;
    public float maxDelay;

    private float currentDelay;
    private float elapsed = 0;

    private void Awake()
    {
        SetCurrentDelay();
        elapsed = (interval + currentDelay) * (0.8f * UnityEngine.Random.value);
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed > interval + currentDelay)
        {
            GetComponent<AudioSource>().Play();
            elapsed = 0;
            SetCurrentDelay();
        }
    }

    private void SetCurrentDelay()
    {
        currentDelay = maxDelay * UnityEngine.Random.value;
    }
}
