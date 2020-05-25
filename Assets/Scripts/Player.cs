using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public event System.Action OnJump;
    public event System.Action OnAttack;
    public event System.Action OnHeavyAttack;
    public event System.Action OnDodge;
    public event System.Action OnMagicAttack;
    
    [SerializeField]
    Text healthText;

    [SerializeField]
    int health = 10;

    [SerializeField]
    PlayerAnimationEventHandler playerAnimationEventHandler;

    [SerializeField]
    Camera cam;

    [SerializeField]
    float moveSpeed = 7f;

    float maxSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    float jumpHeight = 4f;

    private CharacterController characterController;
    private Vector3 moveDirection;

    [SerializeField]
    float gravity = 20f;

    private bool isAttacking;
    private bool isDodge;
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    public int Health { get { return health; } set { health = value; } }

    private float damageModifier = 1f;
    public float DamageModifier { get { return damageModifier; } }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        maxSpeed = moveSpeed;
        playerAnimationEventHandler.OnStopDodge += ()=> { isDodge = false; moveSpeed = maxSpeed; };
    }

    private void Update()
    {
        if (moveSpeed >= 0)
        {
            moveSpeed -= Time.deltaTime;
        }
        if (characterController.isGrounded)
        {
            Vector3 forward = cam.transform.forward.normalized;
            forward.y = 0f;
            Vector3 right = cam.transform.right.normalized;
            right.y = 0f;

            if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f)
            {
                moveSpeed = maxSpeed;
            }
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
            if (Input.GetButtonDown("Dodge") && !isDodge)
            {
                OnDodge?.Invoke();
                moveSpeed += 2f;
                isDodge = true;
            }
            if (isDodge)
            {
                moveDirection = playerAnimationEventHandler.gameObject.transform.forward;
                moveDirection *= moveSpeed;
            }
            if (Input.GetButtonDown("MagicAttack"))
            {
                damageModifier = 1f;
                isAttacking = true;
                OnMagicAttack?.Invoke();
            }
            
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void takeDamage(int Damage)
    {
        health -= Damage;
        healthText.text = "Health:" + health.ToString();
    }
}
