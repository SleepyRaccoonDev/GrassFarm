using UnityEngine;

public static class CustomTools
{
    public static bool TryGetComponentInChildren<T>(Collider collider, out T component) where T : class
    {
        component = collider.GetComponentInChildren<T>();

        return component == null ? false : true;
    }
}