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

    public float GetMaxHealth { get { return maxHealth; }}

    [SerializeField]
    int defense;

    [SerializeField]
    int speed;

    [SerializeField]
    List<GameConstants.Type> types;

    [SerializeField]
    List<Move> moves;

    private float currentHealth;
    public float GetCurrentHealth { get { return currentHealth; } }


    public List<Move> moveList { get { return moves; } }

    private void Awake()        
    {
        currentHealth = maxHealth;
        moves = new List<Move>();
        moves.Add(new Move("Attack 1", 5, 10, GameConstants.Type.Paper,3));
        moves.Add(new Move("Attack 2", 5, 5, GameConstants.Type.Scissors,3));
        moves.Add(new Move("Attack 3", 5, 15, GameConstants.Type.Rock,3));
    }

    private void Update()
    {
        
    }

    public void RecieveMove(Move damage)
    {
        currentHealth -= damage.GetDamage;
    }
}
