using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public GameObject defaultButton;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    private void Start()
    {
        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton, null);
        }
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
    }

    public void OnStartBTNClicked()
    {
        //SceneManager.LoadScene(1);
        //GameMaster.instance.LoadNextLevel();

        defaultButton.SetActive(false);

        level1.SetActive(true);
        EventSystem.current.SetSelectedGameObject(level1, null);

        if (GameMaster.instance.unlockedLevels >= 2)
            level2.SetActive(true);
        if (GameMaster.instance.unlockedLevels >= 3)
            level3.SetActive(true);
    }

    public void LoadLevel(int level)
    {
        GameMaster.instance.LoadSpecificLevel(level);
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
