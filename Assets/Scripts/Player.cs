using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(moveInput * Time.deltaTime * 10f);
    }
}
