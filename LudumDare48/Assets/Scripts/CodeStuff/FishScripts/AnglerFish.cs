using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFish : MonoBehaviour
{
	public float DetectionRadius;
    public int Damage;
	private SpriteRenderer _spriteRenderer;
	private Animator _animator;
	public GameObject BaitPearl;
	private bool _isFinished = false;
	private bool disappering = false;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		if(Vector3.Distance(WorldManager.Instance.player.transform.position, transform.position) <= DetectionRadius)
		{
			if (_spriteRenderer.enabled == false)
			{
				_spriteRenderer.enabled = true;
				BaitPearl.SetActive(false);
				_animator.ResetTrigger("Appear");
				_animator.SetTrigger("Appear");
				_animator.SetBool("Appeared", true);
			}
		}
		else
		{
			if (_spriteRenderer.enabled == true && disappering == false)
			{
				_animator.SetBool("Appeared", false);
				_animator.ResetTrigger("Appear");
				disappering = true;
				_animator.ResetTrigger("Disappear");
				_animator.SetTrigger("Disappear");
			}
		}
	}

	internal void FinishDisappearing()
	{
		_animator.ResetTrigger("Disappear");
		disappering = false;
		_spriteRenderer.enabled = false;
		BaitPearl.SetActive(true);
	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
		if (!_isFinished && WorldManager.Instance.player.Invulnerable == false)
		{
			if (collision.transform.CompareTag("Player"))
			{
				_animator.SetBool("Bite",true);
				UiManager.Instance.SubtractHealth(Damage);
			}
		}
    }

	internal void Finish()
	{
		Destroy(gameObject);
	}
}
