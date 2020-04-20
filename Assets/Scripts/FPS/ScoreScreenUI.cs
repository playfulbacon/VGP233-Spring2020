using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenUI : MonoBehaviour
{
    public GameUI inGameUI;
    public Movement playerMovement;
    public Text timeLeft;
    public Text targetsHit;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        timeLeft.text = inGameUI.GetCountDownTimer.ToString("F1") + "s";
        targetsHit.text = Target.Instance.getTargetshitCount.ToString();
        inGameUI.gameObject.SetActive(false);
        playerMovement.gameObject.SetActive(false);
    }
    private void Update()
    {
        float calculateScore = inGameUI.GetCountDownTimer * 10 + (Target.Instance.getTargetshitCount * 20);
        score.text = calculateScore.ToString("F1");
    }

}
