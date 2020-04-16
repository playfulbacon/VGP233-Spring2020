using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> Objects = new List<GameObject>() ;
    public List<GameObject> AvailableObjects;

    public void Setup(GameObject obj, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject newObj = Instantiate(obj, transform);
            newObj.SetActive(false);
            Objects.Add(newObj);
        }

        AvailableObjects = new List<GameObject>(Objects);
    }

    public GameObject GetAvailableObject(Vector3 position, Quaternion rotation)
    {
        GameObject retureObject = null;
        if (AvailableObjects.Count > 0)
        {
            retureObject = AvailableObjects[0];
            retureObject.SetActive(true);
            retureObject.transform.parent = null;
            retureObject.transform.position = position;
            retureObject.transform.rotation = rotation;
            AvailableObjects.Remove(retureObject);
        }
        else
        {
            Debug.Log("There is no available object");
        }
        return retureObject;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = transform;
        AvailableObjects.Add(obj);
    }
}
