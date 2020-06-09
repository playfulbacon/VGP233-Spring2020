using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    Transform transformToFollow;

    [SerializeField]
    float height = 10;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 position = transformToFollow.position;
        position.y = height;
        transform.position = position;
    }
}
