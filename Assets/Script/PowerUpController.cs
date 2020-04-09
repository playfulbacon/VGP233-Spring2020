using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    private int countPowerUp = 0;
    private bool mActive = false;
    private float mTargetTime = 10.0f;

    private Player mPlayer = null;

    private void Update()
    {
        if (mActive)
        {
            mTargetTime -= Time.deltaTime;
            if (mTargetTime <= 0f)
            {
                mActive = false;
                mPlayer.speed = 15;
            }
        }
    }


    public void ActivePowerUp(Player player)
    {
        countPowerUp++;
        if (countPowerUp == 10)
        {
            mPlayer = player;
            countPowerUp = 0;
            mActive = true;
            mPlayer.speed = 60;
        }
    }
}
