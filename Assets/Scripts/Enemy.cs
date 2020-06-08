using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action OnPatrolStart, OnChaseStart;

    private enum State { None, Patrol, Chase };

    [SerializeField]
    private LayerMask enemyVisibleMask = default;

    [SerializeField]
    float sightLength = 5f;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    private int AttackPower = 10;

    [SerializeField]
    private Transform patrolPointsHolder = default;

    private readonly List<Vector3> patrolPointPositions = new List<Vector3>();
    private State state;
    private int patrolPointIndex = 0;
    private Transform chaseTarget;
    private Rigidbody2D rb;

    public bool IsStomp { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (Transform child in patrolPointsHolder)
            patrolPointPositions.Add(child.position);
    }

    private void Start()
    {
        ChangeState(State.Patrol);
    }

    private void Patrol()
    {
        Vector3 targetPosition = patrolPointPositions[patrolPointIndex];

        if (rb.position == (Vector2)targetPosition)
        {
            patrolPointIndex = patrolPointIndex < patrolPointPositions.Count - 1 ? patrolPointIndex + 1 : 0;
            Vector3 directionToPatrolPoint = patrolPointPositions[patrolPointIndex] - transform.position;
            transform.right = directionToPatrolPoint;
        }

        rb.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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
        Vector2 direction = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, sightLength, enemyVisibleMask);

        if (hit.collider != null)
        {
            PlayerController playerController = hit.transform.GetComponent<PlayerController>();

            if (playerController != null)
            {
                chaseTarget = playerController.transform;
                ChangeState(State.Chase);
            }
            else
                ChangeState(State.Patrol);
        }
        else
            ChangeState(State.Patrol);

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;
        }

        if (IsStomp)
        {
            Destroy(gameObject);
            IsStomp = false;
        }

    }

    private void ChangeState(State newState)
    {
        if (newState == state)
            return;

        switch (newState)
        {
            case State.Patrol:
                OnPatrolStart?.Invoke();
                break;

            case State.Chase:
                OnChaseStart?.Invoke();
                break;
        }

        state = newState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsStomp)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(AttackPower);
            }
        }
    }
}
