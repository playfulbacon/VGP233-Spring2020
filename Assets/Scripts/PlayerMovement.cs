using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    //Distance between a lane
    [SerializeField]
    private float MoveDistance = 3.5f;
    [SerializeField]
    private float jumpForce = 10.0f;
    [SerializeField]
    private float Gravity = -20.0f;
    [SerializeField]
    private float playerSpeed = 4.0f;

    private float currentlane = 1;
    private int speedIncrementer = 1; 
    private int lerpValue = 5;
    private int distanceThreshold = 30;
  

    void Start()
    {
        controller = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
        //col = GetComponent<CapsuleCollider>();

    }

    void Update()
    {
        direction.z = playerSpeed;
 
    
        if (controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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

        controller.Move(direction * Time.deltaTime);
        IncreaseSpeed();
        //Calculates our future position
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
        //Movement Left and Right but not smooth
       //transform.position = targetPosition;

        //Lerps the position from current position to target position for smoother movement.
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpValue * Time.fixedDeltaTime);
        //Stops Jittering when switching between lanes. 
        controller.center = controller.center;
    }

    void IncreaseSpeed()
    {  
        if((int)transform.position.z / speedIncrementer == distanceThreshold)
        {
            Debug.Log("Distance Reached Incrementing Speed");
            playerSpeed++;
            speedIncrementer++;
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            GameOverManager.gameOver = true; 
        }
    }
    private void Jump()
    {
        direction.y = jumpForce;
    }

}
