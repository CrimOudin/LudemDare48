using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFish : MonoBehaviour
{
	public float DetectionRadius;
    public int Damage;
	private SpriteRenderer _spriteRenderer;
	public GameObject BaitPearl;
	private bool _isFinished = false;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if(Vector3.Distance(WorldManager.Instance.player.transform.position, transform.position) <= DetectionRadius)
		{
			if (_spriteRenderer.enabled == false)
			{
				_spriteRenderer.enabled = true;
				BaitPearl.SetActive(false);
			}
		}
		else
		{
			if (_spriteRenderer.enabled == true)
			{
				_spriteRenderer.enabled = false;
				BaitPearl.SetActive(true);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
		if (!_isFinished)
		{
			if (collision.transform.CompareTag("Player"))
			{
				UiManager.Instance.SubtractHealth(Damage);
				StartCoroutine(Finish());
			}
		}
    }

	private IEnumerator Finish()
	{
		var fadeCount = 5;
		for(int i = 1; i < fadeCount; i++)
		{
			var color = _spriteRenderer.color;
			color.a = 1f / i;
			_spriteRenderer.color = color;
			yield return new WaitForSeconds(.2f);
		}
		Destroy(gameObject);
	}
}
