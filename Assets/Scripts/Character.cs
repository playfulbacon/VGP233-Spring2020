using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public event Action OnMovePerformed;
    public event Action OnMoveReceived;
    public event Action OnDeath;
    public event Action OnMovement;

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
    GameConstants.Type pokemoType;

    [SerializeField]
    List<Move> moves;

    [SerializeField]
    GameObject EndPosition;

    List<Vector3> destination = new List<Vector3>();
    public bool IsMoving = false;
    int currentDestiation = 0;

    private float health;
    public List<Move> Moves { get { return moves; } set { moves = value; } }
    public float Health { get { return health; } set { health = value; } }
    public int Speed { get { return speed; } }
    public GameConstants.Type PokemoType { get { return pokemoType; } }
    private void Awake()
    {
        health = maxHealth;
        foreach (var move in Moves)
        {
            move.Reset();
        }
        
        destination.Add(transform.position);
        destination.Add(EndPosition.transform.position);
        currentDestiation = 1;
    }

    private void Update()
    {
        if(IsMoving)
        {
            transform.position += Vector3.Normalize(destination[currentDestiation] - transform.position) * Time.deltaTime * speed;
            if(Vector3.Distance( destination[currentDestiation],transform.position)< 0.5f)
            {
                IsMoving = false;
                //OnMovement?.Invoke();
                currentDestiation = (currentDestiation + 1) % destination.Count; 
            }
        }
    }

    public void PerformMove(int moveIndex)
    {
        OnMovePerformed?.Invoke();
    }

    public void ReceiveMove(Move attack, float mulitplier)
    {
        health -= attack.Damage * mulitplier;
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
       //OnMoveReceived?.Invoke();
    }

    public void RecieveMoveAnimation()
    {
        OnMoveReceived?.Invoke();
    }

    public void StartMovement()
    {
        IsMoving = true;
        OnMovement?.Invoke();
    }
}
