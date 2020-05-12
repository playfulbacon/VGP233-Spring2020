using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [SerializeField]
    public float speed = 1f;
    private ScoreController scoreController;
    

    void Start()
    {
        List<LaneController.Lane> lanes = FindObjectOfType<LaneController>().LevelSegments[0].lanes;
        transform.position = lanes[lanes.Count / 2].laneSements[0].transform.position;
        scoreController = FindObjectOfType<ScoreController>();
        InvokeRepeating("SpeedUp",5.0f,3.0f);
    }

    void Update()
    {
        if(LaneController.isGameFinish)
        {
            List<LaneController.Lane> lanes = FindObjectOfType<LaneController>().LevelSegments[0].lanes;
            transform.position = lanes[lanes.Count / 2].laneSements[0].transform.position;
        }
        else
        {
            List<LaneController.Lane> lanes = FindObjectOfType<LaneController>().LevelSegments[0].lanes;
            transform.position += transform.forward * Time.deltaTime * speed;
            if(Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x <= 1.5)
            {
                transform.position += Vector3.right * 1.5f;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x >= -1.5)
            {
                transform.position += Vector3.left * 1.5f;
            }
        }
    }

    private void SpeedUp()
    {
        speed += 1.0f;
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            LaneController.isGameFinish = true;
            SceneManager.LoadScene("Week1");
        }
        else if (col.gameObject.tag == "Coin")
        {
            scoreController.AddScore(1);
            Destroy(col.gameObject.transform.parent.gameObject);
        }
    }
}
