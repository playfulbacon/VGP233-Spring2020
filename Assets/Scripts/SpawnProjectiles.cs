using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject FirePoint;
    [SerializeField] private GameObject playerRotation;
    [SerializeField] private List<GameObject> vfx = new List<GameObject>();

    private GameObject effectToSpawn;
    private float timeToFire = 0f;

    private float FireRate = 0f;

    private void Start()
    {

        effectToSpawn = vfx[0];
        FireRate = effectToSpawn.GetComponent<ProjectileMove>().FireRate;
    }

    public void SpawnVfx()
    {
        if (FirePoint & Time.time >= timeToFire)
        {
            timeToFire = (Time.time + 1f) / FireRate;
            GameObject vfx = Instantiate(effectToSpawn, FirePoint.transform.position, Quaternion.identity);
            vfx.transform.localRotation = playerRotation.transform.rotation;
        }
    }
}
