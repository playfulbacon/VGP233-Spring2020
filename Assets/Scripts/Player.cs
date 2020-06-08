using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event System.Action OnDeath;

    [SerializeField]
    private float maxHealth;

    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    private float currentHealth;

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    private bool canGetHit;

    private Color color;
    private Color blinkColor;

    private SpriteRenderer renderer;

    private EnemyDamage enemyDamage;

    private void Awake()
    {
        enemyDamage = FindObjectOfType<EnemyDamage>();
        enemyDamage.OnPlayerHit += () => TakeDamage();

        renderer = GetComponentInChildren<SpriteRenderer>();
        color = renderer.color;
        blinkColor = new Color(color.r, color.g, color.b, 0.2f);

        currentHealth = maxHealth;
        canGetHit = true;
    }

    private void Update()
    {
        if (!canGetHit)
        {
            StartCoroutine(Blink());
        }

        // Death
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    void TakeDamage()
    {
        if (canGetHit)
        {
            currentHealth--;
            canGetHit = false;
            StartCoroutine(Invicible());
        }
    }
    private IEnumerator Blink()
    {
        renderer.color = blinkColor;
        yield return new WaitForSeconds(0.1f);
        renderer.color = color;
    }

    private IEnumerator Invicible()
    {
        yield return new WaitForSeconds(2.0f);
        canGetHit = true;
    }
}
