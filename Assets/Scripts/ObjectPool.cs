using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> Objects = new List<GameObject>();
    public List<GameObject> AvailableObjects;

    private List<GameObject> SpentBullets = new List<GameObject>();

    public void Setup(GameObject obj, int size)
    {
        for (int i = 0; i < size; ++i)
        {
            GameObject newObj = Instantiate(obj, transform);
            newObj.SetActive(false);
            Objects.Add(newObj);
        }

        AvailableObjects = new List<GameObject>(Objects);
    }

    public GameObject GetAvailableObect(Vector3 pos, Quaternion rot)
    {
        GameObject returnObject = null;
        if (AvailableObjects.Count > 0)
        {
            returnObject = AvailableObjects[AvailableObjects.Count  - 1];
            returnObject.SetActive(true);
            returnObject.transform.parent = null;
            returnObject.transform.position = pos;
            returnObject.transform.rotation = rot;
            AvailableObjects.Remove(returnObject);
        }
        else
        {
            Debug.Log("There are no available objects left in the pool.");
        }
        return returnObject;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = transform;
        SpentBullets.Add(obj);
    }

    public void Reload()
    {
        foreach (GameObject bullet in SpentBullets)
        {
            AvailableObjects.Add(bullet);
        }
        SpentBullets.Clear();
    }
}
