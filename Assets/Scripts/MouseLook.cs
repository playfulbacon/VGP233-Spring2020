using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float mouseSensitivity = 100f;

    [SerializeField]
    Transform player;

    [SerializeField]
    Camera cam;

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);

        // Move
        //if (Input.GetKey(KeyCode.W))
        //{
        //    player.transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + (speed * Time.deltaTime));
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    player.transform.localPosition = new Vector3(player.transform.position.x - (speed * Time.deltaTime), player.transform.position.y, player.transform.position.z);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    player.transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - (speed * Time.deltaTime));
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    player.transform.localPosition = new Vector3(player.transform.position.x + (speed * Time.deltaTime), player.transform.position.y, player.transform.position.z);
        //}

        // Gotten from internet. Need to understand how it works, better.
        player.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        player.transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
    }
}
