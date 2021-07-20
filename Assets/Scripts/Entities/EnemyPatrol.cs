using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public LayerMask groundLayers;
    public Transform groundCheck;

    Rigidbody2D rb;
    bool isFacingRight = true;
    RaycastHit2D hit;

    public float killWaitTime = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
    }

    private void FixedUpdate()
    {
        if (hit.collider)
        {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        yield return new WaitForSeconds(killWaitTime);
        LevelManager.instance.RespawnPlayer();
        PlayerState.instance.Set(PlayerState.States.NORMAL);
    }
}
