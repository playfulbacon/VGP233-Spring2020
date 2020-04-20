using UnityEngine;
using System;
using System.Collections;

public class Shooter : MonoBehaviour
{

    public event Action OnShoot;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private GuiManager mGuiManager;

    [SerializeField]
    private int mMaxAmmo = 20;

    private ObjectPool bulletPool;
    private float mNextTimeFire = 0.0f;
    private readonly float mFireRate = 15.0f;

    private WaitForSeconds mWaitForSeconds;

    private void Start()
    {
        bulletPool = FindObjectOfType<ObjectPoolManager>().CreateNewObjectPool(bulletPrefab, mMaxAmmo);
        foreach (GameObject obj in bulletPool.Objects)
        {
            Bullet bullet = obj.GetComponent<Bullet>();
            obj.GetComponent<Bullet>().OnDestroy += () =>
            {
                StartCoroutine(BulletDestroy(bullet.gameObject));
            };
            obj.GetComponent<Bullet>().OnBodyCount += () =>
            {
                mGuiManager.UpdateScore(1);
            };
        }
        mGuiManager.UpdateAmmo(mMaxAmmo);
        mWaitForSeconds = new WaitForSeconds(1.5f);
    }

    private void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= mNextTimeFire)
        {
            mNextTimeFire = Time.time + (1.0f / mFireRate);
            if (bulletPool.AvailableObjects.Count > 0)
            {
                Bullet bullet = bulletPool.GetAvailableObjects(muzzle.position, muzzle.rotation).GetComponent<Bullet>();
                bullet.Shoot();
                OnShoot?.Invoke();
                mGuiManager.UpdateAmmo(bulletPool.AvailableObjects.Count);
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }
    }

    IEnumerator BulletDestroy(GameObject obj)
    {
        obj.SetActive(false);
        yield return mWaitForSeconds;
    }


    private void Reload()
    {
        if (bulletPool.AvailableObjects.Count <= 0)
        {
            foreach (GameObject obj in bulletPool.Objects)
            {
                Bullet bullet = obj.GetComponent<Bullet>();
                bulletPool.ReturnObjectToPool(bullet.gameObject);
            }

            mGuiManager.UpdateAmmo(mMaxAmmo);
        }
    }
}
