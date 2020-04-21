using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    [SerializeField]
    CharacterController Controller;
    void Update()
    {
        Vector3 move = (Controller.transform.right * Input.GetAxis("Horizontal")) + (Controller.transform.forward * Input.GetAxis("Vertical"));
        Controller.Move(move * Time.deltaTime * speed);
    }
}
