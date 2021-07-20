using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class VictoryGate : MonoBehaviour
{
    public bool playWinSound = true;
    public GameObject visual;

    bool isOver = false;

    void Update()
    {
        if (isOver)
        {
            return;
        }

        if (LevelManager.instance.LevelIsFinished())
        {
            isOver = true;
            visual.SetActive(false);
        }
    }

    private void WinGame()
    {
        SoundManager.instance.PlaySound("SFX_WIN");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            WinGame();
            GameMaster.instance.UnlockLevel(LevelManager.instance.level + 1);
        }
    }
}
