using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLogUI : MonoBehaviour
{
    public Text playerLog;
    public Text enemyLog;
    private PartySystem party;
    public Character enemy;
    private Character player;
    private void Start()
    {
        party = FindObjectOfType<PartySystem>();   
    }

    // Update is called once per frame
    void Update()
    {
        player = party.partyList[party.playerIndex].gameObject.GetComponent<Character>();
        playerLog.text = player.Result;
        enemyLog.text = enemy.Result;

        if(isSomeoneDead())
            FindObjectOfType<BattleController>().enabled = false;
   
    }

    bool isSomeoneDead()
    {
        if (player.isDead())
        {
            playerLog.text = player.propName + " died";
            return true;
        }
        else if (enemy.isDead())
        {

            enemyLog.text = enemy.propName + " died";
            return true;
        }
        return false;
    }
}
