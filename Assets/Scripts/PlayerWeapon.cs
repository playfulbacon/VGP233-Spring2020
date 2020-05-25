using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    PlayerAnimationEventHandler animationEventHandler;

    private bool canDamage;
    private readonly List<Damageable> hitDamageables = new List<Damageable>();

    private float damageModifier;

    private void Awake()
    {
        damageModifier = FindObjectOfType<Player>().DamageModifier;

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
                damageable.Damage(1f * damageModifier);
                hitDamageables.Add(damageable);
            }
        }
    }
}
