using UnityEngine;

public class EnemyStompArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Enemy enemy = GetComponentInParent<Enemy>();
            enemy.IsStomp = true;
        }
    }

}
