using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    [Header("Save Data")]
    public int unlockedLevels;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);

        LoadGameData(SaveSystem.Load());
    }

    private void Start()
    {
        Debug.Log(Application.productName + " " + Application.version);
    }

    public void SaveGameData()
    {
        SaveSystem.Save(new GameSaveData(unlockedLevels));
    }

    public void LoadGameData(GameSaveData saveData)
    {
        if (saveData != null)
        {
            unlockedLevels = saveData.unlockedLevels;
        }
        else
        {
            unlockedLevels = 1;
        }
        SaveGameData();
    }

    public void LoadNextLevel(bool skipAnimation = false)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1, skipAnimation));
    }

    public void LoadSpecificLevel(int levelIndex, bool skipAnimation = false)
    {
        StartCoroutine(LoadLevel(levelIndex, skipAnimation));
    }

    IEnumerator LoadLevel(int levelIndex, bool skipAnimation = false)
    {
        //if (!skipAnimation)
        //{
        //    // Play crossfade animation
        //    transitionAnimator.SetTrigger("Start");
        //    // Wait until animation finsihes
        //    yield return new WaitForSeconds(transitionTime);
        //}

        SceneManager.LoadScene(levelIndex);
        yield return null;
    }

    public void UnlockLevel(int level)
    {
        if (level > unlockedLevels)
        {
            unlockedLevels += 1;
            SaveGameData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
