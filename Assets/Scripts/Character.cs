using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public event Action OnMovePerformed;
    public event Action OnMoveReceived;

    [SerializeField]
    string mName;

    public string Name { get { return mName; } }

    [SerializeField]
    int level;

    [SerializeField]
    float maxHealth;

    public float MaxHealth { get { return maxHealth; } }

    [SerializeField]
    int defense;

    [SerializeField]
    int speed;

    public int Speed { get { return speed; } }

    [SerializeField]
    List<GameConstants.Attribute> types;

    [SerializeField]
    List<Move> moves;

    public List<Move> Moves {  get { return moves; } }

    private float health;
    public float Health { get { return health; } }

    private void Awake()
    {
        health = maxHealth;

        moves = new List<Move>();
        moves.Add(new Move("Attack 1", 1, 20, GameConstants.Attribute.Rock, 25));
        moves.Add(new Move("Attack 2", 2, 10, GameConstants.Attribute.Paper, 20));
        moves.Add(new Move("Attack 3", 4, 5, GameConstants.Attribute.Scissors, 15));
        moves.Add(new Move("Attack 4", 1, 25, GameConstants.Attribute.Rock, 10));
    }

    public Character(string name)
    {
        mName = name;
    }

    public void PerformMove(int moveindex)
    {
        OnMovePerformed?.Invoke();
    }

    public void ReceiveMove(Move move)
    {
        // Add Modifiers

        // Bonus Damage
        if (move.Type == GameConstants.Attribute.Rock)
        {
            if (types.Contains(GameConstants.Attribute.Scissors) && !types.Contains(GameConstants.Attribute.Paper))
            {
                health -= move.Damage;
            }
        }
        if (move.Type == GameConstants.Attribute.Paper)
        {
            if (types.Contains(GameConstants.Attribute.Rock) && !types.Contains(GameConstants.Attribute.Scissors))
            {
                health -= move.Damage;
            }
        }
        if (move.Type == GameConstants.Attribute.Scissors)
        {
            if (types.Contains(GameConstants.Attribute.Paper) && !types.Contains(GameConstants.Attribute.Rock))
            {
                health -= move.Damage;
            }
        }

        // Less Damage
        if (move.Type == GameConstants.Attribute.Rock)
        {
            if (types.Contains(GameConstants.Attribute.Paper) && !types.Contains(GameConstants.Attribute.Scissors))
            {
                health -= (move.Damage) / 2;
            }
        }
        if (move.Type == GameConstants.Attribute.Paper)
        {
            if (types.Contains(GameConstants.Attribute.Scissors) && !types.Contains(GameConstants.Attribute.Rock))
            {
                health -= (move.Damage) / 2;
            }
        }
        if (move.Type == GameConstants.Attribute.Scissors)
        {
            if (types.Contains(GameConstants.Attribute.Rock) && !types.Contains(GameConstants.Attribute.Paper))
            {
                health -= (move.Damage) / 2;
            }
        }
        else
        {
            health -= move.Damage;
        }

        OnMoveReceived?.Invoke();
    }
}
