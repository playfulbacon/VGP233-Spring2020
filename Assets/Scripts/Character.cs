using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
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

    public List<Move> Moves { get { return moves; } }

    private float health;

    public float Health { get { return health; } }

    private void Awake()
    {
        health = maxHealth;

        moves = new List<Move>();
        moves.Add(new Move("Attack 1", 5, 10, GameConstants.Type.Paper, 3));
        moves.Add(new Move("Attack 2", 5, 5, GameConstants.Type.Scissors, 3));
        moves.Add(new Move("Attack 3", 5, 15, GameConstants.Type.Rock, 3));
    }

    void Update()
    {
        
    }

    public void ReceiveMove(Move attack)
    {
        health -= attack.Damage;
    }
}
