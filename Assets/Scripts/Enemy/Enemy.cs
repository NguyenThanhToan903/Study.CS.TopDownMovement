using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f; // Tốc độ di chuyển của kẻ địch
    public Vector2 movementRange = new Vector2(5.0f, 5.0f); // Kích thước vùng giới hạn di chuyển

    private Vector2 initialPosition;
    private Vector2 targetPosition;

    void Start()
    {
        // Lưu vị trí bắt đầu của kẻ địch
        initialPosition = transform.position;
        SetNewTargetPosition();
    }

    void Update()
    {
        // Di chuyển kẻ địch theo hướng của mục tiêu
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Kiểm tra xem kẻ địch đã đến mục tiêu chưa
        if ((Vector2)transform.position == targetPosition)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        // Tạo một vị trí mục tiêu ngẫu nhiên trong vùng giới hạn
        float randomX = Random.Range(-movementRange.x, movementRange.x);
        float randomY = Random.Range(-movementRange.y, movementRange.y);

        // Cập nhật mục tiêu với vị trí mới tính từ vị trí ban đầu
        targetPosition = initialPosition + new Vector2(randomX, randomY);
    }
}
