using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColor : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Color chaseColor, patrolColor;

    private void Awake()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.OnChase += () => ChangeColor(chaseColor);
        enemy.OnPatrol += () => ChangeColor(patrolColor);
    }
    
    private void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
