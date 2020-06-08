using UnityEngine;
using System;

public class PlayerAction : MonoBehaviour
{
    public event Action<int> OnUpdateHealthUI;
    public event Action<int> OnSetHealthMaxUI;

    private PlayerController mPlayer;
    private PlayerHealth mPlayerHealth;


    private void Awake()
    {
        mPlayer = GetComponent<PlayerController>();
        mPlayer.OnJump += StompEnemy;
        mPlayer.OnTakeDamge += TakeDamage;
        mPlayer.OnHeal += Heal;

        mPlayerHealth = GetComponentInParent<PlayerHealth>();
        OnSetHealthMaxUI?.Invoke(mPlayerHealth.MaxHealth);
    }

    private void StompEnemy(GameObject enemy)
    {
        if (enemy)
        {
            Destroy(enemy);
        }
    }

    private void TakeDamage(int amount)
    {
        mPlayerHealth.CurrentHealth -= amount;
        mPlayerHealth.CurrentHealth = Mathf.Clamp(mPlayerHealth.CurrentHealth, 0, mPlayerHealth.MaxHealth);
        OnUpdateHealthUI?.Invoke(mPlayerHealth.CurrentHealth);
        if (mPlayerHealth.CurrentHealth <= 0)
        {
            GameManager.RestartLevel();
        }
    }

    private void Heal(int amount)
    {
        mPlayerHealth.CurrentHealth += amount;
        mPlayerHealth.CurrentHealth = Mathf.Clamp(mPlayerHealth.CurrentHealth, amount, mPlayerHealth.MaxHealth);
        OnUpdateHealthUI?.Invoke(mPlayerHealth.CurrentHealth);
    }

}
