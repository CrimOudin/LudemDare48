using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
	private float _timeDeath;
	private float _speed;
	private float _frequency;
	private float _magnitude;

	internal void InitBubble(float timeDeath, float speed, float frequency, float magnitude)
	{
		_timeDeath = timeDeath;
		_speed = speed;
		_frequency = frequency;
		_magnitude = magnitude;
	}

	internal void MoveBubble()
	{
		Vector3 newPosition = transform.position + (transform.up * Time.fixedDeltaTime * _speed + transform.right * Mathf.Sin(Time.time * _frequency) * _magnitude);
		transform.position = Vector3.MoveTowards(transform.position, newPosition, _speed * Time.fixedDeltaTime);
	}
	internal bool CheckSpawnTimer()
	{
		return Time.time >= _timeDeath;
	}
}
