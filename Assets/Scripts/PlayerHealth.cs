using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public event Action<float> OnUpdateUI;
    public event Action OnDeath;

    [SerializeField] private int MaxHealth = 100;
    private int currentHealth;
    public bool isDead = false;

    private void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void CurrentHealth(int amount)
    {
        currentHealth -= amount;
        float currentPercentage = currentHealth / (float)MaxHealth;
        OnUpdateUI(currentPercentage);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            OnDeath?.Invoke();
        }
    }

}
