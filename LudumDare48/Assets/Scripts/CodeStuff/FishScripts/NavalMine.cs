using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavalMine : Enemy
{
	public int Damage;
	public float Speed;
	public float DetectionRadius;
	public float ExplosionRadius;
	public float ExplosionDelay;
	private Animator _animator;
	private float _explosionTime;
	private bool exploded = false;
	public AudioSource bang;
	private bool isAggroed = false;
	private bool explodedThisFrame = false;
	private bool isPlayingAggroSound = false;

	// Start is called before the first frame update
	void Start()
	{
		_animator = GetComponent<Animator>();
	}

    private void Update()
    {
		if (isAggroed && !isPlayingAggroSound)
		{
			isPlayingAggroSound = true;
			GetComponent<AudioSource>().Play();
		}
		if (explodedThisFrame)
		{
			explodedThisFrame = false;
			bang.Play();
			GetComponent<AudioSource>().Stop();
		}
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		if(_animator.GetBool("Moving"))
		{
            var target = WorldManager.Instance.player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            if (Time.time >= _explosionTime && !exploded)
			{
				_animator.SetTrigger("Explode");
				explodedThisFrame = true;
				exploded = true;
				if (Vector3.Distance(WorldManager.Instance.player.transform.position, transform.position) <= ExplosionRadius)
				{
					UiManager.Instance.SubtractHealth(Damage);
				}
			}
		}
		else if (Vector3.Distance(WorldManager.Instance.player.transform.position, transform.position) <= DetectionRadius && !exploded)
		{
			_animator.SetBool("Moving", true);
			isAggroed = true;
			_explosionTime = Time.time + ExplosionDelay;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Player") && WorldManager.Instance.player.Invulnerable == false && !exploded)
		{
			_animator.SetTrigger("Explode");
			explodedThisFrame = true;
			exploded = true;
			if (Vector3.Distance(WorldManager.Instance.player.transform.position, transform.position) <= ExplosionRadius)
			{
				UiManager.Instance.SubtractHealth(Damage);
			}
		}
	}

	internal void Finish()
	{
		
		Destroy(gameObject);
	}

	public override void SetLevel(int level)
    {
		if (level <= 3)
		{
			Damage = 15;
			Speed = 50;
			GetComponent<SpriteRenderer>().color = new Color(.5f, .9f, .5f);
		}
		else if (level <= 6)
		{
			Damage = 30;
			Speed = 125;
			GetComponent<SpriteRenderer>().color = new Color(1, .7f, 0f);
		}
    }
}
