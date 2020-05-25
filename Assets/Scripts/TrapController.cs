using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private Player player;
    
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.PlayerHealth.CurrentHealth(damage);
        }
    }

}
