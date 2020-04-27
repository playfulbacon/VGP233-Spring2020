using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum Effect
    {
        Capture
    }

    [SerializeField]
    string name;

    public string mName { get { return name; } set { name = value; } }

    private Effect effect;

    public Effect mEffect { get { return effect; } set { effect = value; } }

    public Item(string name, Effect effect)
    {
        mName = name;
        mEffect = effect;
    }
}
