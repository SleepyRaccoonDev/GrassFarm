using UnityEngine;
using System;

public class Supply : Item, ISelectable
{
    public event Action<Supply> OnSelected;

    private Material _material;

    public float Currency { get; private set; }

    public bool IsTaken { get; private set; }

    public void MarkTaken()
    {
        IsTaken = true;
    }

    public override void Initialize(CuttableType cutableType, Color color, float currency)
    {
        Type = cutableType;
        Currency = currency;

        _material = GetComponent<MeshRenderer>().material;

        _material.color = color;
    }

    public void Select(Transform parent)
    {
        OnSelected?.Invoke(this);
    }
}