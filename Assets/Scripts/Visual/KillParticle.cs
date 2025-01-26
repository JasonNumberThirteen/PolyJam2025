using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class KillParticle : MonoBehaviour
{
    ParticleSystem ParticleSystem;

    private void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ParticleSystem.IsAlive())
            return;

        Destroy(gameObject);
    }
}
