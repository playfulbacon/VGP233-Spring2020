using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    public Player player;

    private float distanceTravelled; 
    public float propDistance { get { return distanceTravelled; }}

    void Update()
    {
        distanceTravelled = player.gameObject.transform.position.z;
        score.text = distanceTravelled.ToString("F1") + "m";
    }
}
