using System.Collections;
using UnityEngine;

public class SpawnObjectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mPrefabsObjs;

    [SerializeField]
    private Vector3 mCenter;

    [SerializeField]
    private Vector3 mSize;

    [SerializeField]
    public int mCount = 10;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        for (int i = 0; i < mCount; ++i)
        {
            float randomX = Random.Range(-mSize.x * 0.5f, mSize.x * 0.5f);
            float randomZ = Random.Range(-mSize.z * 0.5f, mSize.z * 0.5f);

            Vector3 position = mCenter + new Vector3(randomX, 0.0f, randomZ);

            Instantiate(mPrefabsObjs, position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawCube(mCenter, mSize);
    }

}
