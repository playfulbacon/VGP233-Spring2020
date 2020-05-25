using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private Slider enemyHealthBar;
    private EnemyStats enemy;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyHealthBar = GetComponentInChildren<Slider>();
        enemy = GetComponentInParent<EnemyStats>();
        enemy.onDamage += UpdateHealthBar;
    }
   

    void UpdateHealthBar()
    {
        enemyHealthBar.value = enemy.CalculateHealth();
    }

}
