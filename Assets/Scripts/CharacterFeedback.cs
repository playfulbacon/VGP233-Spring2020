using UnityEngine;

public class CharacterFeedback : MonoBehaviour
{
    [SerializeField]
    private GameObject mParticleSystem;
    [SerializeField]
    private AudioSource mSound;

    private Character mPlayer;

    private ParticleSystem mParticles;

    private void Awake()
    {
        mPlayer = GetComponent<Character>();
        mParticles = mParticleSystem.GetComponentInChildren<ParticleSystem>();
        if (mPlayer)
        {
            mPlayer.OnHitParticles += PlayerReceiveParticles;
        }
    }

    private void PlayerReceiveParticles()
    {
        mParticles.Play();
        mSound.Play();
    }

}
