using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instence;

    [SerializeField]
    ObjectPool objectPoolPrefab;

    private void Awake()
    {
        instence = this;
    }

    void Update()
    {
        
    }

    public ObjectPool CreateNewObjectPool(GameObject obj, int size)
    {
        ObjectPool objectPool =  Instantiate(objectPoolPrefab, transform);
        objectPool.Setup(obj, size);
        return objectPool;
    }


}
