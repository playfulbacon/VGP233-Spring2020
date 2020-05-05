using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    float moveSpeed = 7f;

    [SerializeField]
    float jumpHeight = 4f;

    private CharacterController characterController;
    private Vector3 moveDirection;

    [SerializeField]
    float gravity = 20f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController.isGrounded)
        {
            //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            Vector3 forward = cam.transform.forward.normalized;
            forward.y = 0f;
            Vector3 right = cam.transform.right.normalized;
            right.y = 0f;
            
            moveDirection = (forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"));

            moveDirection *= moveSpeed;

            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpHeight;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
