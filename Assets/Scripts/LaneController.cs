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

    private List<LevelSegment> levelSegments = new List<LevelSegment>();
    public List<LevelSegment> LevelSegments { get { return levelSegments; } }

    [SerializeField]
    GameObject laneSegmentPrefab;

    private Renderer rend;
    private float laneWidth = 2f;
    private float laneLength = 10f;

    void Awake()
    {
        rend = laneSegmentPrefab.GetComponentInChildren<Renderer>();
        laneWidth = rend.bounds.size.x;
        laneLength = rend.bounds.size.z;
        
        GenerateLevelSegment(Vector3.zero);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Lane lane = levelSegments[levelSegments.Count - 1].lanes[0];
            Vector3 startPosition = lane.laneSegments[lane.laneSegments.Count - 1].transform.position + (Vector3.forward * laneLength/2);
            GenerateLevelSegment(startPosition);
        }
    }

    void GenerateLevelSegment(Vector3 startPosition)
    {
        int numberOfLanes = 3;
        int laneSegments = 3;

        float spaceBetweenLanes = 0.5f;

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
        levelSegments.Add(levelSegment);
    }
}
