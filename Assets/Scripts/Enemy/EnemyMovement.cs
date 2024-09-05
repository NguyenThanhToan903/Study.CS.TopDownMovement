//using UnityEngine;

//public class EnemyMovement : MonoBehaviour
//{
//    public float speed = 2.0f; // Tốc độ di chuyển của kẻ địch
//    public float movementRadius = 5.0f; // Bán kính của vùng hoạt động hình tròn
//    public float minMoveTime = 2.0f; // Thời gian di chuyển tối thiểu
//    public float maxMoveTime = 3.0f; // Thời gian di chuyển tối đa
//    public float minRestTime = 1.0f; // Thời gian nghỉ tối thiểu
//    public float maxRestTime = 3.0f; // Thời gian nghỉ tối đa
//    public float perceptionRadius = 1.0f; // Bán kính vùng cảm giác

//    private Vector2 initialPosition; // Vị trí ban đầu
//    private Vector2 moveDirection { set; get; }// Hướng di chuyển
//    private float moveTime; // 
//    private float moveTimer;
//    private float restTime;
//    private float restTimer;
//    private bool isMoving = true;
//    private bool isResting = false;

//    void Start()
//    {
//        // Lưu vị trí bắt đầu của kẻ địch và xác định hướng di chuyển ban đầu
//        initialPosition = transform.position;
//        SetNewDirectionAndTime();
//    }

//    void Update()
//    {
//        if (isMoving)
//        {
//            // Di chuyển kẻ địch theo hướng đã xác định
//            transform.position += speed * Time.deltaTime * (Vector3)moveDirection;
//            moveTimer -= Time.deltaTime;

//            // Kiểm tra xem kẻ địch đã chạm vào rìa vùng hoạt động hoặc hết thời gian di chuyển chưa
//            if (moveTimer <= 0 || IsPerceptionRadiusNearEdge())
//            {
//                // Thay đổi hướng di chuyển ngẫu nhiên và chuyển sang trạng thái nghỉ
//                moveDirection = - moveDirection;// fix va cham
//                //moveDirection = Random.insideUnitCircle.normalized;
//                isMoving = false;
//                isResting = true;
//                restTime = Random.Range(minRestTime, maxRestTime);
//                restTimer = restTime;
//            }
//        }
//        else if (isResting)
//        {
//            // Thực hiện khi đang nghỉ
//            restTimer -= Time.deltaTime;
//            if (restTimer <= 0)
//            {
//                // Khi hết thời gian nghỉ, thiết lập lại hướng di chuyển và thời gian di chuyển
//                isResting = false;
//                isMoving = true;
//                SetNewDirectionAndTime();
//            }
//        }
//    }

//    private void SetNewDirectionAndTime()
//    {
//        // Tạo một vector hướng di chuyển ngẫu nhiên
//        moveDirection = Vector2.Lerp(moveDirection, transform.forward, Time.fixedDeltaTime);
//        Debug.Log(transform.forward);
//        // Tính toán thời gian di chuyển ngẫu nhiên
//        moveTime = Random.Range(minMoveTime, maxMoveTime);
//        moveTimer = moveTime;
//    }

//    private bool IsPerceptionRadiusNearEdge()
//    {
//        // Tính khoảng cách từ vị trí hiện tại đến trung tâm của vùng hoạt động
//        Vector2 directionToCenter = initialPosition - (Vector2)transform.position;
//        float distanceToCenter = directionToCenter.magnitude;

//        // Kiểm tra nếu vùng cảm giác (xung quanh kẻ địch) nằm gần rìa của vùng hoạt động
//        return distanceToCenter + perceptionRadius > movementRadius;
//    }

//    void OnDrawGizmos()
//    {
//        // Chỉ vẽ Gizmos nếu có component Transform
//        if (transform == null)
//            return;

//        // Đặt màu của Gizmos
//        Gizmos.color = Color.green;

//        // Vẽ hình tròn đại diện cho vùng hoạt động
//        Gizmos.DrawWireSphere(initialPosition, movementRadius);

//        // Vẽ một điểm ở trung tâm của vùng hoạt động
//        Gizmos.color = Color.red;
//        Gizmos.DrawSphere(initialPosition, 0.1f);

//        // Vẽ vùng cảm giác
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, perceptionRadius);

//        // Vẽ vector di chuyển
//        if (isMoving)
//        {
//            Gizmos.color = Color.blue;
//            Gizmos.DrawLine(transform.position, (Vector2)transform.position + moveDirection * 3.0f); // Vẽ chiều dài vector di chuyển
//        }
//    }
//}




using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f; // Tốc độ di chuyển của kẻ địch
    public float movementRadius = 5.0f; // Bán kính của vùng hoạt động hình tròn
    public float minMoveTime = 2.0f; // Thời gian di chuyển tối thiểu
    public float maxMoveTime = 3.0f; // Thời gian di chuyển tối đa
    public float minRestTime = 1.0f; // Thời gian nghỉ tối thiểu
    public float maxRestTime = 3.0f; // Thời gian nghỉ tối đa
    public float perceptionRadius = 1.0f; // Bán kính vùng cảm giác

    private Vector2 initialPosition; // Vị trí ban đầu
    private Vector2 moveDirection; // Hướng di chuyển
    private float moveTime;
    private float moveTimer;
    private float restTime;
    private float restTimer;
    private bool isMoving = true;
    private bool isResting = false;

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

            // Kiểm tra xem kẻ địch đã chạm vào rìa vùng hoạt động hoặc hết thời gian di chuyển chưa
            if (moveTimer <= 0 || IsPerceptionRadiusNearEdge())
            {
                // Đổi hướng di chuyển
                moveDirection = -moveDirection;
                isMoving = false;
                isResting = true;
                restTime = Random.Range(minRestTime, maxRestTime);
                restTimer = restTime;
            }
        }
        else if (isResting)
        {
            // Thực hiện khi đang nghỉ
            restTimer -= Time.deltaTime;
            if (restTimer <= 0)
            {
                // Khi hết thời gian nghỉ, thiết lập lại hướng di chuyển và thời gian di chuyển
                isResting = false;
                isMoving = true;
                SetNewDirectionAndTime();
            }
        }
    }

    private void SetNewDirectionAndTime()
    {
        // Chuyển transform.forward từ Vector3 thành Vector2 và chuẩn hóa
        Vector2 targetDirection = Vector2.Lerp(
            moveDirection, transform.forward, Time.fixedDeltaTime);

        // Tạo một vector hướng di chuyển ngẫu nhiên
        moveDirection = targetDirection;

        // Tính toán thời gian di chuyển ngẫu nhiên
        moveTime = Random.Range(minMoveTime, maxMoveTime);
        moveTimer = moveTime;
    }

    private bool IsPerceptionRadiusNearEdge()
    {
        // Tính khoảng cách từ vị trí hiện tại đến trung tâm của vùng hoạt động
        Vector2 directionToCenter = initialPosition - (Vector2)transform.position;
        float distanceToCenter = directionToCenter.magnitude;

        // Kiểm tra nếu vùng cảm giác (xung quanh kẻ địch) nằm gần rìa của vùng hoạt động
        return distanceToCenter + perceptionRadius > movementRadius;
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

        // Vẽ vùng cảm giác
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, perceptionRadius);

        // Vẽ vector di chuyển
        if (isMoving)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + moveDirection * 3.0f); // Vẽ chiều dài vector di chuyển
        }
    }
}
