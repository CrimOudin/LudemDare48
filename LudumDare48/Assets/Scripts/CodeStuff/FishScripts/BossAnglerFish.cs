using System;
using System.Collections;
using UnityEngine;

public class BossAnglerFish : MonoBehaviour
{
	public float DetectionRadius;
	public int Damage;
	public GameObject DetectionPoint;
	private SpriteRenderer _spriteRenderer;
	private Animator _animator;
	public GameObject BaitPearl;
	private bool _isFinished = false;
	private bool disappering = false;
	private Sigmund1 _sigmund;


	public float ShowBeforeAttackSequenceTime;
	public float DistanceSideTargetMove;
	public float MoveSequenceSpeed;
	public float AdjustSequenceSpeed;
	public float DistanceAboveTargetMove;
	public float PauseBeforeSlamTime;
	public float PauseAfterSlamTime;
	public float SlamSequenceSpeed;
	public float DistanceSlamMove;
	private float _timeTillAttackSequence;
	private Vector3 _target;
	private Vector3 _slamTarget;
	private bool _beginShowSelfSequence = false;
	private bool _beginMoveSequence = false;
	private bool _moveSequenceStarted = false;
	private bool _beginSlamSequence = false;
	private bool _pauseStarted = false;
	private bool _startSlamAttack = false;
	private bool startingUp = true;
	private bool _startAttacking = false;
	private bool _sigmundDamageTaken = false;
	private bool _wasRight = false;
	private bool _startAdjustPositioning = false; 
	private bool _finishedAdjusting = false;
	private bool _afterSlamStarted = false;
	private bool _fightStarted = false;
	private bool _endSequenceActive = false;

	private void Start()
	{
		_sigmund = BaitPearl.GetComponent<Sigmund1>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		if (_endSequenceActive != true)
		{
			if (_startAttacking == false && _animator.GetBool("Appeared") == false && (_fightStarted == true || Vector3.Distance(WorldManager.Instance.player.transform.position, DetectionPoint.transform.position) <= DetectionRadius))
			{
				if (_spriteRenderer.enabled == false)
				{
					_spriteRenderer.enabled = true;
					_animator.ResetTrigger("Appear");
					_animator.SetTrigger("Appear");
					_animator.SetBool("Appeared", true);
					_beginShowSelfSequence = true;
					_timeTillAttackSequence = Time.time + ShowBeforeAttackSequenceTime;
					_fightStarted = true;
				}
			}
			else if (startingUp || _beginShowSelfSequence == true && _startAttacking == false && Time.time >= _timeTillAttackSequence)
			{
				if (startingUp == false)
				{
					_startAttacking = true;
				}
				//start dissapearing
				StartDisappearing();
			}
			else if (_beginMoveSequence == true && _moveSequenceStarted == false)
			{
				_moveSequenceStarted = true;
				//set target to move above player x distance above
				_target = new Vector3(WorldManager.Instance.player.transform.position.x, WorldManager.Instance.player.transform.position.y + DistanceAboveTargetMove, WorldManager.Instance.player.transform.position.z);
			}
			else if (_moveSequenceStarted == true && _beginSlamSequence == false)
			{
				if (BaitPearl.transform.position == _target)
				{
					_beginSlamSequence = true;
					_slamTarget = new Vector3(BaitPearl.transform.position.x, BaitPearl.transform.position.y - DistanceSlamMove, BaitPearl.transform.position.z);
				}
				BaitPearl.transform.position = Vector3.MoveTowards(BaitPearl.transform.position, _target, MoveSequenceSpeed * Time.deltaTime);
			}
			else if (_beginSlamSequence == true && _startSlamAttack == false && _pauseStarted == false)
			{//pause
				_pauseStarted = true;
				StartCoroutine(PauseThenSlam());
			}
			else if (_startSlamAttack == true && _startAdjustPositioning == false && _afterSlamStarted == false)
			{
				//slam straight down set x distance.
				if (BaitPearl.transform.position == _slamTarget)
				{
					_afterSlamStarted = true;
					StartCoroutine(PauseAfterSlam());
				}
				BaitPearl.transform.position = Vector3.MoveTowards(BaitPearl.transform.position, _slamTarget, SlamSequenceSpeed * Time.deltaTime);
			}
			else if (_startAdjustPositioning == true && _finishedAdjusting == false)
			{
				if (BaitPearl.transform.position == _target)
				{
					BaitPearl.transform.localScale = new Vector3(BaitPearl.transform.localScale.x * -1, BaitPearl.transform.localScale.y, BaitPearl.transform.localScale.z);
					_finishedAdjusting = true;
				}
				BaitPearl.transform.position = Vector3.MoveTowards(BaitPearl.transform.position, _target, AdjustSequenceSpeed * Time.deltaTime);
			}
			else if (_finishedAdjusting)
			{
				ResetAttackSequence();
			}
		}
	}

	private void ResetAttackSequence()
	{
		_beginShowSelfSequence = false;
		_beginMoveSequence = false;
		_moveSequenceStarted = false;
		_beginSlamSequence = false;
		_pauseStarted = false;
		_startSlamAttack = false;
		_startAttacking = false;
		_sigmundDamageTaken = false;
		_startAdjustPositioning = false;
		_finishedAdjusting = false;
		_afterSlamStarted = false;
	}

	private IEnumerator PauseThenSlam()
	{
		yield return new WaitForSeconds(PauseBeforeSlamTime);
		_startSlamAttack = true;
		_pauseStarted = false;
	}
	private IEnumerator PauseAfterSlam()
	{
		yield return new WaitForSeconds(PauseAfterSlamTime);
		_startAdjustPositioning = true;
		if (_wasRight)
		{
			_wasRight = false;
			//target move to left of ship
			_target = new Vector3(WorldManager.Instance.player.transform.position.x - DistanceSideTargetMove, WorldManager.Instance.player.transform.position.y + 300, WorldManager.Instance.player.transform.position.z);
		}
		else
		{
			_wasRight = true;
			//target move to right of ship
			_target = new Vector3(WorldManager.Instance.player.transform.position.x + DistanceSideTargetMove, WorldManager.Instance.player.transform.position.y + 300, WorldManager.Instance.player.transform.position.z);
		}
		_pauseStarted = false;
	}

	internal void BossEndSequence()
	{
		_endSequenceActive = true;
		StartDisappearing();
	}

	private void StartDisappearing()
	{
		_animator.SetBool("Appeared", false);
		_animator.ResetTrigger("Appear");
		disappering = true;
		_animator.ResetTrigger("Disappear");
		_animator.SetTrigger("Disappear");
	}

	internal void FinishDisappearing()
	{
		_animator.ResetTrigger("Disappear");
		disappering = false;
		_spriteRenderer.enabled = false;
		if (startingUp == false)
		{
			_beginMoveSequence = true;
		}
		startingUp = false;
		if(_endSequenceActive)
		{
			Destroy(transform.parent.gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//possibly have bite when close and appeared.
		if (!_isFinished && WorldManager.Instance.player.Invulnerable == false)
		{
			if (collision.transform.CompareTag("Player"))
			{
				_animator.SetBool("Bite", true);
				UiManager.Instance.SubtractHealth(Damage);
			}
		}
	}
	internal void SigmundSlamTrigger(Collider2D collision)
	{
		if (!_isFinished && WorldManager.Instance.player.Invulnerable == false)
		{
			if (collision.transform.CompareTag("Player"))
			{
				if (_startSlamAttack == true && _afterSlamStarted == false)
				{
					_afterSlamStarted = true;
					StartCoroutine(PauseAfterSlam());
				}
				_slamTarget = _sigmund.transform.position;
				UiManager.Instance.SubtractHealth(Damage);
			}
			else
			{
				if (_sigmundDamageTaken == false)
				{
					if (_startSlamAttack == true && _afterSlamStarted == false)
					{
						_afterSlamStarted = true;
						StartCoroutine(PauseAfterSlam());
					}
					_sigmund.Damage();
					_slamTarget = _sigmund.transform.position;
					_sigmundDamageTaken = true;
				}
			}
		}
	}

	internal void Finish()
	{
		Destroy(gameObject);
	}
}
