using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public float Speed;
    public float DashSpeed;
    public int Damage;
    public int Life;
    private RectTransform _rectTransform;
    private Animator _animator;
    private bool _isDead = false;
    internal bool IsDashing = false;
    private float _currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _currentSpeed = Speed;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isDead)
        {
            if (_rectTransform.localScale.x < 0)
            {
                //move Left
                transform.position = new Vector3(transform.position.x - _currentSpeed, transform.position.y);
            }
            else
            {
                //move Right
                transform.position = new Vector3(transform.position.x + _currentSpeed, transform.position.y);
            }
        }
		else
        {
            //death
            transform.position = new Vector3(transform.position.x, transform.position.y - 1);
        }
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (!_isDead)
        {
            if (IsDashing)
            {
                _animator.speed = .5f;
                IsDashing = false;
                _currentSpeed = Speed;
            }
            if (collision.transform.CompareTag("Player"))
            {
                PlayerHit();
            }
            _rectTransform.localScale = new Vector3(_rectTransform.localScale.x * -1, _rectTransform.localScale.y, 1);
        }
    }

    private void PlayerHit()
	{
        UiManager.Instance.SubtractHealth(Damage);
        Life--;
        if (Life <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        _isDead = true;
        _rectTransform.localScale = new Vector3(_rectTransform.localScale.x, _rectTransform.localScale.y * -1, 1);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    internal IEnumerator Dash()
    {
        IsDashing = true;
        _currentSpeed = 0;
        _animator.speed = 2;
        yield return new WaitForSeconds(.5f);
        _currentSpeed = DashSpeed;
    }
}
