using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private PlayerStats playerStats;
    public Slider healthBar;
    public Slider manaBar;
    // Start is called before the first frame update
    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerStats.onDamage += UpdateHealthBar;
        playerStats.onSpellCast += UpdateManaBar;
        playerStats.onRevive += UpdateStats;
    }

    void UpdateStats()
    {
        UpdateHealthBar();
        UpdateManaBar();
    }

    void UpdateHealthBar()
    {
        healthBar.value = playerStats.CalculateHealth();
    }
    void UpdateManaBar()
    {
        manaBar.value = playerStats.CalculateMana();
    }

    /*
        void UpdateHealthBar()
    {
        StartCoroutine(UpdateHealth(playerStats.CurrentHealth));
    }

    private IEnumerator UpdateHealth(float health)
    {
        float sliderValue = healthBar.value;
        float elapsedtime = 0.0f;
        while(elapsedtime < updatedSpeed)
        {
            elapsedtime += Time.deltaTime;
            healthBar.value = Mathf.Lerp(sliderValue, 2, elapsedtime / updatedSpeed);
            yield return null;
        }
    }
     */
}

