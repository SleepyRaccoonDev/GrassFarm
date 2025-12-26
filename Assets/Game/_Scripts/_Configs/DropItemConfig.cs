using UnityEngine;

[CreateAssetMenu(
    fileName = "DropItemConfig",
    menuName = "Configs/Drop Item Config",
    order = 2)]
public class DropItemConfig : ScriptableObject
{
    [field: SerializeField] public CuttableType Type { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public float Currency { get; private set; }
    [field: SerializeField] public Item ItemPrefab { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float ChanseToDropItem { get; private set; }

    [field: SerializeField] public DropItemConfig NextDrop { get; private set; }
}