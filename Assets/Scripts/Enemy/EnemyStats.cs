using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public event System.Action onDamage;
    private float health;
    [SerializeField]
    private float maxHealth;
    public float CurrentHealth { get { return health; }}
    public float MaxHealth { get { return maxHealth; } }
    void Start()
    {
        health = maxHealth;        
    }

    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        onDamage?.Invoke();
    }



    public float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }
}
