using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        //&& transform.position.y -0.5 > enemy.transform.position.y +0.5
        if (enemy != null )
        {
            enemy.KillEnemy();
        }
    }
}
