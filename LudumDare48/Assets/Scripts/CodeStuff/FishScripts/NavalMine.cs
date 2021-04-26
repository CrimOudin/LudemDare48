using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavalMine : MonoBehaviour
{
	public int Damage;
	public float Speed;
	public float DetectionRadius;
	public float ExplosionRadius;
	public float ExplosionDelay;
	private Animator _animator;
	private float _explosionTime;
	private bool exploded = false;

	// Start is called before the first frame update
	void Start()
	{
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{	
		
		if(_animator.GetBool("Moving"))
		{
			var target = WorldManager.Instance.player.transform.position;
			transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
			if(Time.time >= _explosionTime && !exploded)
			{
				_animator.SetTrigger("Explode");
				if (Vector3.Distance(WorldManager.Instance.player.transform.position, transform.position) <= ExplosionRadius)
				{
					UiManager.Instance.SubtractHealth(Damage);
					exploded = true;
				}
			}
		}
		else if (Vector3.Distance(WorldManager.Instance.player.transform.position, transform.position) <= DetectionRadius && !exploded)
		{
			_animator.SetBool("Moving", true);
			_explosionTime = Time.time + ExplosionDelay;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Player") && WorldManager.Instance.player.Invulnerable == false && !exploded)
		{
			_animator.SetTrigger("Explode");
			if (Vector3.Distance(WorldManager.Instance.player.transform.position, transform.position) <= ExplosionRadius)
			{
				UiManager.Instance.SubtractHealth(Damage);
				exploded = true;
			}
		}
	}

	internal void Finish()
	{
		
		Destroy(gameObject);
	}
}
