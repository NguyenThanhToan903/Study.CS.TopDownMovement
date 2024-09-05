using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f; // Tốc độ di chuyển của kẻ địch
    public float movementRadius = 5.0f; // Bán kính của vùng hoạt động hình tròn
    public float minMoveTime = 1.0f; // Thời gian di chuyển tối thiểu
    public float maxMoveTime = 3.0f; // Thời gian di chuyển tối đa

    private Vector2 initialPosition;
    private Vector2 moveDirection;
    private float moveTime;
    private float moveTimer;
    private bool isMoving = true;

    void Start()
    {
        // Lưu vị trí bắt đầu của kẻ địch và xác định hướng di chuyển ban đầu
        initialPosition = transform.position;
        SetNewDirectionAndTime();
    }

    void Update()
    {
        if (isMoving)
        {
            // Di chuyển kẻ địch theo hướng đã xác định
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
            moveTimer -= Time.deltaTime;

            // Kiểm tra xem kẻ địch đã di chuyển đủ thời gian chưa
            if (moveTimer <= 0)
            {
                SetNewDirectionAndTime();
            }

            // Đảm bảo kẻ địch không ra ngoài vùng giới hạn và điều chỉnh nếu cần
            Vector2 clampedPosition = ClampPositionToBounds((Vector2)transform.position);
            transform.position = clampedPosition;

            // Nếu kẻ địch đang ở gần rìa, điều chỉnh hướng di chuyển vào trong
            if (Vector2.Distance(transform.position, initialPosition) > movementRadius * 0.9f)
            {
                // Hướng vào trong
                moveDirection = (initialPosition - (Vector2)transform.position).normalized;
            }
        }
    }

    private void SetNewDirectionAndTime()
    {
        // Tạo một vector hướng di chuyển ngẫu nhiên
        moveDirection = Random.insideUnitCircle.normalized;

        // Tính toán thời gian di chuyển ngẫu nhiên
        moveTime = Random.Range(minMoveTime, maxMoveTime);
        moveTimer = moveTime;
    }

    private Vector2 ClampPositionToBounds(Vector2 position)
    {
        // Tính khoảng cách từ vị trí hiện tại đến trung tâm của vùng hoạt động
        Vector2 directionToCenter = initialPosition - position;
        float distanceToCenter = directionToCenter.magnitude;

        // Nếu kẻ địch nằm ngoài bán kính vùng hoạt động, kẹp nó vào rìa của vùng hoạt động
        if (distanceToCenter > movementRadius)
        {
            position = initialPosition + directionToCenter.normalized * movementRadius;
        }

        return position;
    }

    void OnDrawGizmos()
    {
        // Chỉ vẽ Gizmos nếu có component Transform
        if (transform == null)
            return;

        // Đặt màu của Gizmos
        Gizmos.color = Color.green;

        // Vẽ hình tròn đại diện cho vùng hoạt động
        Gizmos.DrawWireSphere(initialPosition, movementRadius);

        // Vẽ một điểm ở trung tâm của vùng hoạt động
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(initialPosition, 0.1f);
    }
}
