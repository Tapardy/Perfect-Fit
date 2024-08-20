using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance { get; private set; }
    public PlayerMovement player;
    public EventSystem eventSystem;
    public GameObject retryButton;
    public GameObject gameOverUI;
    public AudioSource music;
    private AudioSource audio;
    public static bool gameIsPausedDeath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audio = music.GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            eventSystem.SetSelectedGameObject(retryButton);
            gameIsPausedDeath = true;
            player.gameObject.SetActive(false);
            audio.Stop();
            Time.timeScale = 0f;
            
        }
    }
}
