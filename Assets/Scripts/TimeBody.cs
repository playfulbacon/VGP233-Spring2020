using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    private struct PointInTime
    {
        public Vector3 position;
        public Quaternion rotation;

        public PointInTime(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }

    }

    [Header("Properties")]
    public float recordTime = 5f;

    private bool isRewinding = false;
    private List<PointInTime> pointInTimeList;
    void Start()
    {
        pointInTimeList = new List<PointInTime>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartRewind();
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    private void Rewind()
    {
        if (pointInTimeList.Count > 0)
        {
            PointInTime pointInTime = pointInTimeList[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointInTimeList.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        if (pointInTimeList.Count > Mathf.Round(recordTime / Time.fixedDeltaTime)) // if time pass more the record time, should be removed from the list
        {
            pointInTimeList.RemoveAt(pointInTimeList.Count - 1);
        }
        pointInTimeList.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    private void StartRewind()
    {
        isRewinding = true;
    }

    private void StopRewind()
    {
        isRewinding = false;
    }

}
