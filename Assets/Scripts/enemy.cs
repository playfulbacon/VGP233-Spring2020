using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DestoryEnemy()
    {
        FindObjectOfType<ScoreController>().AddScore(1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Bullet"))
        {
            DestoryEnemy();
        }
    }
}
