using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    void Start()
    {
        List<LaneController.Lane> lanes = FindObjectOfType<LaneController>().LevelSegments[0].lanes;
        transform.position = lanes[lanes.Count / 2].laneSegments[0].transform.position;
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

    }
}
