using UnityEngine;

public interface IMovementStrategy
{
    Vector2 CalculateMovementDirection(Transform transform);
}