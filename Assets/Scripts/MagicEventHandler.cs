using UnityEngine;
using System;

public class MagicEventHandler : MonoBehaviour
{
    public event Action OnStartMagicWindow;
    public event Action OnStopMagicWindow;


    private void StartMagicWindow()
    {
        Debug.Log($"[StartMagicWindow]");
        OnStartMagicWindow?.Invoke();
    }

    private void StopMagicWindow()
    {
        Debug.Log($"[StopMagicWindow]");
        OnStopMagicWindow?.Invoke();
    }
}
