using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move 
{
    [SerializeField]
    string name;

    public string GetName { get { return name; } }

    [SerializeField]
    int speed;
    [SerializeField]
    int damage;

    [SerializeField]
    int maxEnergy;
    public int GetDamage { get { return damage; } }
    [SerializeField]
    GameConstants.Type type;

    private int energy;

    public Move(string name, int speed, int attack, GameConstants.Type type, int maxEnergy )
    {
        this.name = name;
        this.speed = speed;
        this.damage = attack;
        this.type = type;
        this.maxEnergy = energy;
        energy = maxEnergy;
    }

    public bool AttemptMove()
    {
        if (energy > 0)
        {
            energy--;
            return true;
        }
        else
            return false;
    }
}
