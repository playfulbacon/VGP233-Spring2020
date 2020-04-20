using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{
    public Shooter shooter;
    public Text maxBulletCount;
    public Text currentBulletCount;
    public Text targetCount;
    public Text timeRemaining;
    public GameObject scoreScreen; 
    private float countDownTimer = 30.0f;
    public float GetCountDownTimer { get { return countDownTimer; } }
    // Start is called before the first frame update
    void Start()
    {
        maxBulletCount.text = "/ " + shooter.GetMaxClipSize.ToString();
        currentBulletCount.text = shooter.GetMaxClipSize.ToString();
        targetCount.text = Target.Instance.targetListCount.ToString();
        timeRemaining.text = countDownTimer.ToString("F1") + "s";
    }

    void Update()
    {
        countDownTimer -= Time.deltaTime;
        timeRemaining.text = countDownTimer.ToString("F1") + "s";
        if((int)countDownTimer < 10)
        {
            timeRemaining.GetComponent<Text>().color = Color.red;
        }
        currentBulletCount.text = shooter.GetCurrentBullets.ToString();
        targetCount.text = Target.Instance.targetListCount.ToString();
        if((int)countDownTimer == 0)
        {
            scoreScreen.SetActive(true);
        }
        if(Target.Instance.targetListCount == 0)
        {
            scoreScreen.SetActive(true);
        }

    }
}
