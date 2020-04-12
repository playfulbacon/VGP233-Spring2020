using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    public class Lane
    {
        public List<GameObject> laneSegments = new List<GameObject>();
    }

    public class LevelSegment
    {
        public List<Lane> lanes = new List<Lane>();
    }

    private List<LevelSegment> levelSegments = new List<LevelSegment>();
    public List<LevelSegment> LevelSegments { get { return levelSegments; } }

    [SerializeField]
    GameObject LaneSegmentPrefab;

    private Renderer rend;
    private float laneWidth = 2f;
    private float laneLength = 10f;

    GameObject player;

    void Awake()
    {
        // Player
        player = GameObject.FindWithTag("Player");

        rend = LaneSegmentPrefab.GetComponentInChildren<Renderer>();
        laneWidth = rend.bounds.size.x;
        laneLength = rend.bounds.size.z;

        GenerateLaneSegment(Vector3.zero);
    }

    void Update()
    {
        LevelSegment firstLane = levelSegments[0];
        Lane lastLane = levelSegments[levelSegments.Count - 1].lanes[0];
        Vector3 startPosition = lastLane.laneSegments[lastLane.laneSegments.Count - 1].transform.position + (Vector3.forward * (laneLength / 2));

        // Recycle Lanes
        if (player.transform.position.z > startPosition.z)
        {
            // Change Position
            foreach (var lane in firstLane.lanes)
            {
                foreach (var segment in lane.laneSegments)
                {
                    segment.transform.position = new Vector3(segment.transform.position.x, segment.transform.position.y, startPosition.z);
                }
            }

            // Send to back of the list
            LevelSegment firstSegment = levelSegments[0];
            levelSegments.RemoveAt(0);
            levelSegments.Add(firstSegment);
        }
    }

    void GenerateLaneSegment(Vector3 startPosition)
    {
        int numberOfLanes = 3;
        int laneSegments = 3;

        float spaceBetweenLanes = 0.2f;

        List<Lane> lanes = new List<Lane>();
        for (int x = 0; x < numberOfLanes; ++x)
        {
            Lane lane = new Lane();
            for (int z = 0; z < laneSegments; ++z)
            {
                GameObject laneSegment = Instantiate(LaneSegmentPrefab);
                laneSegment.transform.position = startPosition;
                laneSegment.transform.position += Vector3.right * (laneWidth + spaceBetweenLanes) * x;
                laneSegment.transform.position += Vector3.forward * laneLength * z;
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
