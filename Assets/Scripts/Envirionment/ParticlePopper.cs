using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ParticlePopper : MonoBehaviour
{
    public Particle.Type particle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EffectsManager.instance.SpawnParticle(particle, transform.position);
            Destroy(gameObject);
        }
    }
}
