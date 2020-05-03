using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private List<GameObject> characters = new List<GameObject>();

    [SerializeField]
    private ParticleSystem damageParticle;

    private Dictionary<CharacterAnimationController, ParticleSystem> CharacterParticlesDamage = new Dictionary<CharacterAnimationController, ParticleSystem>();

    private void Awake()
    {
        characters.AddRange(GameObject.FindGameObjectsWithTag("Character"));

        foreach (GameObject character in characters)
        {
            CharacterParticlesDamage.Add(character.GetComponent<CharacterAnimationController>(), Instantiate(damageParticle, character.transform.position, Quaternion.identity));
        }
    }

    private void AnimAttack()
    {
        foreach (KeyValuePair<CharacterAnimationController, ParticleSystem> character in CharacterParticlesDamage)
        {
            if (character.Key.GetComponentInChildren<Animator>() != this.GetComponent<Animator>())
            {
                character.Key.GetComponent<CharacterAnimationController>().ReceiveMove();
                character.Value.Play();
            }
        }
    }
}