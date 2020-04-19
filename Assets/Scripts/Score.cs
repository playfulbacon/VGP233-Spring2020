using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreTxt;
    private int Points = 0;

    private GameObject[] targets;

    private void Start()
    {
        scoreTxt = GetComponentInChildren<Text>();
        if (targets == null)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
        }
        foreach (GameObject target in targets)
        {
            target.GetComponent<Target>().OnHit += () => { ScorePoints(); };
        }
    }
    
    private void Update()
    {
        scoreTxt.text = " Score: " + Points;
    }

    private void ScorePoints()
    {
        Points += 1;
    }
}
