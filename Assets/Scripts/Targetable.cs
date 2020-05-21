using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    private Damageable damageable;

    private bool isOnFire = false;
    public bool IsOnFire { get { return isOnFire; } set { isOnFire = value; } }

    private bool damageWait = false;

    void Awake()
    {
        damageable = GetComponent<Damageable>();
    }
    
    void Update()
    {
        // if burning
        if (isOnFire && !damageWait)
        {
            if (damageable)
            {
                damageable.Damage(1.0f);
                damageWait = true;
                StartCoroutine(Burn());
            }
        }
    }

    IEnumerator Burn()
    {
        yield return new WaitForSeconds(2);
        damageWait = false;
    }
}
