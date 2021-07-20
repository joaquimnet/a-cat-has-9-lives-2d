using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 15f;
    public float bulletDamage = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
            return;

        DestructibleTilemap dt;
        collision.gameObject.TryGetComponent(out dt);
        if (dt)
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (var hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                dt.DestroyTileAt(hitPosition);
            }
        }
        Destroy(gameObject);
    }
}
