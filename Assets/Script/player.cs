using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    public float speed = 1f;


    void Start()
    {
        List<LaneController.Lane> lanes = FindObjectOfType<LaneController>().LevelSegments[0].lanes;
        transform.position = lanes[lanes.Count / 2].laneSements[0].transform.position;
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

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            LaneController.isGameFinish = true;
        }
    }
}
