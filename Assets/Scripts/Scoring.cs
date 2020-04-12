using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    Text prefabScore;

    Text score;
    GameObject player;

    private void Awake()
    {
        // Player
        player = GameObject.FindWithTag("Player");
        // Setting Score
        score = Instantiate(prefabScore, Vector3.zero, Quaternion.identity);
        score.transform.SetParent(transform, false);
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        int newScore = (int)player.transform.position.z;
        score.text = string.Format("Score: {0}", newScore); ;
        //Debug.Log(player.transform.position.z);
    }
}
