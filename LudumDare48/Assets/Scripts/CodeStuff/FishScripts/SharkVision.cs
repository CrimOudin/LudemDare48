using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkVision : MonoBehaviour
{
    private Shark _shark;

    void Start()
    {
        _shark = GetComponentInParent<Shark>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerSeen();
        }

    }

	private void PlayerSeen()
	{
        if (!_shark.IsDashing)
        {
            StartCoroutine(_shark.Dash());
        }
    }
}
