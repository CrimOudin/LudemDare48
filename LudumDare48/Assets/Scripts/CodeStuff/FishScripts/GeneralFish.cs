using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFish : MonoBehaviour
{
    public float Speed;
    public int Damage;
    public int Life;
    private RectTransform _rectTransform;
    private bool _isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isDead)
        {
            if (_rectTransform.localScale.x < 0)
            {
                //move Left
                transform.position = new Vector3(transform.position.x - Speed, transform.position.y);
            }
            else
            {
                //move Right
                transform.position = new Vector3(transform.position.x + Speed, transform.position.y);
            }
        }
		else
        {
            //dead
            transform.position = new Vector3(transform.position.x, transform.position.y - 1);
        }
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (!_isDead)
        {
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
}
