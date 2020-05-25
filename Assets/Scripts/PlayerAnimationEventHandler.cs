using UnityEngine;
using System;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    public event Action OnStartDamageWindow;
    public event Action OnStopDamageWindow;

    private void StartDamageWindow()
    {
        OnStartDamageWindow?.Invoke();
    }

    private void StopDamageWindow()
    {
        OnStopDamageWindow?.Invoke();
    }
}
