using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFish : MonoBehaviour
{
    public float Speed;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_spriteRenderer.flipX)
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
            _spriteRenderer.flipX = !_spriteRenderer.flipX;

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
            _spriteRenderer.flipX = !_spriteRenderer.flipX;

        }
	}
}
