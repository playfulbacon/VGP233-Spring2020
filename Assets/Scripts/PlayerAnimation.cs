using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Transform model;

    [SerializeField]
    PlayerAnimationEventHandler playerAnimationEventHandler;

    private CharacterController characterController;
    private Animator animator;
    private Player player;

    private Dictionary<string, float> animationLengthDictionary = new Dictionary<string, float>();

    private void Awake()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerAnimationEventHandler.OnStopDodge += ()=> { animator.SetBool("Dodge", false); };

        player.OnMagicAttack += MagicAttack;
        player.OnJump += Jump;
        player.OnAttack += Attack;
        player.OnHeavyAttack += HeavyAttack;
        player.OnDodge += Dodge;

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            animationLengthDictionary.Add(clip.name, clip.length);
    }

    private void Update()
    {
        bool isJumping = animator.GetBool("IsJumping");

        if (characterController.isGrounded && isJumping)
            animator.SetBool("IsJumping", false);

        if (characterController.velocity.magnitude > 0.1f)
        {
            Vector3 faceDirection = characterController.velocity.normalized;
            faceDirection.y = 0;
            model.transform.forward = faceDirection;
        }

        animator.SetFloat("MoveSpeed", characterController.velocity.magnitude / player.MoveSpeed);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        StartCoroutine(AttackAnimation("Attack"));
    }

    private void HeavyAttack()
    {
        animator.SetTrigger("HeavyAttack");
        StartCoroutine(AttackAnimation("HeavyAttack"));
    }

    private IEnumerator AttackAnimation(string animationName)
    {
        float attackLength = animationLengthDictionary[animationName];
        yield return new WaitForSeconds(attackLength);
        player.IsAttacking = false;
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        animator.SetBool("IsJumping", true);
    }

    private void Dodge()
    {
        //animator.SetTrigger("Dodge");
        animator.SetBool("Dodge", true);
    }

    private void MagicAttack()
    {
        animator.SetTrigger("MagicAttack");
        StartCoroutine(AttackAnimation("MagicAttack"));
    }
}
