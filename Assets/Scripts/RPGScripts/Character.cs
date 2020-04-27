using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    string charname;

    [SerializeField]
    int level;

    [SerializeField]
    float maxHealth;

    [SerializeField]
    GameConstants.Type charType;
    
    [SerializeField]
    int defence;

    [SerializeField]
    int speed;

    [SerializeField]
    List<GameConstants.Type> types;

    [SerializeField]
    List<Move> moves;

    private float health;
    private string recieveMove;

    private float effectMulti = 1.5f;
    private float noteffectMulti = 0.5f;

    public string Result { get { return recieveMove; } }
    public GameConstants.Type GetCharType { get { return charType; } }
    public string propName { get { return charname; } set { charname = value; } }
    public float GetSpeed { get { return speed; } }
    public List<Move> Moves { get { return moves; } }
    public float MaxHealth { get { return maxHealth; } }
    public float Health { get { return health; } }

    private void Awake()
    {
        health = maxHealth;

        moves = new List<Move>();
        moves.Add(new Move("Paper Airplane", 2, 10, GameConstants.Type.Paper, 10));
        moves.Add(new Move("Scissor Cut", 5, 5, GameConstants.Type.Scissors, 5));
        moves.Add(new Move("Rock Throw", 0, 15, GameConstants.Type.Rock, 7));
    }

    public bool isDead()
    {
        return (health <= 0);
    }
    
    public void ReceiveMove(Move attack, GameConstants.Type characterType)
    {
        float resultDamage = 0;
        //Attack Move
        switch (attack.MoveType)
        {
            case GameConstants.Type.Rock:
                //Player Type
                switch (characterType)
                {
                    case GameConstants.Type.Rock:
                        resultDamage = attack.Damage;
                        break;
                    case GameConstants.Type.Paper:
                        resultDamage = attack.Damage * noteffectMulti;
                        break;
                    case GameConstants.Type.Scissors:
                        resultDamage = attack.Damage * effectMulti;
                        break;        
                }
                break;
            case GameConstants.Type.Paper:
                switch (characterType)
                {
                    case GameConstants.Type.Rock:
                        resultDamage = attack.Damage * effectMulti;
                        break;
                    case GameConstants.Type.Paper:
                        resultDamage = attack.Damage;
                        break;
                    case GameConstants.Type.Scissors:
                        resultDamage = attack.Damage * noteffectMulti;
                        break;
                }
                break;
            case GameConstants.Type.Scissors:
                switch (characterType)
                {
                    case GameConstants.Type.Rock:
                        resultDamage = attack.Damage * noteffectMulti;
                        break;
                    case GameConstants.Type.Paper:
                        resultDamage = attack.Damage * effectMulti;
                        break;
                    case GameConstants.Type.Scissors:
                        resultDamage = attack.Damage;
                        break;
                }
            
                break;
        }
            health -= resultDamage;
        recieveMove  = charname + 
                    " recieved " + resultDamage + 
                    "(" + attack.MoveType + ")" + " damage";
    }
}
