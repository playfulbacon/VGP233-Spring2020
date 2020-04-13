using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    // Custom
    [SerializeField]
    int numberOfLanes = 3;

    // Obstacles

    [SerializeField]
    GameObject obstaclePrefab;

    private List<GameObject> obstacles = new List<GameObject>();
    private Renderer obsRend;
    private float obsRendHeight;

    // Coins

    [SerializeField]
    GameObject coinPrefab;

    private List<GameObject> coins = new List<GameObject>();
    private Renderer coinRend;
    private float coinRendHeight;

    // -----------

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

        obsRend = obstaclePrefab.GetComponent<Renderer>();
        obsRendHeight = obsRend.bounds.size.y;

        coinRend = coinPrefab.GetComponent<Renderer>();
        coinRendHeight = coinRend.bounds.size.y;

        GenerateLaneSegment(Vector3.zero);
    }

    void Update()
    {
        LevelSegment firstLane = levelSegments[0];
        Lane lastLane = levelSegments[levelSegments.Count - 1].lanes[0];
        Vector3 startPosition = lastLane.laneSegments[lastLane.laneSegments.Count - 1].transform.position + (Vector3.forward * (laneLength / 2));

        // Recycle Lanes
        if (player.transform.position.z > (startPosition.z - rend.bounds.size.z))
        {
            // Change Position
            foreach (var lane in firstLane.lanes)
            {
                // Change first segment position
                lane.laneSegments[0].transform.position = new Vector3(lane.laneSegments[0].transform.position.x, lane.laneSegments[0].transform.position.y, startPosition.z);
                
                // Set first segment to last
                var firstSegment = lane.laneSegments[0];
                lane.laneSegments.RemoveAt(0);
                lane.laneSegments.Add(firstSegment);

                
            }

            // Obstacles
            // If too many obstacles, destroy first
            if (obstacles.Count > 3)
            {
                GameObject toDestroy = obstacles[0];
                obstacles.RemoveAt(0);
                Destroy(toDestroy);
            }
            // Generate Obstacles
            int randomNum = Random.Range(0, numberOfLanes);
            GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(firstLane.lanes[randomNum].laneSegments[0].transform.position.x, obsRendHeight / 2, startPosition.z), Quaternion.identity);
            obstacles.Add(obstacle);

            // Coins
            // If too many obstacles, destroy first
            if (coins.Count > 3)
            {
                GameObject toDestroy = coins[0];
                coins.RemoveAt(0);
                Destroy(toDestroy);
            }
            // Generate Obstacles
            GameObject coin = Instantiate(coinPrefab, new Vector3(firstLane.lanes[RandomNumberExcept(randomNum)].laneSegments[0].transform.position.x, coinRendHeight / 2, startPosition.z), Quaternion.identity);
            coins.Add(coin);
        }
    }

    void GenerateLaneSegment(Vector3 startPosition)
    {
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

    int RandomNumberExcept(int exception)
    {
        int randomNumber;

        do
        {
            randomNumber = Random.Range(0, numberOfLanes);
        } while (randomNumber == exception);

        return randomNumber;
    }
}
