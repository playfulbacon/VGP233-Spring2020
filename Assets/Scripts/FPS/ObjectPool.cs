using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> Objects = new List<GameObject>();
    public List<GameObject> AvailableObjects;

    public void Setup(GameObject obj, int size)
    {
        for(int i = 0; i < size; ++i)
        {
            GameObject newObj = Instantiate(obj, transform);
            newObj.SetActive(false);

            Objects.Add(newObj);
        }

        AvailableObjects = new List<GameObject>(Objects);
    }

    public GameObject GetAvailableObject(Vector3 position, Quaternion rotation)
    {
        GameObject returnObject = null;
        if (AvailableObjects.Count > 0)
        {
            returnObject = AvailableObjects[AvailableObjects.Count-1];
            returnObject.SetActive(true);
            returnObject.transform.parent = null;
            returnObject.transform.position = position;
            returnObject.transform.rotation = rotation;
            AvailableObjects.Remove(returnObject);
        }
        else
        {
            Debug.Log("There are no available objects in the pool");
        }
        return returnObject;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = transform;
        AvailableObjects.Add(obj);
    }
}
