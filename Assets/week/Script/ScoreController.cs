using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public event System.Action<int> OnScoreChanged;
    private int score = 0;

    void Start()
    {
    }

    void Update()
    {

    }

    public void AddScore(int add)
    {
        score += add;
        OnScoreChanged?.Invoke(score);
    }
}
