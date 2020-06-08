using UnityEngine;

public class EnemyColor : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer = default;

    [SerializeField]
    private Color chaseColor = default, patrolColor = default;

    private void Awake()
    {
        Enemy enemy = GetComponent<Enemy>();
        enemy.OnChaseStart += () => ChangeColor(chaseColor);
        enemy.OnPatrolStart += () => ChangeColor(patrolColor);
    }

    private void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
