using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Deadzone : MonoBehaviour
{
    public Transform respawnPoint;
    public bool kill;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerState.instance.current == PlayerState.States.DYING)
            {
                return;
            }
            StartCoroutine(KillPlayer(collision.gameObject));
        }
    }

    IEnumerator KillPlayer(GameObject player)
    {
        PlayerState.instance.Set(PlayerState.States.DYING);
        LevelManager.instance.GiveHearts(-1);
        player.GetComponent<Animator>().SetTrigger("died");
        yield return new WaitForSeconds(0.8f);
        LevelManager.instance.RespawnPlayer();
        PlayerState.instance.Set(PlayerState.States.NORMAL);
    }
}
