using System;
using UnityEngine;

[Serializable]
public class Move
{
    [SerializeField]
    string name;

    public string Name { get { return $"{name}\n Energy: {maxEnergy}"; } }

    [SerializeField]
    int speed;

    [SerializeField]
    int damage;

    [SerializeField]
    int maxEnergy;

    public int Damage { get { return damage; } }

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

    //public int TypedDamage(GameConstants.Type type)
    //{
    //    if ()

    //    return 0;
    //}

    public bool AttemptMove()
    {
        if (energy > 0)
        {
            energy--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
