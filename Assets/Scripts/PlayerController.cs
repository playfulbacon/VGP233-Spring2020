using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event System.Action OnLanded;
    public event System.Action OnJump;

    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float jumpForce = 50f;

    [SerializeField]
    private Transform groundCheck;

    private Vector2 groundCheckSize = Vector2.one;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    int airJumps;

    private Vector2 velocity = Vector2.zero;
    private float movementSmoothing = 0.01f;

    private bool isGrounded = false;
    private int jumps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;

        isGrounded = false;
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundMask))
        {
            isGrounded = true;
            jumps = 0;
            if (!wasGrounded)
            {
                OnLanded?.Invoke();
            }
        }

        float moveDirection = Input.GetAxis("Horizontal");
        Vector2 targetVelocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        if((isGrounded || jumps < airJumps) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce));
            jumps++;
            OnJump?.Invoke();
        }
    }
}
