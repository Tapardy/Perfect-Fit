using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    public Settings tutorialCheck;
    public static bool gameIsPaused = false;
    public static PauseMenu Instance { get; private set; }
    public GameObject pauseMenuUI;
    public GameObject tutorialUI;
    public EventSystem pauseSystem;
    public GameObject button;

    private void Start()
    {
        if (!tutorialCheck.tutorialWatched)
        {
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
        else
        {
            Resume();
        }
    }

    public void StartButtonPress()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                pauseSystem.SetSelectedGameObject(button);
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
