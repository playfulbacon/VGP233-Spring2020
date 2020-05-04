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

    [SerializeField]
    GameConstants.Type type;

    private int energy;

    [SerializeField]
    string animationName;

    public string AnimationName { get { return animationName; } }

    public int TurnLeft = 2;

    public Move(string name, int speed, int damage, GameConstants.Type type, int maxEnergy, string animationName)
    {
        this.name = name;
        this.speed = speed;
        this.damage = damage;
        this.type = type;
        this.maxEnergy = maxEnergy;
        this.animationName = animationName;

        energy = maxEnergy;
    }

    public void Dynamax(Transform transform)
    {
        if (TurnLeft > 0)
        {
            --TurnLeft;
            damage <<= 1; // damage *2
            transform.localScale *= 2;
        }
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
