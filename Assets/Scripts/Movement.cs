using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    CharacterController charCtrl;

    private float movementSpeed = 15.0f;

    private void Start()
    {
        
    }
    
    private void Update()
    {
        Vector3 move = (charCtrl.transform.right * Input.GetAxis("Horizontal")) + (charCtrl.transform.forward * Input.GetAxis("Vertical"));
        charCtrl.Move(move * Time.deltaTime * movementSpeed);
    }
}
