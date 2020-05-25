using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] public float speed = 20f;
    [SerializeField] public float FireRate = 4f;
    [SerializeField] public GameObject MuzzePrefabs;
    [SerializeField] public GameObject HitPrefabs;

    private Damageable damageable;

    private void Start()
    {
        if (MuzzePrefabs)
        {
            var muzzeVFX = Instantiate(MuzzePrefabs, transform.position, Quaternion.identity);
            muzzeVFX.transform.forward = gameObject.transform.forward;

            var posMuzzle = muzzeVFX.GetComponent<ParticleSystem>();
            if (posMuzzle)
            {
                Destroy(muzzeVFX, posMuzzle.main.duration);
            }
            else
            {
                var posChild = muzzeVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzeVFX, posChild.main.duration);
            }
        }
    }

    private void Update()
    {
        if (speed != 0f)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0f;
        if (HitPrefabs)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            var hitVFX = Instantiate(HitPrefabs, pos, rot);
            var posHit = hitVFX.GetComponent<ParticleSystem>();
            if (posHit)
            {
                Destroy(hitVFX, posHit.main.duration);
            }
            else
            {
                var posChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, posChild.main.duration);
                Damageable damage = collision.gameObject.GetComponent<Damageable>();
                if (damage)
                {
                    damage.Damage(1f);
                }

            }
        }

        Destroy(gameObject);
    }
}
