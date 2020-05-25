using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Transform model;

    private CharacterController characterController;
    private Animator animator;
    private Player player;
    private PlayerHealth playerHealth;

    private readonly Dictionary<string, float> animationLengthDictionary = new Dictionary<string, float>();

    private void Awake()
    {
        player = GetComponent<Player>();
        playerHealth = GetComponent<PlayerHealth>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        player.OnJump += Jump;
        player.OnAttack += Attack;
        player.OnHeavyAttack += HeavyAttack;
        player.OnDodge += Dodge;
        player.OnCastMagic += CastMagic;

        playerHealth.OnDeath += DeathAnimation;

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            animationLengthDictionary.Add(clip.name, clip.length);
        }
    }

    private void Update()
    {
        bool isJumping = animator.GetBool("IsJumping");

        if (characterController.isGrounded && isJumping)
        {
            animator.SetBool("IsJumping", false);
        }

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
        if (player.IsDodge)
        {
            StartCoroutine(DodgeAnimation("Standing Dive Forward"));
        }
    }

    private IEnumerator DodgeAnimation(string dodgeAnimation)
    {
        float dodgeLength = animationLengthDictionary[dodgeAnimation] / 0.6f;
        float elapsedTime = 0f;
        while (elapsedTime < dodgeLength)
        {
            player.moveDirection *= Time.deltaTime;
            elapsedTime += Time.deltaTime;
        }
        animator.SetTrigger("Dodge");
        yield return new WaitForSeconds(dodgeLength);
        player.IsDodge = false;
    }

    private void CastMagic()
    {
        animator.SetTrigger("Magic");
        StartCoroutine(CastMagicRoutine());
    }

    private IEnumerator CastMagicRoutine()
    {
        float length = animationLengthDictionary["Standing Magic Attack"] / 0.6f;
        yield return new WaitForSeconds(length);
        player.IsMove = true;
    }

    private void DeathAnimation()
    {
        animator.SetTrigger("Death");
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        float length = animationLengthDictionary["Standing Dive Forward"] / 0.6f;
        yield return new WaitForSeconds(length);
        GameManager.RestartCurrentLevel();
    }

}
