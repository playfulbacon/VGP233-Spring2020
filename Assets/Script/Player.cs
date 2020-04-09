using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    private List<LaneController.Lane> lanes;

    private int mSwitchLanes = 0;
    void Start()
    {
        //List<LaneController.Lane> lanes = FindObjectOfType<LaneController>().LevelSegments[0].lanes;
        lanes = FindObjectOfType<LaneController>().LevelSegments[0].lanes;
        transform.position = lanes[lanes.Count >> 1].laneSegments[0].transform.position;
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (mSwitchLanes > 0)
            {
                mSwitchLanes--;
                transform.position = lanes[mSwitchLanes].laneSegments[0].transform.position;
            }

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (mSwitchLanes < lanes.Count - 1)
            {
                mSwitchLanes++;
                transform.position = lanes[mSwitchLanes].laneSegments[0].transform.position;
            }
        }

    }
}
