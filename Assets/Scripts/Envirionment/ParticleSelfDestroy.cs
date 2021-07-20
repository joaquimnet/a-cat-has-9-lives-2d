using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestroy : MonoBehaviour
{
  private ParticleSystem ps;

  void Awake()
  {
    ps = GetComponent<ParticleSystem>();
  }

  // Update is called once per frame
  void Update()
  {
    if (!ps.IsAlive())
    {
      Destroy(gameObject);
    }
  }
}
