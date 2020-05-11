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

    private void Awake()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        player.OnJump += Jump;
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

    private void Jump()
    {
        animator.SetTrigger("Jump");
        animator.SetBool("IsJumping", true);
    }
}
