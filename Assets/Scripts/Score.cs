using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text distance;
    public Text coins; 
    public Player player;
    public PlayerMovement playerMovement;

    private float distanceTravelled; 
    public float propDistance { get { return distanceTravelled; }}

    void Update()
    {
        distanceTravelled = player.gameObject.transform.position.z;
        if (distance.text != null)
        {
            distance.text = distanceTravelled.ToString("F1") + "m";
        }
        coins.text = playerMovement.Coins.ToString();
    }
}
