using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private Slider playerHealth;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerHealth.maxValue = player.MaxHealth;
    }
    
    private void Update()
    {
        playerHealth.value = player.CurrentHealth;
    }
}
