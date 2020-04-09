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
    private GameObject laneSegmentPreFab = null;

    [SerializeField]
    private Player mPlayer = null;

    

    private Renderer render;

    private float laneWidth = 2f;
    private float laneLength = 10f;

    void Awake()
    {
        render = laneSegmentPreFab.GetComponentInChildren<Renderer>();
        laneWidth = render.bounds.size.x;
        laneLength = render.bounds.size.z;

        GenerateLaneSegment(Vector3.zero);
    }

    void Update()
    {
        GenerateLane(mPlayer.gameObject);
    }


    float mCurrentPos = 0.0f;

    public void GenerateLane(GameObject player)
    {
        mCurrentPos = player.transform.position.z;
        Lane lane = levelSegments[levelSegments.Count - 1].lanes[0];
        float lanePos = lane.laneSegments[lane.laneSegments.Count - 1].transform.position.z;

        if (mCurrentPos > (lanePos * 0.75f))
        {
            Vector3 startPosition = lane.laneSegments[lane.laneSegments.Count - 1].transform.position + (Vector3.forward * (laneLength * 0.5f));
            GenerateLaneSegment(startPosition);
           // DestroyOldLanes();
        }
    }

    private void DestroyOldLanes()
    {
        int numberOfLanes = 3;
        int laneSegments = 3;
        for (int i = 0; i < numberOfLanes*laneSegments; ++i)
        {
            Lane lane = LevelSegments[i].lanes[i];
            
            Destroy(lane.laneSegments[i]);
        }
    }

    private void GenerateLaneSegment(Vector3 startPosition)
    {
        int numberOfLanes = 3;
        int laneSegments = 3;

        float spaceBetweenLane = 0.5f;
        List<Lane> lanes = new List<Lane>();
        for (int x = 0; x < numberOfLanes; ++x)
        {
            Lane lane = new Lane();
            for (int z = 0; z < laneSegments; ++z)
            {
                GameObject laneSegment = Instantiate(laneSegmentPreFab);
                laneSegment.transform.position = startPosition;
                laneSegment.transform.position += Vector3.right * (laneWidth + spaceBetweenLane) * x;
                laneSegment.transform.position += Vector3.forward * laneLength * z;
                laneSegment.transform.position += Vector3.forward * laneLength * 0.5f;
                lane.laneSegments.Add(laneSegment);
            }
            lanes.Add(lane);
        }

        LevelSegment levelSegment = new LevelSegment
        {
            lanes = lanes
        };
        levelSegments.Add(levelSegment);
    }
}
