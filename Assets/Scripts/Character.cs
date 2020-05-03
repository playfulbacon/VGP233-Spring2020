using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public event Action OnMovePerformed;
    public event Action OnMoveReceived;

    [SerializeField]
    string name;

    [SerializeField]
    int level;

    [SerializeField]
    float maxHealth;

    public float MaxHealth { get { return maxHealth; } }
    
    [SerializeField]
    int defence;

    [SerializeField]
    int speed;

    [SerializeField]
    List<GameConstants.Type> types;

    [SerializeField]
    List<Move> moves;

    public List<Move> Moves { get { return moves; } }

    private float health;

    public float Health { get { return health; } }

    private bool isMovingForward = false;

    public bool IsMovingForward { get { return isMovingForward; } set { isMovingForward = value; } }

    private bool isMovingBackward = false;

    public bool IsMovingBackward { get { return isMovingBackward; } set { isMovingBackward = value; } }

    private void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (IsMovingForward)
        {
            MoveForward();
        }
        else if (isMovingBackward)
        {
            MoveBackward();
        }
    }

    public void PerformMove(int moveIndex)
    {
        OnMovePerformed?.Invoke();
    }

    public void ReceiveMove(Move attack)
    {
        // modify damage based on type matchup
        health -= attack.Damage;

        OnMoveReceived?.Invoke();
    }

    public void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime;
    }

    public void MoveBackward()
    {
        transform.position -= transform.forward * Time.deltaTime;
    }
}
