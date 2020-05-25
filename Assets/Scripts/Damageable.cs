using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    float maxHealth;

    private float health;

    private Renderer RenderColor;

    private void Awake()
    {
        health = maxHealth;
        RenderColor = GetComponentInChildren<Renderer>();
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Destroy(gameObject);

        RenderColor.material.color = Color.Lerp(Color.white, Color.red, health / maxHealth);
    }
}
