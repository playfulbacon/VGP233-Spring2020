using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    List<LaneController.Lane> lanes;
    int currentLane;
    
    void Start()
    {
        lanes = FindObjectOfType<LaneController>().LevelSegments[0].lanes;
        transform.position = lanes[lanes.Count / 2].laneSegments[0].transform.position;
        currentLane = lanes.Count / 2;
    }
    
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        // Switch Lanes
        if (currentLane > 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            transform.position = new Vector3(lanes[currentLane - 1].laneSegments[0].transform.position.x, transform.position.y, transform.position.z);
            currentLane -= 1;
        }
        else if (currentLane < (lanes.Count - 1) && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            transform.position = new Vector3(lanes[currentLane + 1].laneSegments[0].transform.position.x, transform.position.y, transform.position.z);
            currentLane += 1;
        }

        speed *= 1.0002f;
    }

    private void OnCollisionEnter(Collision coin)
    {
        if (coin.gameObject.tag == "Coin")
        {
            speed *= 1.2f;
            Destroy(coin.gameObject);
        }
    }
}
