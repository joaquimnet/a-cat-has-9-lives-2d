using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int level = 0;

    public Transform respawnPoint;

    [Header("UI")]
    public TMPro.TMP_Text fishcakeText;
    public Image hearts1;
    public Image hearts2;
    public Image hearts3;
    public Image hearts4;
    public Image hearts5;
    public Image hearts6;
    public Image hearts7;
    public Image hearts8;
    public Image hearts9;

    int fishcakes = 0;
    int hearts = 9;
    GameObject player;
    public int totalFoods;

    public Transform currentCheckpoint;

    public enum Mode
    {
        NORMAL,
        BLUE,
        BLACK
    }

    public Mode currentMode = Mode.NORMAL;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentCheckpoint = respawnPoint;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        totalFoods = Resources.FindObjectsOfTypeAll<CurrencyPickup>().Length - 1;
        if (level == 0)
        {
            throw new System.Exception("Please set the current level in the Level Manager.");
        }
    }

    private void Start()
    {

    }

    public int GiveFishcake(int amount)
    {
        fishcakes += amount;
        if (fishcakes > totalFoods)
        {
            fishcakeText.text = fishcakes + "/" + totalFoods + " (wow)";
        }
        else
        {
            fishcakeText.text = fishcakes + "/" + totalFoods;
        }
        return fishcakes;
    }

    public int GiveHearts(int amount)
    {
        hearts += amount;

        switch (hearts)
        {
            case 1:
                currentMode = Mode.BLUE;
                hearts2.enabled = false;
                break;
            case 2:
                currentMode = Mode.BLACK;
                hearts3.enabled = false;
                break;
            case 3:
                currentMode = Mode.NORMAL;
                hearts4.enabled = false;
                break;
            case 4:
                currentMode = Mode.BLUE;
                hearts5.enabled = false;
                break;
            case 5:
                currentMode = Mode.NORMAL;
                hearts6.enabled = false;
                break;
            case 6:
                currentMode = Mode.BLACK;
                hearts7.enabled = false;
                break;
            case 7:
                currentMode = Mode.NORMAL;
                hearts8.enabled = false;
                break;
            case 8:
                currentMode = Mode.BLUE;
                hearts9.enabled = false;
                break;
            case 9:
                // we good
                break;
        }

        return hearts;
    }

    public void SetCurrentCheckPoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void RespawnPlayer()
    {
        if (hearts <= 0)
        {
            GameMaster.instance.LoadSpecificLevel(4);
            return;
        }
        player.transform.position = LevelManager.instance.currentCheckpoint.position + Vector3.up;
    }

    public bool LevelIsFinished()
    {
        return fishcakes == totalFoods;
    }
}
