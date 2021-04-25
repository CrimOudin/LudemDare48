using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFish : MonoBehaviour
{
    public float Speed;
    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_rectTransform.localScale.x < 0)
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
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.transform.CompareTag("Player"))
        {
            //TODO: player takes dmage.
        }
        else
        {
            _rectTransform.localScale = new Vector3(_rectTransform.localScale.x * -1, _rectTransform.localScale.y, 1);
        }
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.transform.CompareTag("Player"))
		{
            //TODO: player takes dmage.
		}
		else
        {
            _rectTransform.localScale = new Vector3(_rectTransform.localScale.x * -1, _rectTransform.localScale.y, 1);
        }
	}
}
