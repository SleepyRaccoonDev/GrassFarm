using System;
using UnityEngine;

public class OnTriggerEventer : MonoBehaviour
{
    public event Action<Collider> IsTriggered;
    public event Action<Collider> IsExit;

    private void OnTriggerEnter(Collider collider)
    {
        IsTriggered?.Invoke(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        IsExit?.Invoke(collider);
    }
}