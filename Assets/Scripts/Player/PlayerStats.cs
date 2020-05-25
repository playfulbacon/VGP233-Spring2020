using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public event System.Action onDamage;
    public event System.Action onSpellCast;
    public event System.Action onDeath;
    public event System.Action onRevive;

    [SerializeField]
    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    [SerializeField]
    private float currentMana;
    public float CurrentMana { get { return currentMana; } set { currentMana = value; } }
    [SerializeField]
    private float maxMana;
    public float MaxMana { get { return maxMana; } set { maxMana = value; } }

    private float baseDamage = 10f;
    public float BaseDamage { get { return baseDamage; } }
    private float damageModifier = 1f;
    public float DamageModifier { get { return damageModifier; } set { damageModifier = value; } }
    private float baseDefense = 5f;
    private float damageTaken = 0f;


    private bool isDead = false;
    private bool isTrapActivated = false;   
    private void Start()
    {        
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (isTrapActivated)
                TakeTrapDamage();
        }
    }

    public void RestoreStats()
    {
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
        onRevive?.Invoke();
    }

    private void TakeTrapDamage()
    {
        if (CurrentHealth <= 0)
            Dead();
        CurrentHealth -= Time.deltaTime * damageTaken;
        onDamage?.Invoke();
        isTrapActivated = false;
    }

    private void TakeDamage()
    {
        if (CurrentHealth <= 0)
            Dead();
        CurrentHealth -= damageTaken;
        onDamage?.Invoke();       
    }

    public void SpellCast(GameObject spell)
    {
        CurrentMana -= spell.GetComponent<MagicSpell>().MagicCost;
        onSpellCast?.Invoke();
    }
  
    private void OnTriggerStay(Collider other)
    {      
        if (other.tag == "Trap")
        {
            isTrapActivated = true;
            damageTaken = other.GetComponent<Trap>().TrapDamage;
        }
    }

    public float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    public float CalculateMana()
    {
        return CurrentMana / MaxMana;
    }

    private void Dead()
    {
        isDead = true;
        CurrentHealth = 0;
        onDeath?.Invoke();
    }
}
