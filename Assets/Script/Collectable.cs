using UnityEngine;

public class Collectable : MonoBehaviour
{
    private readonly float mSpeed = 100;

    void Update()
    {
        transform.Rotate(0.0f, mSpeed * Time.deltaTime, 0.0f);
    }
}