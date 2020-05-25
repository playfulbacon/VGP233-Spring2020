using UnityEngine;
using System;

[RequireComponent(typeof(Camera))]
public class Player : MonoBehaviour
{
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnHeavyAttack;
    public event Action OnDodge;
    public event Action OnCastMagic;

    [SerializeField] Camera cam;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float evadeSpeed = 14f;
    [SerializeField] float gravity = 20f;
    [SerializeField] float jumpHeight = 4f;

    private CharacterController characterController;
    private PlayerHealth playerHealth;
    public Vector3 moveDirection;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    private bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    private float damageModifier = 1f;
    public float DamageModifier { get { return damageModifier; } }
    public bool IsDodge { get; set; }

    public PlayerHealth PlayerHealth { get { return playerHealth; } }

    private bool doubleJump = false;

    public SpawnProjectiles MagicCast;

    public bool IsMove = true;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();

        MagicCast = GetComponentInChildren<SpawnProjectiles>();

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

            if (Input.GetKeyDown(KeyCode.X))
            {
                IsDodge = true;
                OnDodge?.Invoke();
                moveDirection *= evadeSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                IsMove = false;
                OnCastMagic?.Invoke();
            }

        }
        DoubleJump();
        moveDirection.y -= gravity * Time.deltaTime;

        if (IsMove)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    private void DoubleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (characterController.isGrounded)
            {
                OnJump?.Invoke();
                moveDirection.y = jumpHeight;
                doubleJump = true;
            }
            else
            {
                if (doubleJump)
                {
                    OnJump?.Invoke();
                    moveDirection.y = jumpHeight;
                }
            }
        }
    }

}
