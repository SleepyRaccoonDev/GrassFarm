using System.Collections.Generic;
using UnityEngine;

public class Caster<T> where T : class
{
    private readonly Transform _transform;
    private readonly LayerMask _mask;

    private readonly List<T> _list = new List<T>();

    private float _radius;

    public float Radius => _radius;
    public float Diameter => _radius * 2f;

    public Caster(Transform transform, GameConfig playerConfig)
    {
        _transform = transform;
        _mask = playerConfig.Mask;
        _radius = playerConfig.BraidRadius;
    }

    public void Cast()
    {
        _list.Clear();

        Collider[] colliders = Physics.OverlapSphere(
            _transform.position,
            _radius,
            _mask
        );

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<T>(out T component))
            {
                _list.Add(component);
            }
        }
    }

    public List<T> GetColliders()
    {
        return _list;
    }

    public void ImproveRadius(float value)
    {
        _radius += value;
    }
}