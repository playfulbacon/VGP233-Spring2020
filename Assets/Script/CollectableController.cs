using UnityEngine;

public class CollectableController : MonoBehaviour
{
    [SerializeField]
    private GameObject mCollectables;

    public void GenerateCollectables(Vector3 position)
    {
        GameObject obstacles = Instantiate(mCollectables);
        float randomPosZ = position.z + Random.Range(0.0f, 30.0f);
        obstacles.transform.position += new Vector3(position.x, obstacles.transform.position.y, position.z + randomPosZ);
    }
}
