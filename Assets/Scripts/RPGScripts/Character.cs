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
    List<Move> movesList;

    private float health;
    private string recieveMove;

    private float effectMulti = 1.5f;
    private float noteffectMulti = 0.5f;
    private float defNegation = 0.1f;

    public string Result { get { return recieveMove; } }
    public GameConstants.Type GetCharType { get { return charType; } }
    public string propName { get { return charname; } set { charname = value; } }
    public float GetSpeed { get { return speed; } }
    public List<Move> Moves { get { return movesList; } }
    public float MaxHealth { get { return maxHealth; } }
    public float Health { get { return health; } }


    private void Awake()
    {
        int randomlength = Random.Range(3, 9);
        int randomHealth = Random.Range(50, 200);
        int randomLevel = Random.Range(0, 5);
        int randomSpeed = Random.Range(0, 10);
        GameConstants.Type randomType = (GameConstants.Type)Random.Range(0, 2);
        int randomDefense = Random.Range(0, 10);

        charname = CreateRandomName(randomlength);
        maxHealth = randomHealth;
        health = maxHealth;
        level = randomLevel;
        speed = randomSpeed;
        defence = randomDefense;
        charType = randomType;
        movesList = PopulateMoves();
        //moves = new List<Move>();
        //moves.Add(new Move("Paper Airplane", 2, 7, GameConstants.Type.Paper, 10));
        //moves.Add(new Move("Scissor Cut", 5, 5, GameConstants.Type.Scissors, 5));
        //moves.Add(new Move("Rock Throw", 0, 15, GameConstants.Type.Rock, 7));
        // moves.Add(new Move("Switch"));
    }

    List<Move> PopulateMoves()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameConstants.Type randomType = (GameConstants.Type)Random.Range(0, 2);
            int randomDamage = Random.Range(1, 10);
            int randomEnergy = Random.Range(5, 10);
            int randomSpeed = Random.Range(0, 10);
            movesList.Add(new Move("move" + i, randomSpeed, randomDamage, randomType, randomEnergy));
        }
        movesList.Add(new Move("Switch"));
        return movesList;
    }

    public bool isDead()
    {
        return (health <= 0);
    }


    string CreateRandomName(int nameLength)
    {
    
        string randomName = "";
        string[] characters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        for (int i = 0; i <= nameLength; i++)
        {
            randomName = randomName + characters[Random.Range(0, characters.Length)];
        }
        return randomName + "mon";
    }

    public void ReceiveMove(Move attack, GameConstants.Type characterType)
    {
        float resultDamage = 0;
        string effective;
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
        
        if(resultDamage > attack.Damage)
        {
            effective = " Super Effective!";
        }
        else if (resultDamage < attack.Damage)
        {
            effective = " not very Effective...";
        }
        else
        {
            effective = "";
        }
        resultDamage = resultDamage - (defNegation * defence);
        if (resultDamage < 0)
        {
            recieveMove = charname + "\n recieved no damage"; 
            return;
        }
        
         health -= resultDamage;
        recieveMove  = charname + "\n" +
                    " recieved " + resultDamage.ToString("F1") + 
                    "(" + attack.MoveType + ")" + " damage" + effective;
    }
}
