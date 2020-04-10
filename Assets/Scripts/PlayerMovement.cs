using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField]
    private float speed = 10.0f;

    //Distance between a lane
    [SerializeField]
    private float MoveDistance = 3.5f;
    private float currentlane = 1;

    

    void Awake()
    {
        List<LaneController.Lane> lanes = FindObjectOfType<LaneController>().propLevelSegments[0].lanes;
        //sets player position to middle lane (Had to offset quad by -3.5 units though)
        transform.position = lanes[lanes.Count / 2].laneSegments[0].transform.position;
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentlane++;
            if (currentlane == 3)
                currentlane = 2; 
        }

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentlane--;
            if (currentlane == -1)
                currentlane = 0; 
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
       // Debug.Log(targetPosition);

        //Left most lane then move player to the left
        if (currentlane == 0)
        {
        targetPosition += Vector3.left * MoveDistance;
      
        }

        //Right most lane then move player to the left
        else if (currentlane == 2)
        {
         targetPosition += Vector3.right * MoveDistance;

        }
        transform.position = targetPosition;

    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

}
