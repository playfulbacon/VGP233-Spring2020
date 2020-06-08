using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchPlayerJump : MonoBehaviour
{
    [SerializeField]
    string twitchMessageToJump = "jump";

    [SerializeField]
    PlayerController player;

    private void Awake()
    {
        TwitchChat twitchChat = FindObjectOfType<TwitchChat>();
        twitchChat.OnMessageReceived += (x) => AttemptJump(x);
    }

    void AttemptJump(string twitchMessage)
    {
        Debug.Log("attempt player jump");
        if (twitchMessage.ToLower() == twitchMessageToJump.ToLower())
        {
            player.Jump();
        }
    }
}
