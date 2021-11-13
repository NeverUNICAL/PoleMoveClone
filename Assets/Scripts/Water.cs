using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private ParticleSystem _template;

    public void CreateParticle(Transform transformPosition)
    {
        ParticleSystem particle = Instantiate(_template, transformPosition);
        particle.Play();
    }
}

