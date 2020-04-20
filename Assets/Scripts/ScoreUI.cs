using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    Text scoreText;

    private void Awake()
    {
        FindObjectOfType<ScoreController>().OnScoreChanged += (x) => { scoreText.text = "Score: " + x.ToString(); };
    }

    void Update()
    {
        
    }
}
