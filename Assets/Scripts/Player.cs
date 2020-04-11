using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    private Vector3 direction;
    private float posZ;
    public float getDistannce { get { return posZ; } }
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //List<LaneController.Lane> lanes = FindObjectOfType<LaneController>().propLevelSegments[0].lanes;
        //transform.position = lanes[lanes.Count / 2].laneSegments[0].transform.position;
        transform.position += transform.forward * 0.5f;    
       Debug.Log(transform.position);
    }

    private void Update()
    {
        posZ = transform.position.z;
    }


}
