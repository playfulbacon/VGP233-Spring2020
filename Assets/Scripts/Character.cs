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
    int defense;

    [SerializeField]
    int speed;

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
        moves.Add(new Move("Attack 1", 5, 10, GameConstants.Attribute.Rock, 25));
        moves.Add(new Move("Attack 2", 5, 15, GameConstants.Attribute.Paper, 20));
        moves.Add(new Move("Attack 3", 5, 20, GameConstants.Attribute.Scissors, 15));
        moves.Add(new Move("Attack 4", 5, 25, GameConstants.Attribute.Rock, 10));
    }
    
    private void Update()
    {
        
    }

    public void ReceiveMove(Move move)
    {
        health -= move.Damage;
    }
}
