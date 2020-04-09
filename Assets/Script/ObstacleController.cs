using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private GameObject mObstacle;

    public void GenerateObstacles(Vector3 position)
    {
        GameObject obstacles = Instantiate(mObstacle);
        float randomPosZ = position.z + Random.Range(0.0f, 30.0f);
        obstacles.transform.position += new Vector3(position.x, obstacles.transform.position.y, position.z *randomPosZ);
    }

}
