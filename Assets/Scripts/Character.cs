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
    GameConstants.Type pokemoType;

    [SerializeField]
    List<Move> moves;

    private float health;
    public List<Move> Moves { get { return moves; } set { moves = value; } }
    public float Health { get { return health; } set { health = value; } }
    public int Speed { get { return speed; } }
    public GameConstants.Type PokemoType { get { return pokemoType; } }
    private void Awake()
    {
        health = maxHealth;

        moves = new List<Move>();
        moves.Add(new Move("Ember ", 6, 10, GameConstants.Type.Fire, 3));
        moves.Add(new Move("Vine Whip ", 10, 5, GameConstants.Type.Grass, 3));
        moves.Add(new Move("water Gun ", 3, 15, GameConstants.Type.Water, 3));
    }

    void Update()
    {
        
    }

    public void ReceiveMove(Move attack, float mulitplier)
    {
        health -= attack.Damage * mulitplier;
    }
}
