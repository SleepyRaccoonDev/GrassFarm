using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class Backpack : MonoBehaviour, ISeller
{
    public event Action<CuttableType, float> IsSelled;

    private Stack<StackItem> _supplies = new Stack<StackItem>();
    private List<ISelectable> _actions = new List<ISelectable>();

    private int _countOfSlots;
    private float _offset;

    private float _currentHeight;

    public bool IsFull => _supplies.Count >= _countOfSlots;

    [Inject]
    private void Construct(GameConfig gameConfig)
    {
        _countOfSlots = gameConfig.CountOfSlotsInBackpack;
        _offset = gameConfig.BackpackOffset;
    }

    public void ImproveCapacity(int value)
    {
        _countOfSlots += value;
    }

    public void SingOnEvent(ISelectable supply)
    {
        supply.OnSelected += Take;
        _actions.Add(supply);
    }

    private void OnDisable()
    {
        foreach (var action in _actions)
        {
            action.OnSelected -= Take;
        }
    }

    public void Take(Supply supply)
    {
        if (supply.IsTaken)
            return;

        if (_supplies.Count >= _countOfSlots)
            return;

        supply.MarkTaken();
        supply.gameObject.layer = 0;

        float height = _supplies.Count * _offset;

        _supplies.Push(new StackItem(height, supply));

        StartCoroutine(Grabbing(supply.transform, height));
    }

    private IEnumerator Grabbing(Transform itemTransform, float height)
    {
        Vector3 worldTarget = transform.position + Vector3.up * height;

        while (Vector3.Distance(itemTransform.position, worldTarget) > 0.05f)
        {
            worldTarget = transform.position + Vector3.up * height;
            itemTransform.position = Vector3.MoveTowards(
                itemTransform.position,
                worldTarget,
                15f * Time.deltaTime
            );
            yield return null;
        }

        itemTransform.SetParent(transform);
        itemTransform.localPosition = Vector3.up * height;
    }

    public StackItem Sell()
    {
        if (_supplies.Count <= 0)
            return new StackItem();

        var supply = _supplies.Pop();

        IsSelled?.Invoke(supply.Supply.Type, supply.Supply.Currency);

        return supply;
    }
}