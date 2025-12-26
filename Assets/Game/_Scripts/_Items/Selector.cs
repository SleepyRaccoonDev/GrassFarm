using System.Collections.Generic;
using UnityEngine;

public class Selector
{
    private Backpack _backpack;

    public Selector(Backpack backpack)
    {
        _backpack = backpack;
    }

    public void Select(List <ISelectable> selectables)
    {
        foreach (var selectable in selectables)
        {
            _backpack.SingOnEvent(selectable);
            selectable.Select(_backpack.transform);
        }
    }
}