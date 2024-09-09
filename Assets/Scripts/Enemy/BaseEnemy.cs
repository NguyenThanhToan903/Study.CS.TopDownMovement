using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public float speed;

    protected Vector2 movementDirection;
    protected Vector2 Velocity;

    // Abstract method for specific movement behavior
    public abstract void Move();

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected void SetVelocity(Vector2 newVelocity)
    {
        Velocity = newVelocity;
        transform.position += (Vector3)(Time.fixedDeltaTime * Velocity);
    }
}
