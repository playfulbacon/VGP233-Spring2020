using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    private ObjectPoolManager objPoolMng;

    private Text ammoTxt;

    private void Start()
    {
        objPoolMng = FindObjectOfType<ObjectPoolManager>();
        ammoTxt = GetComponentInChildren<Text>();
    }
    
    private void Update()
    {
        ammoTxt.text = "Ammo: " + objPoolMng.objectPool.AvailableObjects.Count + " ";
    }
}
