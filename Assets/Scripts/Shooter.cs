using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooter : MonoBehaviour
{
    public event Action OnShoot;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform muzzle;

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
        if (Input.GetMouseButtonDown(0))
        {
            Bullet bullet = bulletPool.GetAvailableObject(muzzle.position, muzzle.rotation).GetComponent<Bullet>();
            bullet.Shoot();
            OnShoot?.Invoke();
        }    
    }
}
