using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public event Action OnDestroy;
    private float speed = 300f;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Target")
        {
            Debug.Log("Hit");
            Destroy(collision.gameObject);
        }
    }

    private void Destroy()
    {
        OnDestroy?.Invoke();
    }
}
