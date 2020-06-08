using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    [SerializeField]
    private Vector2 offSet = Vector2.zero;

    PlayerController mPlayer;

    private void Awake()
    {
        mPlayer = FindObjectOfType<PlayerController>();

    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(mPlayer.transform.position.x + offSet.x, mPlayer.transform.position.y + offSet.y, -10f); // Camera follows the player with specified offset position
    }

}
