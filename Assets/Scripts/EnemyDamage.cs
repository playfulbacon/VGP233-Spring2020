using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // ----- Stats -----

    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    // -----------------

    [SerializeField]
    private Transform headCheck;

    [SerializeField]
    private Transform leftCheck;

    [SerializeField]
    private Transform rightCheck;

    private Vector2 headCheckSize = new Vector2(0.65f, 0.001f);
    private Vector2 sideCheckSize = new Vector2(0.001f, 0.65f);

    [SerializeField]
    private LayerMask playerMask;

    private Player player;
    private PlayerController playerController;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerController = player.GetComponent<PlayerController>();
        currentHealth = maxHealth;
    }
    
    private void Update()
    {
        // Head stomp
        if (Physics2D.OverlapBox(headCheck.position, headCheckSize, 0, playerMask))
        {
            playerController.Bounce();
            currentHealth -= maxHealth;
        }

        // Hit Player
        if (Physics2D.OverlapBox(leftCheck.position, sideCheckSize, 0, playerMask) ||
            Physics2D.OverlapBox(rightCheck.position, sideCheckSize, 0, playerMask))
        {
            player.TakeDamage();
        }

        // Death
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
