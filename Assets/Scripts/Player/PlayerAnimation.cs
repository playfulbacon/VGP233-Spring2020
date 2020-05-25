using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Transform model;

    private CharacterController characterController;
    private Animator animator;
    private Player player;
    private PlayerStats playerStats;

    private Dictionary<string, float> animationLengthDictionary = new Dictionary<string, float>();

    private void Awake()
    {
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        
        player.OnJump += Jump;
        player.OnAttack += Attack;
        player.OnHeavyAttack += HeavyAttack;
        player.OnMagicAttack += MagicAttack;
        player.OnDodgeRoll += DodgeRoll;
        player.OnTaunt += Taunt;
        playerStats.onDeath += Death;
        

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

    private void MagicAttack()
    {
        animator.SetTrigger("MagicAttack");
        StartCoroutine(AttackAnimation("MagicAttack"));
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

    private void Taunt()
    {
        animator.SetTrigger("Taunt");
        animator.SetBool("IsTaunting", true);
        characterController.enabled = false;
        StartCoroutine(TauntAnimation("Taunt1"));
    }

    private IEnumerator TauntAnimation(string animationName)
    {
        float tauntLength = animationLengthDictionary[animationName];
        yield return new WaitForSeconds(tauntLength);
        animator.SetBool("IsTaunting", false);
        characterController.enabled = true;
        player.IsTaunting = false;

    }

    private void DodgeRoll()
    {
        animator.SetTrigger("Dodge");
        animator.SetBool("IsDodging", true);
        
        StartCoroutine(DodgeAnimation("DodgeRoll"));
    }

    private IEnumerator DodgeAnimation(string animationName)
    {
        float dodgeLength = animationLengthDictionary[animationName];
        yield return new WaitForSeconds(dodgeLength);
        animator.SetBool("IsDodging", false);
        player.IsDodging = false;        
    }

    private void Death()
    {
        animator.SetTrigger("Dead");
        characterController.enabled = false;
        StartCoroutine(DeathAnimation("Death"));
    }

    private IEnumerator DeathAnimation(string animationName)
    {
        float deathLength = animationLengthDictionary[animationName];
        yield return new WaitForSeconds(deathLength);
        model.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        player.Revive();        
        characterController.enabled = true;
        model.gameObject.SetActive(true);        
       
    }
}
