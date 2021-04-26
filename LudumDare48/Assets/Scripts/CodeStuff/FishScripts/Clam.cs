using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Clam : Enemy
{
    public float SnapCloseTime;
    public int Damage;
    public Pickup MyTreasure;

    private Animator[] _animators;
    private Collider2D _collider;
    private float _nextSnapTime;
    // Start is called before the first frame update
    void Start()
    {
        _animators = GetComponentsInChildren<Animator>().Where(x => !x.name.Contains("Pearl")).ToArray();
        _collider = GetComponent<Collider2D>();
        _nextSnapTime = Time.time + SnapCloseTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= _nextSnapTime)
		{
            _collider.enabled = true;
            _nextSnapTime = Time.time + SnapCloseTime;
            GetComponent<AudioSource>().Play();
            foreach (var animator in _animators)
            {
                animator.SetTrigger("SnapCloseT");
            }
            StartCoroutine(FinishSnapping());
        }
    }

    IEnumerator FinishSnapping()
	{
        yield return new WaitForSeconds(.2f);
        foreach (var animator in _animators)
        {
            animator.ResetTrigger("SnapCloseT");
        }
        _collider.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (WorldManager.Instance.player.Invulnerable == false)
        {
            if (collision.transform.CompareTag("Player"))
            {
                UiManager.Instance.SubtractHealth(Damage);
            }
        }
    }


    public override void SetLevel(int level)
    {
        MyTreasure.SetValue(level);

        if (level <= 3)
        {
            Damage = 25;
        }
        else if (level <= 6)
        {
            Damage = 40;
        }
    }
}
