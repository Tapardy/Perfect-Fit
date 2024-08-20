using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static PauseMenu Instance { get; private set; }
    public GameObject pauseMenuUI;

    private void Start()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
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
                Pause();
            }
        }
    }

    private void Resume()
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
