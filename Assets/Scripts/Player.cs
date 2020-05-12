using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Camera cam;

    public System.Action OnJump;
    public System.Action OnLand;
    public System.Action OnAttack;
    public System.Action OnHeavyAttack;

    private bool isAttacking;

    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }

    [SerializeField]
    private float moveSpeed = 7f;

    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    float jumpHeight = 10f;

    [SerializeField]
    float gravity = 9.8f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();
    }
    
    private void Update()
    {
        // Run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 2f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 2f;
        }

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
                OnAttack?.Invoke();
            }

            if (Input.GetButtonDown("HeavyAttack"))
            {
                OnHeavyAttack?.Invoke();
            }

            //// OnLand
            //OnLand?.Invoke();
        }

        

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
