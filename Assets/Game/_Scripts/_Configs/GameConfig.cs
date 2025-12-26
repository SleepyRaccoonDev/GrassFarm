using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "GameConfig",
    menuName = "Configs/Game Config",
    order = 1)]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public float PlayerMovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerRotationSpeed { get; private set; }

    [field: SerializeField] public float CharacterMinMovementSpeed { get; private set; }
    [field: SerializeField] public float CharacterMaxMovementSpeed { get; private set; }
    [field: SerializeField] public float CharacterMinRotationSpeed { get; private set; }
    [field: SerializeField] public float CharacterMaxRotationSpeed { get; private set; }

    [field: SerializeField] public int GrassAmountForSpawn { get; private set; }
    [field: SerializeField] public float TimeToGrassGrow { get; private set; }

    [field: SerializeField] public float BraidRadius { get; private set; }
    [field: SerializeField] public float ImproveRadius { get; private set; }
    [field: SerializeField] public LayerMask Mask { get; private set; }
    [field: SerializeField] public float SpeedDisappearing { get; private set; }
    [field: SerializeField] public float SpeedGrowing { get; private set; }

    [field: SerializeField] public int CountOfSlotsInBackpack { get; private set; }
    [field: SerializeField] public float BackpackOffset { get; private set; }
    [field: SerializeField] public int ImproveCapacity { get; private set; }

    [field: SerializeField] public List<Character> CustomerPrefabs { get; private set; }
    [field: SerializeField] public List<Transform> QueuePoints { get; private set; }

    [field: SerializeField] public float IncreeseToolGreen { get; private set; }
    [field: SerializeField] public float IncreeseToolYellow { get; private set; }

    [field: SerializeField] public float IncreeseBackpackGreen { get; private set; }
    [field: SerializeField] public float IncreeseBackpackYellow { get; private set; }
}