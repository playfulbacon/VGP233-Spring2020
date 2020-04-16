using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterFeedback : MonoBehaviour
{
    [SerializeField]
    ParticleSystem shootParticles;

    private void Start()
    {
        GetComponent<Shooter>().OnShoot += () => { Shoot(); };
    }
    
    private void Update()
    {

    }

    private void Shoot()
    {
        shootParticles.Clear();
        shootParticles.Play();
    }
}
