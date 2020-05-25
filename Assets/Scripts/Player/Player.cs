using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event System.Action OnJump;
    public event System.Action OnAttack;
    public event System.Action OnHeavyAttack;
    public event System.Action OnMagicAttack;
    public event System.Action OnDodgeRoll;
    public event System.Action OnTaunt;
    public Transform SpawnPosition;

    [SerializeField]
    Camera cam;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    private float verticalSpeed;
    private float horizontalSpeed;

    [SerializeField]
    private float rollSpeed = 15f;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    float jumpHeight = 5f;
    int allowedJumps = 1;
    int jumpCounter = 0;
    private CharacterController characterController;
    private Vector3 moveDirection;
    public Vector3 MoveDirection { get { return moveDirection; } }

    [SerializeField]
    float gravity = 20f;

    private bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }

    private bool isDodging;
    public bool IsDodging { get { return isDodging; } set { isDodging = value; } }

    private bool isTaunting;
    public bool IsTaunting { get { return isTaunting; } set { isTaunting = value; } }
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController.enabled)
        {
            if (characterController.isGrounded && !isDodging)
            {
                Vector3 forward = cam.transform.forward.normalized;
                forward.y = 0f;
                Vector3 right = cam.transform.right.normalized;
                right.y = 0f;
                moveDirection = Vector3.zero;
                if (!isAttacking)
                {
                    moveDirection = (forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"));
                    if (Input.GetButtonDown("DodgeRoll"))
                    {
                        IsDodging = true;
                        OnDodgeRoll?.Invoke();
                        moveDirection *= rollSpeed;
                    }
                    else
                        moveDirection *= MoveSpeed;
                }
                if (Input.GetButtonDown("Attack"))
                {
                    FindObjectOfType<PlayerStats>().DamageModifier = 1f;
                    isAttacking = true;
                    OnAttack?.Invoke();
                }

                if (Input.GetButtonDown("HeavyAttack"))
                {
                    FindObjectOfType<PlayerStats>().DamageModifier = 2.5f;
                    isAttacking = true;
                    OnHeavyAttack?.Invoke();
                }

                if (Input.GetButtonDown("DodgeRoll"))
                {
                    IsDodging = true;
                    OnDodgeRoll?.Invoke();
                }

                if (Input.GetButtonDown("MagicAttack"))
                {
                    isAttacking = true;
                    OnMagicAttack?.Invoke();
                }

                if(Input.GetButtonDown("Taunt"))
                {
                    IsTaunting = true;
                    OnTaunt?.Invoke();
                }

            }

            if (Input.GetButtonDown("Jump"))
            {
                if (characterController.isGrounded)
                {
                    OnJump?.Invoke();
                    moveDirection.y = jumpHeight;
                    jumpCounter = 0;
                }
                if (!characterController.isGrounded && jumpCounter < allowedJumps)
                {
                    OnJump?.Invoke();
                    moveDirection.y = jumpHeight * 1.5f;
                    jumpCounter++;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    public void Revive()
    {
        transform.position = SpawnPosition.position;
        FindObjectOfType<PlayerStats>().RestoreStats();
    }
}
