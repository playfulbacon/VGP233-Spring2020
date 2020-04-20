using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static Target Instance { get; private set; }
    private Queue<GameObject> targetList = new Queue<GameObject>();
    public GameObject targetPrefab;
    private int targetsHit = 0;
    public int targetListCount { get { return targetList.Count; } }
    public int getTargetshitCount { get { return targetsHit; } }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            PopulateList();
        }
        else
        {
            Debug.Log("Another Instance has already been created");
        }
    
    }

    private void PopulateList()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Target"))
        {
            targetList.Enqueue(obj);
        }
        Debug.Log("Targets in List: " + targetList.Count);
    }

    public void RemoveFromList()
    {
        targetList.Dequeue();
        targetsHit++;
        Debug.Log("Remaining Targets: " + targetList.Count);
    }
}
