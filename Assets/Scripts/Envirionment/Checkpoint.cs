using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    float lastSaveTime = 0f;
    float saveCooldown = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time - lastSaveTime > saveCooldown)
            {
                LevelManager.instance.SetCurrentCheckPoint(transform);
                lastSaveTime = Time.time;
                SoundManager.instance.PlaySound("SFX_NYAA");
            }
        }
    }
}
