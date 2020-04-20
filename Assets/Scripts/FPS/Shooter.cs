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
    [SerializeField]
    private int currentBullets;
    [SerializeField]
    private int maxClipSize = 6;
    public int GetMaxClipSize { get { return maxClipSize; } }
    public int GetCurrentBullets { get { return currentBullets; } }

    private void Start()
    {
        //Multiply maxclip size by 2 incase player reloads and shoots faster than the objects are able to be added back to the pool
        bulletPool = FindObjectOfType<ObjectPoolManager>().CreateNewObjectPool(bulletPrefab, maxClipSize * 2);
        currentBullets = maxClipSize;
        Debug.Log("Starting Bullets: " + currentBullets);
        foreach(GameObject obj in bulletPool.Objects)
        {
            Bullet bullet = obj.GetComponent<Bullet>();
            bullet.OnDestroy += () => { bulletPool.ReturnObjectToPool(bullet.gameObject); };
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentBullets > 0)
        {
            Bullet bullet = bulletPool.GetAvailableObject(muzzle.position, muzzle.rotation).GetComponent<Bullet>();
            bullet.Shoot();
            currentBullets--;
            Debug.Log("Remaining Bullets: " + currentBullets);
            OnShoot?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Reload()
    {
        currentBullets = maxClipSize;
        Debug.Log("Reloading.. current bullets" + currentBullets);
    }
}
