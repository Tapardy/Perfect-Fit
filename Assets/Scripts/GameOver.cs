using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
	[SerializeField] private GameObject gameOverUI;
	public bool playerDied;
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player") && !playerDied)
		{
			playerDied = true;
			DeathManager.Instance.GameOver();
		}
	}
}