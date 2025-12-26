using System;
using UnityEngine;

public interface ISelectable
{
    public event Action<Supply> OnSelected;

    void Select(Transform parent);
}