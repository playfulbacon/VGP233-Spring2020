using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField]
    ObjectPool objectPoolPrefab;

    public ObjectPool objectPool;

    private void Awake()
    {
        // Make it a singleton.
        instance = this;
    }

    public ObjectPool CreateNewObjectPool(GameObject obj, int size)
    {
        objectPool = Instantiate(objectPoolPrefab, transform);
        objectPool.Setup(obj, size);
        return objectPool;
    }
}
