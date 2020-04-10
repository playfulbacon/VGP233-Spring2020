using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    void Start()
    {
        List<LaneController.Lane> lanes = FindObjectOfType<LaneController>().propLevelSegments[0].lanes;
        transform.position = lanes[lanes.Count / 2].laneSegments[0].transform.position;
        Debug.Log(transform.position);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 10.0f;
    }
}
