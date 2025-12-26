using System;
using UnityEngine;

public interface IDroppable 
{
    event Action<Vector3> IsDropped;
}