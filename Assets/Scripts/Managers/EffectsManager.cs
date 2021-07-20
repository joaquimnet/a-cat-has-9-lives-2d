using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectsManager : MonoBehaviour
{
  public Particle[] particles;

  public static EffectsManager instance;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);
  }

  public void SpawnParticle(Particle.Type particleType, Vector3 where)
  {
    Particle particle = Array.Find(particles, arr => arr.particleType.Equals(particleType));
    if (particle == null && !Particle.Type.NONE.Equals(particleType))
    {
      Debug.LogWarning("Tried to spawn particle '" + particleType + "' but it doesn't exist.");
      return;
    }
    Instantiate(particle.particle, where, Quaternion.identity);
  }

  public void ChangeBloom(Color color)
  {
    var profile = GameObject.FindObjectOfType<Volume>().profile;

    if (profile.TryGet<Bloom>(out Bloom bloom))
    {
      bloom.tint.value = color;
    }
  }
}
