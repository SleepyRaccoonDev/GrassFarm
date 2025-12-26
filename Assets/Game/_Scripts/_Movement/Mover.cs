using UnityEngine;

public class Mover : IMovable
{
    private Rigidbody _rigidbody;

    public Mover(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Move(Vector3 direction, float speedForce)
    {
        _rigidbody.velocity = direction * speedForce;
    }
}