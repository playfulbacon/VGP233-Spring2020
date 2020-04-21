using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField]
    Text ScoreText;

    private void Awake()
    {
        FindObjectOfType<ScoreController>().OnScoreChanged += (x) => { ScoreText.text = "score: " + x.ToString(); };
    }

    void Update()
    {
        
    }
}
