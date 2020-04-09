using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float mSpeed = 15f;

    [SerializeField]
    private ScoreManager mScoreManager = null;

    private List<LaneController.Lane> lanes;

    private int mSwitchLanes = 0;
    void Start()
    {
        mScoreManager = FindObjectOfType<ScoreManager>();

        lanes = FindObjectOfType<LaneController>().LevelSegments[0].lanes;
        transform.position = lanes[lanes.Count >> 1].laneSegments[0].transform.position;

    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (mSwitchLanes > 0)
            {
                mSwitchLanes--;
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (mSwitchLanes < lanes.Count - 1)
            {
                mSwitchLanes++;
            }
        }

        transform.position = new Vector3(lanes[mSwitchLanes].laneSegments[0].transform.position.x, transform.position.y, transform.position.z);
        transform.position += transform.forward * Time.deltaTime * mSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            mScoreManager.UpdatePickUpCoins();
            Destroy(other);
        }
    }

}
