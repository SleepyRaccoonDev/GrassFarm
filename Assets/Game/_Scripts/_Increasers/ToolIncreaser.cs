using UnityEngine;
using Zenject;

[RequireComponent(typeof(OnTriggerEventer))]
public class ToolIncreeser : MonoBehaviour
{
    private Wallet _wallet;

    private OnTriggerEventer _onTriggerEventer;

    private float _greenPrice;
    private float _yellowPrice;

    private float _improveRadius;

    private Caster<ICuttable> _cuttableCaster;
    private Caster<ISelectable> _selectableCaster;

    [Inject]
    private void Construct(Wallet wallet, GameConfig gameConfig)
    {
        _wallet = wallet;

        _greenPrice = gameConfig.IncreeseToolGreen;
        _yellowPrice = gameConfig.IncreeseToolYellow;

        _improveRadius = gameConfig.ImproveRadius;
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

    public void Initialize(
    Caster<ICuttable> cuttableCaster,
    Caster<ISelectable> selectableCaster) {
        _cuttableCaster = cuttableCaster;
        _selectableCaster = selectableCaster;
    }

    private void TryImprove(Collider collider)
    {
        if (CustomTools.TryGetComponentInChildren<MovArea>(collider, out MovArea movArea))
        {
            if (_wallet.TryBuy(_greenPrice, _yellowPrice))
            {
                _cuttableCaster.ImproveRadius(_improveRadius);
                _selectableCaster.ImproveRadius(_improveRadius);

                float diameter = _cuttableCaster.Diameter;

                movArea.transform.localScale = new Vector3(
                    diameter,
                    movArea.transform.localScale.y,
                    diameter
                );
            }
        }
    }

}