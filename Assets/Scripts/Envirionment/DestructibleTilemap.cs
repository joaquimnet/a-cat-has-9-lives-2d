using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTilemap : MonoBehaviour
{
    Tilemap destructibleTilemap;

    private void Awake()
    {
        destructibleTilemap = GetComponent<Tilemap>();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bullet"))
    //    {
    //        Vector3 hitPosition = Vector3.zero;
    //        foreach(ContactPoint2D hit in collision.contacts)
    //        {
    //            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
    //            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
    //            destructibleTilemap.SetTile(destructibleTilemap.WorldToCell(hitPosition), null);
    //        }
    //    }
    //}

    public void DestroyTileAt(Vector2 hitPosition)
    {
        destructibleTilemap.SetTile(destructibleTilemap.WorldToCell(hitPosition), null);
    }
}
