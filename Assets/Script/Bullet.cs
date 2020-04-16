using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event System.Action OnDestroy;
    [SerializeField]
    float speed = 100f;
    private Rigidbody rb;

    public void Shoot()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Invoke("Destroy", 2f);
    }

    private void Update()
    {
        //transform.position += transform.forward * Time.deltaTime * speed;
    }

    public void Destroy()
    {
        OnDestroy?.Invoke();
    }
}
