using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    [System.Serializable]
    public class Lane
    {
        public List<GameObject> laneSements = new List<GameObject>();
    }

    [System.Serializable]
    public class levelSegment
    {
        public List<Lane> lanes = new List<Lane>();
        public List<GameObject> Obsecles = new List<GameObject>();
        public List<GameObject> Coin = new List<GameObject>(); 
    }

    private List<levelSegment> levelSegments = new List<levelSegment>();
    public List<levelSegment> LevelSegments { get { return levelSegments; } } 

    [SerializeField]
    GameObject laneSegmentPrefab;
    [SerializeField]
    Transform player;
    [SerializeField]
    GameObject ObstaclePrefab;
    [SerializeField]
    GameObject CoinPrefab;

    Renderer rend;
    float laneWidth = 2f;
    float laneLength = 10f;
    
    public static bool isGameFinish = false;

    void Awake()
    {
        rend = laneSegmentPrefab.GetComponentInChildren<Renderer>();
        laneWidth = rend.bounds.size.x;
        laneLength = rend.bounds.size.z;
        isGameFinish = false;
        GenerateLevelSegment(Vector3.zero);
    }

    void Update()
    {
        if(isGameFinish)
        {
            CleanLevel();
        }
        else
        {
            Lane lane = levelSegments[levelSegments.Count - 1].lanes[0];
            if (player.position.z >= lane.laneSements[lane.laneSements.Count - 2].transform.position.z + (Vector3.forward * laneLength / 2).z)
            {
                Vector3 startPostion = lane.laneSements[lane.laneSements.Count - 1].transform.position + (Vector3.forward * laneLength / 2);
                addNewSegment(startPostion);
                Debug.Log($"{lane.laneSements[lane.laneSements.Count - 2].transform.position.z + (Vector3.forward * laneLength / 2).z}");
            }
            Destroy();
        }
    }

    void GenerateLevelSegment(Vector3 startPosition)
    {
        int numberOFLanes = 3;
        int laneSegments = 3;
        int maxObstacles = 2;
        int maxCoin = 2;

        float spaceBetweemLanes = 0.5f;
        List<Lane> lanes = new List<Lane>();
        List<GameObject> Obstacles = new List<GameObject>();
        List<GameObject> Coins = new List<GameObject>();
        for (int x = 0; x < numberOFLanes; ++x)
        {
            Lane lane = new Lane();
            for (int z = 0; z < laneSegments; ++z)
            {
                GameObject laneSegment = Instantiate(laneSegmentPrefab);
                laneSegment.transform.position = startPosition;
                laneSegment.transform.position += Vector3.right * (laneWidth + spaceBetweemLanes) * x;
                laneSegment.transform.position += Vector3.forward * (laneLength)*z;
                laneSegment.transform.position += Vector3.forward * (laneLength / 2);
                lane.laneSements.Add(laneSegment);
            }
            lanes.Add(lane);
        }
        for(int i = 0; i < maxObstacles; ++i )
        {
             GameObject Obstacle = Instantiate(ObstaclePrefab);
             Obstacle.transform.position = startPosition;
             Obstacle.transform.position += Vector3.right * (laneWidth + spaceBetweemLanes) * Random.Range(0, laneSegments-1);
             Obstacle.transform.position += Vector3.forward * Random.Range(0, (laneLength) * (laneSegments-1) + (laneLength / 2));
             Obstacle.transform.position += Vector3.forward * (laneLength / 2);
             Obstacles.Add(Obstacle);
        }

        for (int i = 0; i < maxCoin; ++i)
        {
            GameObject Coin = Instantiate(CoinPrefab);
            Coin.transform.position = startPosition;
            Coin.transform.position += Vector3.right * (laneWidth + spaceBetweemLanes) * Random.Range(0, laneSegments - 1);
            Coin.transform.position += Vector3.forward * Random.Range(0, (laneLength) * (laneSegments - 1) + (laneLength / 2));
            Coin.transform.position += Vector3.forward * (laneLength / 2);
            Coins.Add(Coin);
        }

        levelSegment levelSegment = new levelSegment();
        levelSegment.lanes = lanes;
        levelSegment.Obsecles = Obstacles;
        levelSegment.Coin = Coins;
        levelSegments.Add(levelSegment);
    }

    void addNewSegment(Vector3 startPosition)
    {
        int numberOFLanes = 3;
        int laneSegments = 3;
        int maxObstacles = 2;
        int maxCoin = 2;

        float spaceBetweemLanes = 0.5f;
        for (int x = 0; x < numberOFLanes; ++x)
        {
            Lane lane = new Lane();
            for (int z = 0; z < laneSegments; ++z)
            {
                GameObject laneSegment = Instantiate(laneSegmentPrefab);
                laneSegment.transform.position = startPosition;
                laneSegment.transform.position += Vector3.right * (laneWidth + spaceBetweemLanes) * x;
                laneSegment.transform.position += Vector3.forward * (laneLength) * z;
                laneSegment.transform.position += Vector3.forward * (laneLength / 2);
                levelSegments[levelSegments.Count - 1].lanes[x].laneSements.Add(laneSegment);
            }
        }
        for (int i = 0; i < maxObstacles; ++i)
        {
            GameObject Obstacle = Instantiate(ObstaclePrefab);
            Obstacle.transform.position = startPosition;
            Obstacle.transform.position += Vector3.right * (laneWidth + spaceBetweemLanes) * Random.Range(0, laneSegments - 1);
            Obstacle.transform.position += Vector3.forward * Random.Range(0, (laneLength) * (laneSegments - 1) + (laneLength / 2));
            Obstacle.transform.position += Vector3.forward * (laneLength / 2);
            levelSegments[levelSegments.Count - 1].Obsecles.Add(Obstacle);
        }
        for (int i = 0; i < maxCoin; ++i)
        {
            GameObject Coin = Instantiate(CoinPrefab);
            Coin.transform.position = startPosition;
            Coin.transform.position += Vector3.right * (laneWidth + spaceBetweemLanes) * Random.Range(0, laneSegments - 1);
            Coin.transform.position += Vector3.forward * Random.Range(0, (laneLength) * (laneSegments - 1) + (laneLength / 2));
            Coin.transform.position += Vector3.forward * (laneLength / 2);
            levelSegments[levelSegments.Count - 1].Coin.Add(Coin);
        }
    }

    void Destroy()
    {
        for (int i = 0; i < levelSegments[levelSegments.Count - 1].lanes.Count; i++)
        {
            for (int j = 0; j < levelSegments[levelSegments.Count - 1].lanes[i].laneSements.Count; ++j)
            {
                float LaneLong = levelSegments[levelSegments.Count - 1].lanes[i].laneSements[j].transform.position.z + (Vector3.forward * laneLength / 2).z;
                if (player.position.z > LaneLong)
                {
                    Destroy(levelSegments[levelSegments.Count - 1].lanes[i].laneSements[j].gameObject);
                    levelSegments[levelSegments.Count - 1].lanes[i].laneSements.Remove(levelSegments[levelSegments.Count - 1].lanes[i].laneSements[j]);
                    for (int z = 0; z < levelSegments[levelSegments.Count - 1].Obsecles.Count; ++z)
                    {
                        if (LaneLong > levelSegments[levelSegments.Count - 1].Obsecles[z].transform.position.z)
                        {
                            Destroy(levelSegments[levelSegments.Count - 1].Obsecles[z].gameObject);
                            levelSegments[levelSegments.Count - 1].Obsecles.Remove(levelSegments[levelSegments.Count - 1].Obsecles[z]);
                        }
                        if (LaneLong > levelSegments[levelSegments.Count - 1].Coin[z].transform.position.z)
                        {
                            if (levelSegments[levelSegments.Count - 1].Coin[z].gameObject)
                            {
                                Destroy(levelSegments[levelSegments.Count - 1].Coin[z].gameObject);
                            }
                            levelSegments[levelSegments.Count - 1].Coin.Remove(levelSegments[levelSegments.Count - 1].Coin[z]);
                        }
                    }
                }
            }
        }
    }

    void CleanLevel()
    {
        levelSegments.Clear();
        Awake();
    }
}
