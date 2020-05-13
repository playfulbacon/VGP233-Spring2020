using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event System.Action OnJump;
    public event System.Action OnAttack;
    public event System.Action OnHeavyAttack;

    [SerializeField]
    Camera cam;

    [SerializeField]
    float moveSpeed = 7f;

    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    float jumpHeight = 4f;

    private CharacterController characterController;
    private Vector3 moveDirection;

    [SerializeField]
    float gravity = 20f;

    private bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }

    private float damageModifier = 1f;
    public float DamageModifier { get { return damageModifier; } }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController.isGrounded)
        {
            Vector3 forward = cam.transform.forward.normalized;
            forward.y = 0f;
            Vector3 right = cam.transform.right.normalized;
            right.y = 0f;

            moveDirection = Vector3.zero;
            if (!isAttacking)
            {
                moveDirection = (forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"));
                moveDirection *= moveSpeed;
            }

            if (Input.GetButtonDown("Jump"))
            {
                OnJump?.Invoke();
                moveDirection.y = jumpHeight;
            }

            if (Input.GetButtonDown("Attack"))
            {
                damageModifier = 1f;
                isAttacking = true;
                OnAttack?.Invoke();
            }

            if (Input.GetButtonDown("HeavyAttack"))
            {
                damageModifier = 2f;
                isAttacking = true;
                OnHeavyAttack?.Invoke();
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
