using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooter : MonoBehaviour
{
    public event Action OnShoot;

    [SerializeField]
    float speed = 5.0f;

    [SerializeField]
    GameObject bulletPrefab = null;

    [SerializeField]
    GameObject muzzle = null;

    private ObjectPool bulletPool;

    private void Start()
    {
        bulletPool = FindObjectOfType<ObjectPoolManager>().CreateNewObjectPool(bulletPrefab, 50);

        foreach (GameObject obj in bulletPool.Objects)
        {
            Bullet bullet = obj.GetComponent<Bullet>();
            obj.GetComponent<Bullet>().OnDestroy += () => { bulletPool.ReturnObjectToPool(bullet.gameObject); };
        }
    }
    
    private void Update()
    {
        // Shoot
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObj = bulletPool.GetAvailableObect(muzzle.transform.position, muzzle.transform.rotation);
            if (bulletObj != null)
            {
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.Shoot();
                OnShoot?.Invoke();
            }
        }
        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            bulletPool.Reload();
        }
    }
}
