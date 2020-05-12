using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    PlayerAnimationEventHandler animationEventHandler;

    private List<Damageable> hitDamageables = new List<Damageable>();

    private bool canDamage;

    private void Awake()
    {
        animationEventHandler.OnStartDamageWindow += () => { canDamage = true; };
        animationEventHandler.OnStopDamageWindow += () => { canDamage = false; hitDamageables.Clear(); };
    }

    private void OnTriggerStay(Collider collider)
    {
        if (canDamage)
        {
            Damageable damageable = collider.GetComponent<Damageable>();

            if (damageable != null && !hitDamageables.Contains(damageable))
            {
                damageable.Damage(1.0f);
                hitDamageables.Add(damageable);
            }
        }
    }
}
