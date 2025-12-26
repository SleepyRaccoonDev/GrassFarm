using UnityEngine;
using Zenject;

[RequireComponent(typeof(OnTriggerEventer))]
public class BackpackIncreaser : MonoBehaviour
{
    private Wallet _wallet;

    private OnTriggerEventer _onTriggerEventer;

    private float _greenPrice;
    private float _yellowPrice;

    private int _improveCapacityOn;

    [Inject]
    private void Construct(Wallet wallet, GameConfig gameConfig)
    {
        _wallet = wallet;

        _greenPrice = gameConfig.IncreeseBackpackGreen;
        _yellowPrice = gameConfig.IncreeseBackpackYellow;
        _improveCapacityOn = gameConfig.ImproveCapacity;
    }

    private void Awake()
    {
        _onTriggerEventer = GetComponent<OnTriggerEventer>();
        _onTriggerEventer.IsTriggered += TryImprove;
    }

    private void OnDisable()
    {
        _onTriggerEventer.IsTriggered -= TryImprove;
    }

    private void TryImprove(Collider collider)
    {
        if (CustomTools.TryGetComponentInChildren<Backpack>(collider, out Backpack backpack))
        {
            if (_wallet.TryBuy(_greenPrice, _yellowPrice))
                backpack.ImproveCapacity(_improveCapacityOn);
        }
    }
}