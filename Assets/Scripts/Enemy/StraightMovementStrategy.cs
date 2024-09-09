using UnityEngine;

public class StraightMovementStrategy : IMovementStrategy
{
    public Vector2 CalculateMovementDirection(Transform enemyTransform)
    {
        
        return enemyTransform.right;
    }
}
