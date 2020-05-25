using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    [SerializeField]
    private float magicDamage = 20f;
    [SerializeField]
    private float magicCost = 10f;
    public float MagicCost { get { return magicCost; } }
    public float MagicDamage { get { return magicDamage; } set { magicDamage = value; } }
    [SerializeField]
    private float durationLength = 3f;
    private Rigidbody mRigidBody;

    private void Awake()
    {
        mRigidBody = GetComponent<Rigidbody>();
    
    }

    private void Update()
    {
        StartCoroutine(ObjectDuration());
    }

    IEnumerator ObjectDuration()
    {       
        yield return new WaitForSeconds(durationLength);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<EnemyStats>().TakeDamage(MagicDamage);
        Destroy(this.gameObject);
    }
}
