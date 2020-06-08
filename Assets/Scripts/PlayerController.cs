using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public event System.Action OnLand;
    public event System.Action OnJump;

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    float moveSpeed = 10f;

    [SerializeField]
    float jumpForce = 200f;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    int doubleJumps = 1;

    [SerializeField]
    float health = 50;

    [SerializeField]
    float maxHealth = 100;

    [SerializeField]
    string twitchMessageToJump = "jump";

    [SerializeField]
    string twitchMessageToHealth = "health";

    private Vector2 groundCheckSize = Vector2.one;

    private Rigidbody2D rb;
    private bool isPlayerDie;
    private Vector2 velocity = Vector2.zero;
    private float movementSmoothing = 0.01f;
    private bool grounded = false;
    private int jumps = 0;
    private float timeDelay = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        TwitchChat twitchChat = FindObjectOfType<TwitchChat>();
        twitchChat.OnMessageReceived += (x) => GiveJump(x);
        twitchChat.OnMessageReceived += (x) => GiveHealth(x);
        isPlayerDie = false;
    }

    private void Update()
    {
        healthBar.value = health/maxHealth;
        CheckPLayerHealth();
        if (isPlayerDie)
        {
            SceneManager.LoadScene("Main");
        }
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

        float moveDirection = Input.GetAxis("Horizontal");
        Vector2 targetVelocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
        if(Input.GetButtonDown("jump"))
        {
           jump();
        }
    }

    void jump()
    {
        if ((grounded || jumps < doubleJumps))
        {
            rb.AddForce(new Vector2(0, jumpForce));
            jumps++;
            OnJump?.Invoke();
        }
    }

    void GiveJump(string twitchMessage)
    {
        Debug.Log("jump " + twitchMessage);
        if (twitchMessage.ToLower() == twitchMessageToJump.ToLower())
        {
            jump();
        }
    }

    void GiveHealth(string twitchMessage)
    {
        Debug.Log("health " + twitchMessage);
        if (twitchMessage.ToLower() == twitchMessageToHealth.ToLower())
        {
            health += 10;
        }
    }

    void CheckPLayerHealth()
    {
        if (health < 0)
        {
            isPlayerDie = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (timeDelay < Time.time)
            {
                timeDelay = Time.time + 1.0f;
                health -= 10.0f;
            }
        }
    }
}
