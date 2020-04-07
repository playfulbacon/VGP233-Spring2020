using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    Transform follow;

    [SerializeField]
    Vector3 cameraOffeset;

    void Start()
    {
        
    }
    
    void Update()
    {
        transform.position = follow.transform.position + cameraOffeset;
    }
}
