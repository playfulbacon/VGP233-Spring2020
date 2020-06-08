using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarp : MonoBehaviour
{
    bool isActive = false;
    [SerializeField]
    bool isTrap = false;
    float timeDelay;
    [SerializeField]
    float activeTime;
    [SerializeField]
    GameObject block;

    void Update()
    {
        if (isTrap)
        {
            if (timeDelay < Time.time)
            {
                timeDelay = Time.time + activeTime;
                if (isActive)
                {
                    block.SetActive(false);
                    isActive = false;
                }
                else
                {
                    block.SetActive(true);
                    isActive = true;
                }
            }
        }
    }
}

