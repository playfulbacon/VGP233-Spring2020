using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchEnemySpawner : MonoBehaviour
{
    [SerializeField]
    string twitchMessageToSpawn = "enemy";

    [SerializeField]
        GameObject enemyPrefab;

    PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();

        TwitchChat twitchChat = FindObjectOfType<TwitchChat>();
        twitchChat.OnMessageReceived += (x) => AttemptEnemySpawn(x);
    }

    void AttemptEnemySpawn(string twitchMessage)
    {
        Debug.Log("attempt enemy spawn");
        if (twitchMessage.ToLower() == twitchMessageToSpawn.ToLower())
        {
            Instantiate(enemyPrefab, new Vector3(player.transform.position.x, player.transform.position.y + 3f, 0f), Quaternion.identity);

        }
    }
}
