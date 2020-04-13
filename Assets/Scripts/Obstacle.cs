using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.tag == "Player")
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
