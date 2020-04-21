using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField]
    Text AmmoText;


    void Update()
    {
        AmmoText.text = "Ammo: " + FindObjectOfType<Shooter>().ammo;
    }
}
