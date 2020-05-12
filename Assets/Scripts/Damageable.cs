using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    float maxHealth;

    private float health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Destroy(gameObject);

        GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.white, Color.red, health / maxHealth);
    }
}
