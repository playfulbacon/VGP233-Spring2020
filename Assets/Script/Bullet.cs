using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public event Action OnDestroy;
    public event Action OnShoot;
    public event Action OnBodyCount;

    private readonly float speed = 200f;
    private Rigidbody rb;

    public void Shoot()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Invoke("Destroy", 2f);
    }

    private void Destroy()
    {
        OnDestroy?.Invoke();
    }

    public void BodyCount()
    {
        OnBodyCount?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Invoke("BodyCount", 0f);
        }
    }
}
