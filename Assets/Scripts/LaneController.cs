using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{

    [System.Serializable]
    public class Lane
    {
        public List<GameObject> laneSegments = new List<GameObject>();
    }

    [System.Serializable]
    public class LevelSegment
    {
        public List<Lane> lanes = new List<Lane>();
    }

    private List<LevelSegment> listlevelSegments = new List<LevelSegment>();
    public List<LevelSegment> propLevelSegments { get { return listlevelSegments; } }

    [SerializeField]
    GameObject laneSegmentPrefab;
    [SerializeField]
    GameObject obstaclePrefab;

    public Transform playerTransform;
    private Renderer rend;
    private float laneWidth = 2f;
    private float laneLength = 10f;

    int segmentIndex = 0;

    void Awake()
    {
        rend = laneSegmentPrefab.GetComponentInChildren<Renderer>();
        laneWidth = rend.bounds.size.x;
        laneLength = rend.bounds.size.z;
        
        GenerateLevelSegment(Vector3.zero);
    }

    void Update()
    {
        Lane lane = listlevelSegments[listlevelSegments.Count - 1].lanes[0];
        if (playerTransform.position.z  > lane.laneSegments[lane.laneSegments.Count - 1].transform.position.z - laneLength * 3)
        {
            Vector3 startPosition = lane.laneSegments[lane.laneSegments.Count - 1].transform.position + (Vector3.forward * laneLength / 2);
            GenerateLevelSegment(startPosition);
            if (playerTransform.position.z - laneLength * 3 > listlevelSegments[segmentIndex].lanes[0].laneSegments[0].transform.position.z)
            {
                DeleteLevelSegment(segmentIndex);
                segmentIndex++;
            }
        }

        //if (playerTransform.position.z - laneLength > lane.laneSegments[lane.laneSegments.Count - 1].transform.position.z)
        //{
        //    Vector3 startPosition = lane.laneSegments[lane.laneSegments.Count - 1].transform.position + (Vector3.forward * laneLength / 2);
        //    GenerateLevelSegment(startPosition);
        //    DeleteLevelSegment(segmentIndex);
        //    segmentIndex++;
        //}

        ////Debug
        //    if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Vector3 startPosition = lane.laneSegments[lane.laneSegments.Count - 1].transform.position + (Vector3.forward * laneLength / 2);
        //    GenerateLevelSegment(startPosition);
        //}
        //Debug.Log("Player Transform Z: " + playerTransform.position.z);
        //Debug.Log("Lane transform Z: " + (lane.laneSegments[lane.laneSegments.Count - 1].transform.position.z));
    }

    void GenerateLevelSegment(Vector3 startPosition)
    {
        int numberOfLanes = 3;
        int laneSegments = 3;

        float spaceBetweenLanes = 0.2f;

        List<Lane> lanes = new List<Lane>();
        for(int x = 0; x < numberOfLanes; ++x)
        {
            Lane lane = new Lane();
            for(int z = 0; z < laneSegments; ++z)
            {
                GameObject laneSegment = Instantiate(laneSegmentPrefab);
                laneSegment.transform.position = startPosition;
                laneSegment.transform.position += Vector3.right * (laneWidth + spaceBetweenLanes) * x;
                laneSegment.transform.position += Vector3.forward * (laneLength) * z;
                laneSegment.transform.position += Vector3.forward * (laneLength / 2);
                lane.laneSegments.Add(laneSegment);
            }
            lanes.Add(lane);
        }

        LevelSegment levelSegment = new LevelSegment();
        levelSegment.lanes = lanes;
        listlevelSegments.Add(levelSegment);

        //Debug.Log("Level Segment Count" + listlevelSegments.Count);
        //Debug.Log("Lanes Count" + listlevelSegments[0].lanes.Count);
    }

    void GenerateObstacles()
    {

    }

    void DeleteLevelSegment(int segment)
    { 
        for(int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                Destroy(listlevelSegments[segment].lanes[i].laneSegments[j]);
            }

        }
    }
}
