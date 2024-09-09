using UnityEngine;

public class StraightMovementStrategy : IMovementStrategy
{
    public Vector2 CalculateMovementDirection(Transform enemyTransform)
    {
        // Move in the direction the enemy is facing
        return enemyTransform.right;
    }
}
