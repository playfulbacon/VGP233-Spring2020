using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchPlayerGiveHealth : MonoBehaviour
{
    [SerializeField]
    string twitchMessageToHeal = "heal";

    [SerializeField]
    Player player;

    private void Awake()
    {
        TwitchChat twitchChat = FindObjectOfType<TwitchChat>();
        twitchChat.OnMessageReceived += (x) => AttemptHeal(x);
    }

    void AttemptHeal(string twitchMessage)
    {
        Debug.Log("attempt player heal");
        if (twitchMessage.ToLower() == twitchMessageToHeal.ToLower())
        {
            player.CurrentHealth++;
        }
    }
}
