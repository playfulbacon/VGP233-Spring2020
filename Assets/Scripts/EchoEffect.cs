using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    [SerializeField]
    private float timeBtwSpaws = 0.0f;

    [SerializeField]
    private float startTimeBtwSpawns = 0.0f;

    [SerializeField]
    private GameObject echo = default;

    private PlayerController mPlayer;

    private void Start()
    {
        mPlayer = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (mPlayer.moveDirection != 0.0f)
        {
            if (timeBtwSpaws <= 0.0f)
            {
                GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
                Destroy(instance, 8.0f);
                timeBtwSpaws = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpaws -= Time.deltaTime;

            }
        }
       
    }
}
