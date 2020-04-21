using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    bool ItCanMove = false;
    [SerializeField]
    float enemySpeed = 2.5f;
    [SerializeField]
    GameObject[] waypoints;
    int current = 0;
    float WPreadius = 1;

    void Update()
    {
        if (ItCanMove)
        {
            if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPreadius)
            {
                current++;
                if (current >= waypoints.Length)
                {
                    current = 0;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * enemySpeed);
        }
    }

    public void DestoryEnemy()
    {
        FindObjectOfType<ScoreController>().AddScore(1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Bullet"))
        {
            DestoryEnemy();
        }
    }
}
