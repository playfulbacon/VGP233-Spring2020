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
    public class LaneSegment
    {
        public List<Lane> lanes = new List<Lane>();
    }

    private Queue<GameObject> coinQueue = new Queue<GameObject>();
    private Queue<GameObject> blockQueue = new Queue<GameObject>();
    private List<LaneSegment> listlevelSegments = new List<LaneSegment>();
    public List<LaneSegment> propLaneSegments { get { return listlevelSegments; } }

    public GameObject laneSegmentPrefab;
    public GameObject blockPrefab;
    public GameObject coinPrefab;

    public Transform playerTransform;
    private Renderer rend;
    private float laneWidth = 2f;
    private float laneLength = 10f;
    private float[] xPositions = { 0.0f, 3.2f, 6.5f };
    private float[] yElevation = { 0.0f, 1.5f, 2.5f };

    [SerializeField]
    int segmentIndex = 0;

    void Awake()
    {
        rend = laneSegmentPrefab.GetComponentInChildren<Renderer>();
        laneWidth = rend.bounds.size.x;
        laneLength = rend.bounds.size.z;
        
        GenerateLaneSegment(Vector3.zero);
    }

    void Update()
    {
        Lane lane = listlevelSegments[listlevelSegments.Count - 1].lanes[0];
        if (playerTransform.position.z  > lane.laneSegments[lane.laneSegments.Count - 1].transform.position.z - laneLength * 5f)
        {
            Vector3 startPosition = lane.laneSegments[lane.laneSegments.Count - 1].transform.position + (Vector3.forward * laneLength / 2);
            GenerateLaneSegment(startPosition);
       
        }
        if (playerTransform.position.z - laneLength * 2.7f > listlevelSegments[segmentIndex].lanes[0].laneSegments[0].transform.position.z)
        {
            DeleteLaneSegment(segmentIndex);
            segmentIndex++;
        }

        //if (playerTransform.position.z - laneLength > lane.laneSegments[lane.laneSegments.Count - 1].transform.position.z)
        //{
        //    Vector3 startPosition = lane.laneSegments[lane.laneSegments.Count - 1].transform.position + (Vector3.forward * laneLength / 2);
        //    GenerateLaneSegment(startPosition);
        //    DeleteLaneSegment(segmentIndex);
        //    segmentIndex++;
        //}

        ////Debug
        //    if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Vector3 startPosition = lane.laneSegments[lane.laneSegments.Count - 1].transform.position + (Vector3.forward * laneLength / 2);
        //    GenerateLaneSegment(startPosition);
        //}
        //Debug.Log("Player Transform Z: " + playerTransform.position.z);
        //Debug.Log("Lane transform Z: " + (lane.laneSegments[lane.laneSegments.Count - 1].transform.position.z));
    }

    void GenerateLaneSegment(Vector3 startPosition)
    {
        int numberOfLanes = 3;
        int laneSegments = 3;

        float spaceBetweenLanes = 0.3f;

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
            GameObject obstacleSegment = Instantiate(blockPrefab);
            GameObject coin = Instantiate(coinPrefab);
            int xposBlockIndex = Random.Range(0, 3);
            obstacleSegment.transform.position = new Vector3(xPositions[xposBlockIndex], Random.Range(0, 1), Random.Range(lane.laneSegments[x].transform.position.z, lane.laneSegments[x].transform.position.z + 10.0f));                        
            blockQueue.Enqueue(obstacleSegment);

            int xposCoinIndex = Random.Range(0, 3);
            int yposCoinElevation = Random.Range(0, 3);
            coin.transform.position = new Vector3(xPositions[xposCoinIndex], yElevation[yposCoinElevation], Random.Range(lane.laneSegments[x].transform.position.z , lane.laneSegments[x].transform.position.z + 10.0f));
            if (coin.transform.position == obstacleSegment.transform.position)
            {
                coin.transform.position = new Vector3(coin.transform.position.x, coin.transform.position.y + 5.0f, coin.transform.position.z);
            }
            coinQueue.Enqueue(coin);
            lanes.Add(lane);
        }

        LaneSegment levelSegment = new LaneSegment();
        levelSegment.lanes = lanes;
        listlevelSegments.Add(levelSegment);

        //Debug.Log("Level Segment Count" + listlevelSegments.Count);
        //Debug.Log("Lanes Count" + listlevelSegments[0].lanes.Count);
    }


    void DeleteLaneSegment(int segment)
    {

        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                Destroy(listlevelSegments[segment].lanes[i].laneSegments[j]);
            }
            if (blockQueue.Count > 1)
            {
                Destroy(blockQueue.Dequeue());
            }
            if (coinQueue.Count > 1)
            {
                Destroy(coinQueue.Dequeue());
            }
        }

 
    }
    

}
