using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public event System.Action Onshoot;

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject muzzle;

    private ObjectPool bulletPool;
    private void Start()
    {
        bulletPool = FindObjectOfType<ObjectPoolManager>().CreateNewObjectPool(bulletPrefab, 50);

        foreach(GameObject obj in bulletPool.Objects)
        {
            Bullet bullet = obj.GetComponent<Bullet>();
            bullet.OnDestroy += () => { bulletPool.ReturnObjectToPool(bullet.gameObject); };
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Bullet bullet =  bulletPool.GetAvailableObject(muzzle.transform.position, muzzle.transform.rotation).GetComponent<Bullet>();
            bullet.Shoot();
            Onshoot?.Invoke();
        }
    }
}
