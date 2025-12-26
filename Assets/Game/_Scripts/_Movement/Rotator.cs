using UnityEngine;

public class Rotator : IRotatable
{
    private Rigidbody _rigidbody;

    public Rotator(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Rotate(Vector3 direction, float rotateSpeed)
    {
        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        _rigidbody.MoveRotation(
            Quaternion.RotateTowards(
                _rigidbody.rotation,
                targetRotation,
                rotateSpeed * Time.deltaTime
            )
        );
    }
}