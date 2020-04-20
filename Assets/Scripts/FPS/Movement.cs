using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private float movementSpeed = 15.0f;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed *= 3;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed /= 3;
        }
        if(Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(Vector3.right * -movementSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        }
    }
}
