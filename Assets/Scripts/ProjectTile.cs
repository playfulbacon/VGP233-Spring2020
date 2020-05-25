using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    float destoryTime = 10.0f;

    private void Awake()
    {
        Invoke("destoryGameObject", destoryTime);
    }


    void destoryGameObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();

        if (damageable != null)
        {
            damageable.Damage(1f * FindObjectOfType<Player>().DamageModifier);
            Destroy(gameObject);
        }
    }
}
