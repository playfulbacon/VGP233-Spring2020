using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event System.Action OnPatrol, OnChase;

    private enum State
    {
        None,
        Patrol,
        Chase
    }

    private State state;

    [SerializeField]
    private LayerMask enemyVisibleMask;

    [SerializeField]
    private float sightLength = 5.0f;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private Transform patrolPointsHolder;

    private List<Vector3> patrolPointPositions = new List<Vector3>();
    private int patrolPointIndex = 0;
    private Transform chaseTarget;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        foreach (Transform child in patrolPointsHolder)
            patrolPointPositions.Add(child.position);
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
            patrolPointIndex = (patrolPointIndex < patrolPointPositions.Count - 1) ? patrolPointIndex + 1 : 0;
            // determine the direction from the enemy to the patrol point
            Vector3 directionToPatrolPoint = patrolPointPositions[patrolPointIndex] - transform.position;
            transform.right = directionToPatrolPoint;
        }

        rb.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        // enemy faces target direction
    }

    private void Chase()
    {
        Vector3 chaseTargetPosition = chaseTarget.position;
        chaseTargetPosition.y = transform.position.y;

        Vector3 directionToChaseTarget = chaseTargetPosition - transform.position;
        transform.right = directionToChaseTarget;

        rb.position = Vector3.MoveTowards(transform.position, chaseTargetPosition, speed * Time.deltaTime);
    }
    
    void Update()
    {
        // raycast in the line of sight, detect when player is seen

        Vector2 direction = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, sightLength, enemyVisibleMask);
        //canSeePlayer = hit.collider;

        if (hit.collider != null)
        {
            PlayerController playerController = hit.transform.GetComponent<PlayerController>();
            // if playerController is not null, we can see the player

            if (playerController != null)
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
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }

    void ChangeState(State newState)
    {
        if (newState == state)
            return;

        // starte new state
        switch (newState)
        {
            case State.Patrol:
                OnPatrol?.Invoke();
                break;
            case State.Chase:
                OnChase?.Invoke();
                break;
        }

        state = newState;
    }
}
