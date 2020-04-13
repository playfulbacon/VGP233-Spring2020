using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public Text inGameDistance;
    public Text inGameCoinText;
    public Text InGameCoins;
    public Text Distance;
    public Text Coins;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {                        
            Distance.text = inGameDistance.text;
            Coins.text = InGameCoins.text;
            Destroy(inGameDistance);
            Destroy(InGameCoins);
            Destroy(inGameCoinText);
            //Stops the player from going forward
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }
}
