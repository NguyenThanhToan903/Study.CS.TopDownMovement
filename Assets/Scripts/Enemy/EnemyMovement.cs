//using UnityEngine;

//public class EnemyMovement : MonoBehaviour
//{
//    public float speed = 5f;
//    [SerializeField] private Vector2 movementDirection;
//    [SerializeField] private Vector2 Velocity;

//    private void FixedUpdate()
//    {
//        movementDirection = transform.right;

//        Velocity = Vector2.Lerp(Velocity, movementDirection * speed, Time.fixedDeltaTime);
//        transform.position += (Vector3)(Time.fixedDeltaTime * Velocity);

//        // Kiểm tra giá trị di chuyển trong Console
//        Debug.Log($"Di chuyển hướng: {movementDirection} | Vị trí: {transform.position}");
//    }

//    private void OnDrawGizmos()
//    {
//        if (Application.isPlaying)
//        {
//            Gizmos.color = Color.red;
//            Gizmos.DrawLine(transform.position, transform.position + (Vector3)movementDirection * 2f);
//            Gizmos.DrawRay(transform.position, (Vector3)movementDirection * 2f);
//        }
//    }
//}

using UnityEngine;

public class EnemyMovement : BaseEnemy
{
    private IMovementStrategy movementStrategy;

    private void Awake()
    {
        movementStrategy = new StraightMovementStrategy(); // You can change this to any other strategy
    }

        // Kiểm tra giá trị di chuyển trong Console
        Debug.Log($"Di chuyển hướng: {movementDirection} | Vị trí: {transform.position}");
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)movementDirection * 2f);
        }
    }
}
