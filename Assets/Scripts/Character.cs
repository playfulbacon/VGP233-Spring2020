using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public event Action OnMovePerformed;
    public event Action OnMoveReceived;
    public event Action OnDeath;
    public event Action OnRun;

    public event Action OnHitParticles;

    [SerializeField]
    string name;

    [SerializeField]
    int level;

    [SerializeField]
    float maxHealth;

    public float MaxHealth { get { return maxHealth; } }

    [SerializeField]
    int defence;

    [SerializeField]
    int speed;

    [SerializeField]
    List<GameConstants.Type> types;

    [SerializeField]
    List<Move> moves;

    public bool IsDead = false;

    public List<Move> Moves { get { return moves; } }

    private float health;

    public float Health { get { return health; } }

    public Vector3 originalPos;

    private void Awake()
    {
        health = maxHealth;
        originalPos = transform.position;
    }

    public void Run()
    {
        OnRun?.Invoke();
    }

    public void PerformMove(Move attack)
    {
        if (attack.Name == "Dynamax")
        {
            attack.Dynamax(transform);
        }

        OnMovePerformed?.Invoke();
    }

    public void ReceiveMove(Move attack)
    {
        // modify damage based on type matchup
        health -= attack.Damage;
        if (health <= 0)
        {
            IsDead = true;
            OnDeath?.Invoke();
        }
        else
        {
            OnMoveReceived?.Invoke();
            OnHitParticles?.Invoke();
        }
        
    }
}
