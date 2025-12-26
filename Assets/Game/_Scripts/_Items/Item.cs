using UnityEngine;

public abstract class Item: MonoBehaviour
{
    public abstract void Initialize(CuttableType cutableType, Color color, float currency);

    public CuttableType Type { get; protected set; }
}