using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action OnLand;
    public event Action<GameObject> OnJump;
    public event Action<int> OnTakeDamge;
    public event Action<int> OnHeal;

    [Header("Properties")]
    [SerializeField]
    float moveSpeed = 10f;

    [SerializeField]
    float jumpForce = 50f;

    [SerializeField]
    private Transform groundCheck = default;

    [SerializeField]
    private LayerMask groundMask = default;

    [SerializeField]
    int doubleJumps = 1;

    [Header("Twitch Message")]
    [SerializeField]
    private string twitchMessageJump = "jump";
    [SerializeField]
    private string twitchMessageHeal = "heal";

    private Vector2 groundCheckSize = Vector2.one;
    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    private readonly float movementSmoothing = 0.01f;
    private bool grounded = false;
    private int jumps = 0;

    public float moveDirection = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        TwitchChat twitchChat = FindObjectOfType<TwitchChat>();
        twitchChat.OnMessageReceived += (x) => TwitchCommand(x);
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundMask))
        {
            grounded = true;
            jumps = 0;
            if (!wasGrounded)
                OnLand?.Invoke();
        }

        moveDirection = Input.GetAxis("Horizontal");
        Vector2 targetVelocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        if ((grounded || jumps < doubleJumps) && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
        jumps++;
    }

    public void TakeDamage(int amount)
    {
        OnTakeDamge?.Invoke(amount);
    }

    public void JumpStompArea(Enemy enemy)
    {
        OnJump?.Invoke(enemy.gameObject);
    }

    private void TwitchCommand(string twitchMessage)
    {
        Debug.Log($"attempt player to jump {twitchMessage}");
        if (twitchMessage.ToLower() == twitchMessageJump.ToLower())
        {
            Jump();
        }
        else if (twitchMessageHeal.ToLower() == twitchMessageHeal.ToLower())
        {
            OnHeal?.Invoke(50);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GroundLimit") || 
            collision.CompareTag("Goal"))
        {
            GameManager.RestartLevel();
        }
    }

}
