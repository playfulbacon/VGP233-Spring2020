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
    int maxEnergy;

    public int MaxEnergy {  get { return maxEnergy; } }

    [SerializeField]
    int speed;

    public int Speed { get { return speed; } }

    [SerializeField]
    int damage;

    public int Damage { get { return damage; } }

    private int energy;
    public int Energy {  get { return energy; } }

    [SerializeField]
    GameConstants.Attribute type;

    public GameConstants.Attribute Type { get { return type; } }

    public Move(string name, int speed, int damage, GameConstants.Attribute type, int maxEnergy)
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
