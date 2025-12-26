using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class QueueConttoller
{
    private CharacterInitializer _characterInitializer;
    private Factory<Character> _factory;
    private GameConfig _gameConfig;
    private List<Transform> _queuePoints;

    private Queue<Controller> _characters = new Queue<Controller>();
    
    private RaycastHit _hit;

    [Inject]
    private void Construct(CharacterInitializer characterInitializer, Factory<Character> factory, GameConfig gameConfig)
    {
        _characterInitializer = characterInitializer;
        _factory = factory;
        _queuePoints = gameConfig.QueuePoints;
        _gameConfig = gameConfig;

        CreateQueue(gameConfig.CustomerPrefabs, gameConfig.QueuePoints);
    }

    private void CreateQueue(List<Character> characters, List<Transform> queuePoints)
    {
        for(int i = 1; i < queuePoints.Count; i++)
        {
            Character randomCharacter = characters
                .OrderBy(_ => UnityEngine.Random.value)
                .FirstOrDefault();

            Vector3 direction = queuePoints[i - 1].position - queuePoints[i].position;

            Physics.Raycast(queuePoints[i].position, Vector3.down, out _hit);

            Character character = _factory.Get(randomCharacter,
                new Vector3(_hit.point.x, _hit.point.y + randomCharacter.GetComponent<CapsuleCollider>().height / 2, _hit.point.z),
                Quaternion.LookRotation(direction));

            float minMove = UnityEngine.Random.Range(_gameConfig.CharacterMinMovementSpeed, _gameConfig.CharacterMaxMovementSpeed);
            float minRotate = UnityEngine.Random.Range(_gameConfig.CharacterMinRotationSpeed, _gameConfig.CharacterMinRotationSpeed);

            Controller controller = _characterInitializer.Initialize(character, new NPCController(character), minMove, minRotate);

            _characters.Enqueue(controller);
        }
    }

    public Character GetCurrentCaracter()
    {
        Controller controller = _characters.Peek();
        return controller.GetCharacter();
    }

    public IEnumerator ScrollQueue(YieldInstruction action, Action callback)
    {
        Controller controller = _characters.Dequeue();
        _characters.Enqueue(controller);

        if (action != null)
            yield return action;

        var snapshot = _characters.ToArray();

        int i = 1;

        foreach (var character in snapshot) {
            character.SetNewPosition(_queuePoints[i++].position);
            yield return null;
        }

        callback?.Invoke();
    }


    public void  UpdateControllers()
    {
        foreach (var characters in _characters)
        {
            characters.OnUpdate();
        }
    }
}