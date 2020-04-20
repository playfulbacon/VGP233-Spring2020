using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{

    [SerializeField]
    private ObjectPool objectPoolPrefab;

    public ObjectPool CreateNewObjectPool(GameObject obj, int size)
    {
        ObjectPool objectPool = Instantiate(objectPoolPrefab, transform);
        objectPool.Setup(obj, size);
        return objectPool;
    }
}
