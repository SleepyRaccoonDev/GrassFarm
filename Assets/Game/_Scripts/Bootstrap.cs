using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private Character _character;
    private Backpack _backpack;
    private GameConfig _gameConfig;
    private QueueConttoller _queueConttoller;
    private InputsSystem _inputsSystem;
    private Wallet _wallet;
    private ToolIncreeser _toolIncreeser;

    private Controller _characterButtonsController;
    private CharacterInitializer _characterInitializer;

    private Caster<ICuttable> _casterCuttables;
    private Caster<ISelectable> _casterSelectables;

    private Cutter _cutter;
    private Selector _selector;

    [Inject]
    private void Construct(
        Character character,
        Backpack backpack,
        CharacterInitializer characterInitializer,
        GameConfig gameConfig,
        QueueConttoller queueConttoller,
        InputsSystem inputsSystem,
        Wallet wallet,
        ToolIncreeser toolIncreeser)
    {
        _character = character;
        _backpack = backpack;
        _characterInitializer = characterInitializer;
        _gameConfig = gameConfig;
        _queueConttoller = queueConttoller;
        _inputsSystem = inputsSystem;
        _wallet = wallet;
        _toolIncreeser = toolIncreeser;
    }

    private void Start()
    {
        ActivatePlayer();
        _toolIncreeser.Initialize(_casterCuttables, _casterSelectables);
    }

    private void Update()
    {
        UpdatePlayer();
        _queueConttoller.UpdateControllers();
    }

    private void OnDisable()
    {
        _cutter.OnCut -= _character.SetCutting;
        _backpack.IsSelled -= _wallet.AddCoin;
    }

    private void ActivatePlayer()
    {
        _characterButtonsController = _characterInitializer.Initialize(
            _character,
            new CharacterButtonsController(_character, _inputsSystem),
            _gameConfig.PlayerMovementSpeed,
            _gameConfig.PlayerRotationSpeed
            );

        _casterCuttables = new Caster<ICuttable>(_character.transform, _gameConfig);
        _casterSelectables = new Caster<ISelectable>(_character.transform, _gameConfig);

        _cutter = new Cutter();
        _selector = new Selector(_backpack);

        _cutter.OnCut += _character.SetCutting;
    }

    private void UpdatePlayer()
    {
        _characterButtonsController.OnUpdate();

        _casterCuttables.Cast();
        _casterSelectables.Cast();

        _cutter.Cut(_casterCuttables.GetColliders(), _backpack);
        _selector.Select(_casterSelectables.GetColliders());
    }
}