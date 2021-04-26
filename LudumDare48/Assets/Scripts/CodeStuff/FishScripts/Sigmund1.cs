using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigmund1 : MonoBehaviour
{

    private BossAnglerFish _boss;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private int _damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        _boss = GetComponentInChildren<BossAnglerFish>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }



    internal void Damage()
	{
        _damage++;
        _animator.SetInteger("Damage", _damage);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        _boss.SigmundSlamTrigger(collision);
	}

	internal void Explode()
	{
        _spriteRenderer.enabled = false;
        _boss.BossEndSequence();
	}
}
