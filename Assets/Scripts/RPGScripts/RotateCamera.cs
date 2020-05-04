using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float speed;
    bool isBattleView = false;
    public bool ToggleBattleView { get { return isBattleView; } set { isBattleView = value; } }
    
   public Quaternion originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos.SetLookRotation(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBattleView)
            transform.Rotate(0, speed * Time.deltaTime, 0);
        else
            transform.rotation = originalPos;
        
    }
}
