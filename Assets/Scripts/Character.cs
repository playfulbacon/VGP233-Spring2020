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

    [SerializeField]
    private float health;

    public float MaxHealth { get { return maxHealth; } }

    [SerializeField]
    int defense;

    [SerializeField]
    int speed;

    public int Speed { get { return speed; } }

    [SerializeField]
    private GameConstants.CharacterType mCharType;

    public GameConstants.CharacterType CharType { get { return mCharType; } }

    [SerializeField]
    List<GameConstants.Type> types;

    [SerializeField]
    List<Move> moves;

    public List<Move> Moves { get { return moves; } }

   

    public float Health { get { return health; } }

    private void Awake()
    {
        health = maxHealth;
        moves = new List<Move>
        {
            new Move("Paper attack", 5, 10, GameConstants.Type.Paper, 3),
            new Move("Special Scissor", 5, 5, GameConstants.Type.Scissors, 3),
            new Move("Rock attack", 5, 15, GameConstants.Type.Rock, 3)
        };
    }

    public void CheckCharacterType(Character enemy, Move receiveAttack)
    {
        if (mCharType != enemy.mCharType)
        {
            if (((mCharType == GameConstants.CharacterType.Water) && (enemy.mCharType == GameConstants.CharacterType.Fire))
                || (((mCharType == GameConstants.CharacterType.Wind) && (enemy.mCharType == GameConstants.CharacterType.Rock))))
            {
                health -= receiveAttack.Damage >> 1;
            }
            else
            {
                health -= receiveAttack.Damage << 1;
            }
        }
        else
        {
            health -= receiveAttack.Damage;
        }
    }

    public void ReceiveMove(Move attack)
    {
        health -= attack.Damage;
    }
}
