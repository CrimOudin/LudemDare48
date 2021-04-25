using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Clam : MonoBehaviour
{
    public float SnapCloseTime;
    public int Damage;

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
        if (collision.transform.CompareTag("Player"))
        {
            UiManager.Instance.SubtractHealth(Damage);
        }
    }
}
