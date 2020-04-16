using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterFeelback : MonoBehaviour
{
    [SerializeField]
    ParticleSystem shootParticles;

    void Start()
    {
        GetComponent<Shooter>().Onshoot += () => { Shoot(); };
    }

    void Update()
    {

    }

    private void Shoot()
    {
        shootParticles.Clear();
        shootParticles.Play();
    }
}
