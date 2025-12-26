using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private IMovable _mover;
    private IRotatable _rotator;

    private float _speedForceRotate;
    private float _speedForceMove;

    public Vector3 CurrentDirection { get; private set; }
    public event Action IsCutted;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(IMovable mover, IRotatable rotator, float speedForceMove, float speedForceRotate)
    {
        _mover = mover;
        _rotator = rotator;

        _speedForceMove = speedForceMove;
        _speedForceRotate = speedForceRotate;
    }

    private void FixedUpdate()
    {
        Vector3 directionNormalized = CurrentDirection.normalized;

        if (_mover != null)
            _mover.Move(directionNormalized, _speedForceMove);

        if (_rotator != null)
            _rotator.Rotate(directionNormalized, _speedForceRotate);
    }

    public void SetDirection(Vector3 direction) => CurrentDirection = direction;

    public void SetCutting() => IsCutted?.Invoke();
}