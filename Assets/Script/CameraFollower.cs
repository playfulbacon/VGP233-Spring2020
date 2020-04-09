using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform follow;

    [SerializeField]
    private Vector3 cameraOffSet;

    void Update()
    {
        transform.position = follow.transform.position + cameraOffSet;
    }
}
