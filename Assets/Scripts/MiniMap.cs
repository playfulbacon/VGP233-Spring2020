using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    private Transform transformToFollow;

    [SerializeField]
    private float height;

    private void Update()
    {
        Vector3 position = transformToFollow.position;
        position.y = height;
        transform.position = position;
    }
}
