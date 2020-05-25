using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private float damage = 5.0f;
   public float TrapDamage { get { return damage; } }
}
