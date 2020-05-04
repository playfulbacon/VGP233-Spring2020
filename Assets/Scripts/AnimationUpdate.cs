using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUpdate : MonoBehaviour
{
    BattleController battleController;

    private void Start()
    {
        battleController = FindObjectOfType<BattleController>();
    }

    private void UpdateDamage()
    {
        if (battleController.Enemy.gameObject != GetComponentInParent<Character>().gameObject)
        {
            battleController.Enemy.RecieveMoveAnimation();
        }
        else
        {
            battleController.Player.RecieveMoveAnimation();
        }
    }
}
