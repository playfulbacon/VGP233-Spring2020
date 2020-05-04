using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFeedback : MonoBehaviour
{
    [SerializeField]
    ParticleSystem damageParticle;

    void Start()
    {
        GetComponent<Character>().OnMoveReceived += PlayerDamageParticle;
    }

    void PlayerDamageParticle()
    {
        damageParticle.Play();
    }

}
