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

    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            eventSystem.SetSelectedGameObject(retryButton);
            gameIsPausedDeath = true;
            player.gameObject.SetActive(false);
            Time.timeScale = 0f;
            
        }
    }
}
