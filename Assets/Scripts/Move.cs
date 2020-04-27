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

    [SerializeField]
    int damage;

    [SerializeField]
    int maxEnergy;

    public int Damage { get { return damage; } }
    public int Energy { get { return energy; } }
    public int Speed { get { return speed; } }
    public GameConstants.Type Type { get { return type; } }

    [SerializeField]
    GameConstants.Type type;

    private int energy;

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
