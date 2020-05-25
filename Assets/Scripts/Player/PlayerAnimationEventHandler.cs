using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    public event System.Action OnStartDamageWindow;
    public event System.Action OnStopDamageWindow;
    public event System.Action OnMagicCast;

    private void SpellCast()
    {
        Debug.Log("Called");
        OnMagicCast?.Invoke();
    }

    private void StartDamageWindow()
    {
        OnStartDamageWindow?.Invoke();
    }

    private void StopDamageWindow()
    {
        OnStopDamageWindow?.Invoke();
    }
}
