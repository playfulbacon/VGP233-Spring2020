using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLogUI : MonoBehaviour
{
    public Text playerLog;
    public Text enemyLog;
    public Character player;
    public Character enemy;


    // Update is called once per frame
    void Update()
    {
        playerLog.text = player.Result;
        enemyLog.text = enemy.Result;
        if(player.isDead())
        {
            playerLog.text = player.propName + " died";
        }
        else if (enemy.isDead())
        {
            enemyLog.text = enemy.propName + " died";
        }
    }
}
