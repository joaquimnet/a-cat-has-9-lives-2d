using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] sprites;

    private void Awake()
    {
        var sprite = sprites[Mathf.FloorToInt(Random.Range(0f, sprites.Length))];
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
