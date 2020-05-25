using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    int damage = 1;
    float delayTime;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
            if (delayTime <= Time.time)
            {
                delayTime = Time.time + 1.0f;
                other.gameObject.GetComponent<Player>().takeDamage(damage);
            }
        }
    }
}
