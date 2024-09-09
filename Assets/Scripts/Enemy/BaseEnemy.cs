using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Vector2 movementDirection;
    [SerializeField] private Vector2 Velocity;

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
