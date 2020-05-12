﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    Transform model;

    private CharacterController characterController;
    private Animator animator;
    private Player player;

    private Dictionary<string, float> animationLengthDictionary = new Dictionary<string, float>();

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();

        player.OnJump += Jump;
        player.OnLand += Land;
        player.OnAttack += Attack;
        player.OnHeavyAttack += HeavyAttack;

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            animationLengthDictionary.Add(clip.name, clip.length);
        }
    }
    
    private void Update()
    {
        if (characterController.velocity.magnitude > 0.1f)
        {
            Vector3 faceDirection = characterController.velocity.normalized;
            faceDirection.y = 0;
            model.transform.forward = faceDirection;
        }

        animator.SetFloat("MoveSpeed", characterController.velocity.magnitude / player.MoveSpeed);
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
    }

    private void Land()
    {
        animator.SetTrigger("Landed");
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        player.IsAttacking = true;
        StartCoroutine(AttackAnimation("Slash"));
    }

    private void HeavyAttack()
    {
        animator.SetTrigger("HeavyAttack");
        player.IsAttacking = true;
        StartCoroutine(AttackAnimation("ThrustSlash"));
    }

    private IEnumerator AttackAnimation(string animationName)
    {
        float attackTime = animationLengthDictionary[animationName];
        yield return new WaitForSeconds(attackTime);
        player.IsAttacking = false;
    }
}
