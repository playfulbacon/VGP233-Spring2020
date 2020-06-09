using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField]
    private Transform transformToFollow;

    [SerializeField]
    private float height = 10f;

    [SerializeField]
    private float cameraSize = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Camera>().orthographicSize = cameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transformToFollow.position;
        position.y = height;
        transform.position = position;
    }
}
