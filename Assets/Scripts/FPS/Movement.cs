using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;

    [SerializeField]
    private float movementSpeed = 15.0f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
            movementSpeed *= 3;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
            movementSpeed /= 3;

        Vector3 move = (controller.transform.right * Input.GetAxis("Horizontal")) + (controller.transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * movementSpeed);
    }
}
