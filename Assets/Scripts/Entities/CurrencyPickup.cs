using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.instance.GiveFishcake(1);
            SoundManager.instance.PlaySound("SFX_LAUGH");
            EffectsManager.instance.SpawnParticle(Particle.Type.BLUE_ENERGY, transform.position);
            Destroy(gameObject);
        }
    }
}
