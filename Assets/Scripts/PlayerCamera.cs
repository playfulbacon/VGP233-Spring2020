using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    Vector3 offset;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    private void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.transform);
    }
}
