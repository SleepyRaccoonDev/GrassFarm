using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [Header("Player")]
    [SerializeField] private Character _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private GameConfig _playerConfig;

    [Header("Grass")]
    [SerializeField] private Grass _grassPrefab;

    [Header("Other")]
    [SerializeField] private ToolIncreeser _toolIncreeserPrefab;

    public override void InstallBindings()
    {
        BindPlayer();
    }

    private void BindPlayer()
    {
        Container.Bind<GameConfig>().FromInstance(_playerConfig).NonLazy();

        Container.Bind<InputsSystem>().AsSingle().NonLazy();
        Container.Bind<CharacterInitializer>().AsSingle().NonLazy();
        Container.Bind<CharacterButtonsController>().AsSingle().NonLazy();

        Container.Bind<Factory<Item>>().AsTransient();
        Container.Bind<Factory<Character>>().AsTransient();

        Container.Bind<Character>()
            .FromComponentInNewPrefab(_playerPrefab)
            .AsSingle()
            .OnInstantiated<Character>((ctx, character) => {
                character.transform.position = _playerSpawnPoint.position;
            });

        Container.Bind<Grass>()
            .FromInstance(_grassPrefab)
            .AsSingle();

        Container.Bind<Backpack>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();

        Container.Bind<QueueConttoller>().AsSingle().NonLazy();
        Container.Bind<Wallet>().AsSingle().NonLazy();

        Container.Bind<ToolIncreeser>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();
    }
}