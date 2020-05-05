using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Camera cam;

    [SerializeField]
    float moveSpeed = 7f;

    [SerializeField]
    float jumpHeight = 10f;

    [SerializeField]
    float gravity = 9.8f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cam = FindObjectOfType<Camera>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 2f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= 2f;
        }

        if (characterController.isGrounded)
        {
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
