using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private Slider magicBar;

    [SerializeField]
    private Text target;

    private void Awake()
    {
        player.OnRetarget += Retarget;
    }

    void Start()
    {
        healthBar.maxValue = player.HP;
        magicBar.maxValue = player.MP;
    }
    
    void Update()
    {
        healthBar.value = player.HP;
        magicBar.value = player.MP;
    }

    void Retarget(Targetable newTarget)
    {
        target.text = newTarget.name;
    }
}
