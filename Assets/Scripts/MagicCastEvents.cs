using UnityEngine;

public class MagicCastEvents : MonoBehaviour
{
    [SerializeField] private MagicEventHandler magicEventHandler;

    private Player player;

    private bool canDamage;

    private void Awake()
    {
        player = GetComponentInParent<Player>();

        magicEventHandler.OnStartMagicWindow += () => { canDamage = true; };
        magicEventHandler.OnStopMagicWindow += () => { canDamage = false; };
    }


    private void Update()
    {
        if (canDamage)
        {
            player.MagicCast.SpawnVfx();
        }
    }
}
