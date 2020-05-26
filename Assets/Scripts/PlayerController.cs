using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action OnLand;
    public event Action OnJump;

    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float jumpForce = 300f;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    int doubleJumps = 1;

    private Vector2 groundCheckSize = Vector2.one;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private readonly float movementSmoothing = 0.01f;

    private int jumps = 0;

    private bool isGrounded = false;

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
                OnLand?.Invoke();
            }
        }

        float moveDirection = Input.GetAxis("Horizontal");
        Vector2 targetVelocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
        
        if ((isGrounded || jumps < doubleJumps) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0.0f, jumpForce));
            jumps++;
            OnJump?.Invoke();
        }
    }
}
