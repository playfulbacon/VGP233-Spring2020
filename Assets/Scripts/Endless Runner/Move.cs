using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move
{
    [SerializeField]
    string name;

    public string Name { get { return name; } }

    [SerializeField]
    int speed;

    public int GetMoveSpeed { get { return speed; } }

    [SerializeField]
    int damage;

    [SerializeField]
    int maxEnergy;

    public int GetMaxEnergy { get { return maxEnergy; } }

    public int Damage { get { return damage; } }

    [SerializeField]
    GameConstants.Type type;

    public GameConstants.Type MoveType { get { return type; } }

    [SerializeField]
    private int energy;

    public int GetEnergy { get { return energy; } }

    public Move(string name)
    {
        this.name = name;
        maxEnergy = 100;
        energy = maxEnergy;
    }

    public Move(string name, int speed, int damage, GameConstants.Type type, int maxEnergy)
    {
        this.name = name;
        this.speed = speed;
        this.damage = damage;
        this.type = type;
        this.maxEnergy = maxEnergy;

        energy = maxEnergy;
    }

    public bool AttemptMove()
    {
        if (energy > 0)
        {
            energy--;
            return true;
        }
        else return false;
    }
}
