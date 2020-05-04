using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLogUI : MonoBehaviour
{
    public Text playerLog;
    public GameObject panel;
    private PartySystem party;
    public Character enemy;
    private Character player;
    private void Start()
    {
        party = FindObjectOfType<PartySystem>();   
    }

    public void ShowBattleLog()
    {
        if(!panel.activeSelf)
        panel.SetActive(true);
        else
        panel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        player = party.partyList[party.playerIndex].gameObject.GetComponent<Character>();
        if (player.GetSpeed > enemy.GetSpeed)       
            playerLog.text = player.Result + "\n" + enemy.Result;
        else
            playerLog.text = enemy.Result + "\n" + player.Result;



        if (isSomeoneDead())
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

            playerLog.text = enemy.propName + " died";
            return true;
        }
        return false;
    }
}
