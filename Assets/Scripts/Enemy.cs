using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnPatrolStart, OnChaseStart;

    enum State { None, Patrol, Chase };
    private State state;

    [SerializeField]
    private float sightLength = 5f;

    [SerializeField]
    private LayerMask enemyVisibleMask;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    Transform patrolPointHolder;

    private List<Vector3> patrolPointPositions = new List<Vector3>();
    private int patrolPointIndex = 0;
    private Transform chaseTarget;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (Transform child in patrolPointHolder)
        {
            patrolPointPositions.Add(child.position);
        }
    }

    void Start()
    {
        ChangeState(State.Patrol);
    }

    private void Patrol()
    {
        Vector3 targetPosition = patrolPointPositions[patrolPointIndex];
        if (rb.position == (Vector2)targetPosition)
        {
            patrolPointIndex = ++patrolPointIndex % patrolPointPositions.Count;
            Vector3 directiontoPatrolPoint = patrolPointPositions[patrolPointIndex] - transform.position;
            transform.right = directiontoPatrolPoint;
        }
        rb.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void Chase()
    {
        // enemy staus grounded ( doesn't jump with the player)
        // enemy faces target direction
        Vector3 chaseTargetPosition = chaseTarget.position;
        chaseTargetPosition.y = transform.position.y;

        Vector3 directionToChaseTarget = chaseTargetPosition - transform.position;
        transform.right = directionToChaseTarget;

        transform.position = Vector3.MoveTowards(transform.position, chaseTargetPosition, speed * Time.deltaTime);
    }


    private void Update()
    {
        Vector2 direction = transform.right;
        // raycast in the line of sight, detect when player is green
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, sightLength, enemyVisibleMask);

        if (hit.collider)
        {
            PlayerController playerController = hit.transform.GetComponent<PlayerController>();
            // if playerController is not null, we can see the player
            if (playerController)
            {
                chaseTarget = playerController.transform;
                ChangeState(State.Chase);
            }
            else
            {
                ChangeState(State.Patrol);
            }
        }
        else
        {
            ChangeState(State.Patrol);
        }

        switch (state)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase: Chase(); break;
            default: break;
        }
    }

    private void ChangeState(State newState)
    {
        if (newState == state)
        {
            return;
        }

        // end the current state

        // start the next state
        switch (newState)
        {
            case State.Patrol: OnPatrolStart?.Invoke(); break;
            case State.Chase: OnChaseStart?.Invoke(); break;
            default: break;
        }

        state = newState;
    }
}
