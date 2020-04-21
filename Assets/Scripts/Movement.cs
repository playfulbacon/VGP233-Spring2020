using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    [SerializeField]
    CharacterController Controller;
    [SerializeField]
    GameObject player;
    void Update()
    {
        player.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        player.transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        Vector3 move = (Controller.transform.right * Input.GetAxis("Horizontal")) + (Controller.transform.forward * Input.GetAxis("Vertical"));
        Controller.Move(move * Time.deltaTime * speed);
    }
}
