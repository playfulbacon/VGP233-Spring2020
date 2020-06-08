using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int mMaxHealth = 100;

    public int CurrentHealth { get; set; }

    public int MaxHealth => mMaxHealth;

    private void Awake()
    {
        CurrentHealth = mMaxHealth;
    }

}
