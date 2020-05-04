using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Character : MonoBehaviour
{
    public event System.Action OnMovePerformed;
    public event System.Action OnMoveRecieved;
    public event System.Action OnDamaged;
    public event System.Action OnDeath;
    [SerializeField]
    string charname;

    [SerializeField]
    int level;

    [SerializeField]
    private float health;

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
    private string recieveMove;

    private float effectMulti = 1.5f;
    private float noteffectMulti = 0.5f;
    private float defNegation = 0.1f;
    private int dynamaxDur = 3;
    private int bonusDamageMulti = 1;
    private bool dynaActive = false;

    public string Result { get { return recieveMove; } }
    public GameConstants.Type GetCharType { get { return charType; } }
    public string propName { get { return charname; } set { charname = value; } }
    public float GetSpeed { get { return speed; } }
    public List<Move> Moves { get { return movesList; } }
    public float MaxHealth { get { return maxHealth; } }
    public float Health { get { return health; } }
    public int GetDynaDuration { get { return dynamaxDur; } set { dynamaxDur = value; } }
    public int BonusDamage { get { return bonusDamageMulti; } set { bonusDamageMulti = value; } }
    public bool GetDynaMode { get { return dynaActive; } set { dynaActive = value; } }

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
    }

    List<Move> PopulateMoves()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameConstants.Names randomMoveName = (GameConstants.Names)Random.Range(0, System.Enum.GetValues(typeof(GameConstants.Names)).Length);
            GameConstants.Type randomType = (GameConstants.Type)Random.Range(0, System.Enum.GetValues(typeof(GameConstants.Type)).Length);
            int randomDamage = Random.Range(1, 10);
            int randomEnergy = Random.Range(5, 10);
            int randomSpeed = Random.Range(0, 10);
            movesList.Add(new Move(randomType.ToString() + " " + randomMoveName.ToString(), randomSpeed, randomDamage, randomType, randomEnergy));
        }
        movesList.Add(new Move("DYNAMAX", 100, 0, GameConstants.Type.Paper, 1));
        movesList.Add(new Move("Switch"));
        return movesList;
    }

    public bool isDead()
    {
        if(health <= 0)
        {
            OnDeath?.Invoke();
            return true;
        }
        return false;
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

    public void PerformMove(int MoveIndex)
    {
        OnMovePerformed?.Invoke();
    }

    public void ReceiveMove(Move attack, GameConstants.Type characterType, int bonusDamage)
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
        resultDamage = (resultDamage - (defNegation * defence)) * bonusDamage;

        if (resultDamage < 0)
        {
            recieveMove = charname + "\n recieved no damage"; 
            return;
        }

        OnDamaged?.Invoke();
         health -= resultDamage;
        recieveMove  = charname + "\n" +
                    " recieved " + resultDamage.ToString("F1") + 
                    "(" + attack.MoveType + ")" + " damage" + effective;
        OnMoveRecieved?.Invoke();
    }
}
