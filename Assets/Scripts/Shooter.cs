using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooter : MonoBehaviour
{
    public event Action OnShoot;
    public event Action<int> OnShootChanged;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform muzzle;

    [SerializeField]
    public int ammo = 7;

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
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            Bullet bullet = bulletPool.GetAvailableObject(muzzle.position, muzzle.rotation).GetComponent<Bullet>();
            bullet.Shoot();
            OnShoot?.Invoke();
            ammo -= 1;
            OnShootChanged?.Invoke(ammo);
        }

        if (Input.GetKey(KeyCode.R))
        {
            ammo = 7;
        }
    }
}
