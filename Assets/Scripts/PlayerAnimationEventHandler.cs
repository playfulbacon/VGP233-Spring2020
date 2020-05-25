using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    public event System.Action OnStartDamageWindow;
    public event System.Action OnStopDamageWindow;
    public event System.Action OnStopDodge;

    [SerializeField]
    GameObject ProjectTilePrefab;
    [SerializeField]
    Transform ProjectTileSpawnPositon;

    private void StartDamageWindow()
    {
        OnStartDamageWindow?.Invoke();
    }

    private void StopDamageWindow()
    {
        OnStopDamageWindow?.Invoke();
    }

    private void StopDodge()
    {
        OnStopDodge?.Invoke();
    }

    private void MagicAttack()
    {
        GameObject projectTile = Instantiate(ProjectTilePrefab);
        projectTile.transform.position = ProjectTileSpawnPositon.position;
        projectTile.GetComponent<Rigidbody>().AddForce(transform.forward * 20.0f, ForceMode.Impulse);
    }
}
