using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
	public GameObject Bubble;
	public float BubbleMinTime;
	public float BubbleMaxTime;
	public int MinBubbleAmount;
	public float MinSpeed;
	public float MaxSpeed;
	public float MinFrequency;
	public float MaxFrequency;
	public float MinMagnitude;
	public float MaxMagnitude;
	public float SpwanDelay;

	private float _nextSpawnTime = 0;
	private List<Bubble> _bubbles;

	private void Start()
	{
		_bubbles = new List<Bubble>();
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		SpawnBubbles();
		UpdateBubbles();
	}

	private void UpdateBubbles()
	{
		for (var i = 0; i < _bubbles.Count; i++)
		{
			var bubble = _bubbles[i];
			if (bubble.CheckSpawnTimer())
			{
				_bubbles.Remove(bubble);
				Destroy(bubble.gameObject);
				i--;
				continue;
			}
			else
			{

				bubble.MoveBubble();
			}
		}
	}

	private void SpawnBubbles()
	{
		if (Time.time >= _nextSpawnTime)
		{
			if (_bubbles.Count < MinBubbleAmount)
			{
				var bubble = Instantiate(Bubble, transform).GetComponent<Bubble>();
				var bubbleTime = Random.Range(BubbleMinTime, BubbleMaxTime);
				var bubbleSpeed = Random.Range(MinSpeed, MaxSpeed);
				var bubbleFrequencey = Random.Range(MinFrequency, MaxFrequency);
				var bubbleMagnitude = Random.Range(MinMagnitude, MaxMagnitude);
				bubble.InitBubble(Time.time + bubbleTime, bubbleSpeed, bubbleFrequencey, bubbleMagnitude);
				_bubbles.Add(bubble);
				_nextSpawnTime = Time.time + SpwanDelay;
			}
		}
	}
}
