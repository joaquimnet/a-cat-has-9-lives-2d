using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseGameUI;
    public GameObject gameplayUI;
    public GameObject defaultButton;
    public static bool IsGamePaused = false;

    bool cancelAxisIsPressed = false;

    private void Update()
    {
        if (Input.GetAxisRaw("Cancel") > .5)
        {
            if (!cancelAxisIsPressed)
            {
                if (IsGamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                    if (defaultButton != null)
                    {
                        EventSystem.current.SetSelectedGameObject(defaultButton, null);
                    }
                }
                cancelAxisIsPressed = true;
            }
        }
        else
        {
            cancelAxisIsPressed = false;
        }
    }

    public void Pause()
    {
        pauseGameUI.SetActive(true);
        gameplayUI.SetActive(false);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void Resume()
    {
        pauseGameUI.SetActive(false);
        gameplayUI.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void GoToTitle()
    {
        Time.timeScale = 1f;
        GameMaster.instance.LoadSpecificLevel(0, true);
        IsGamePaused = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
