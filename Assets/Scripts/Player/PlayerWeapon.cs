using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    PlayerAnimationEventHandler animationEventHandler;

    private bool canDamage;
    private List<EnemyStats> hitDamageables = new List<EnemyStats>();

    private void Awake()
    {
        animationEventHandler.OnStartDamageWindow += () => { canDamage = true; };
        animationEventHandler.OnStopDamageWindow += () => { canDamage = false; hitDamageables.Clear(); };
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.GetComponent<EnemyStats>().TakeDamage(FindObjectOfType<PlayerStats>().BaseDamage);
    }

    private void OnTriggerStay(Collider collider)
    {

        if (canDamage)
        {
            EnemyStats damageable = collider.GetComponent<EnemyStats>();

            if (damageable != null && !hitDamageables.Contains(damageable))
            {
                Debug.Log("hit");
                damageable.TakeDamage(FindObjectOfType<PlayerStats>().BaseDamage * FindObjectOfType<PlayerStats>().DamageModifier);
                hitDamageables.Add(damageable);
            }
        }
    }
}
