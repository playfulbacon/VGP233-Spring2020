using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{

    private Animator animator;
    private Character character;
    private readonly Dictionary<string, float> animationNameLengthDictionary = new Dictionary<string, float>();

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        character = GetComponent<Character>();

        character.OnMovePerformed += PerformMove;
        character.OnMoveReceived += ReceiveMove;
        character.OnDeath += DeathAnimation;
        character.OnRun += RunMoves;

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            animationNameLengthDictionary.Add(clip.name, clip.length);
        }
    }

    public float GetAnimationLength(string name)
    {
        if (animationNameLengthDictionary.ContainsKey(name))
        {
            return animationNameLengthDictionary[name];
        }
        throw new Exception($"The animation of name {name} does not exist in the animationNameLengthDictionary");
    }

    private void PerformMove()
    {
        // TODO: get trigger name from Move
        animator.SetTrigger("Attack");
    }

    private void ReceiveMove()
    {
        // TODO: get trigger name from Move
        animator.SetTrigger("Damage");
    }

    private void RunMoves()
    {
        animator.SetTrigger("Run");
    }

    private void DeathAnimation()
    {
        animator.SetTrigger("Death");
        //if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        //{
        //    animator.gameObject.SetActive(false);
        //}
    }
}
