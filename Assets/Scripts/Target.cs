using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    public event Action OnHit;

    private void OnCollisionEnter(Collision bullet)
    {
        if (bullet.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
            OnHit?.Invoke();
        }
    }
}