using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassSpawner : MonoBehaviour, IDroppable
{
    public event Action<Vector3> IsDropped;

    private Collider _targetCollider;

    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private LayerMask _raycastMask;

    private void Awake()
    {
        _targetCollider = GetComponent<Collider>();
        SpawnGrassOnTilemap();
    }

    private void SpawnGrassOnTilemap() {
        if (_tilemap == null) return;

        Bounds bounds = _targetCollider.bounds;

        Vector3Int minCell = _tilemap.WorldToCell(bounds.min);
        Vector3Int maxCell = _tilemap.WorldToCell(bounds.max);

        for (int x = minCell.x; x <= maxCell.x; x++)
        {
            for (int y = minCell.y; y <= maxCell.y; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);

                Vector3 worldPos = _tilemap.GetCellCenterWorld(cellPos);

                if (_targetCollider.bounds.Contains(worldPos))
                {
                    IsDropped?.Invoke(worldPos);
                }
            }
        }
    }
}