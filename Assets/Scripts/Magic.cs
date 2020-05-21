using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    ParticleSystem fireParticles;

    void Awake()
    {
        player.OnCastFire += CastFire;
    }
    
    void Update()
    {
        
    }

    void CastFire(Targetable target)
    {
        ParticleSystem newFire = Instantiate(fireParticles, target.gameObject.transform.position, fireParticles.transform.rotation);
        newFire.transform.parent = target.gameObject.transform;
        newFire.Play();
    }
}
