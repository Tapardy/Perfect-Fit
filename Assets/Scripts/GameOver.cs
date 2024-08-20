using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
	public bool playerDied;
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player") && !playerDied)
		{
			Debug.LogWarning("Game Over");
			playerDied = true;
		}
	}
}