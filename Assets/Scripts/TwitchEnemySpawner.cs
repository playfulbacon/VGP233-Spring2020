using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchEnemySpawner : MonoBehaviour
{

    [SerializeField]
    private string twitchMessageToSpawn = "enemy";

    [SerializeField]
    private GameObject enemyPrefabs;

    private void Awake()
    {
        TwitchChat twitchChat = FindObjectOfType<TwitchChat>();
        twitchChat.OnMessageReceived += (x) => AttemptEnemySpawn(x);
    }

    void AttemptEnemySpawn(string twitchMessage)
    {
        Debug.Log("[AttemptEnemySpawn]");
        if (twitchMessage.ToLower() == twitchMessageToSpawn.ToLower())
        {
            Instantiate(enemyPrefabs, transform.position, Quaternion.identity);
        }
    }

}
