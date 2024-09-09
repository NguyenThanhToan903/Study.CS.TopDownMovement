using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private Vector2 moveDir;

    private void Update()
    {
        Move();
        GetMoveDir();
    }


    private void Move()
    {
        moveInput = InputManager.Instance.input.normalized;
        transform.parent.position += moveSpeed * Time.deltaTime * moveInput; 
    }

    private void GetMoveDir()
    {
        moveDir = InputManager.Instance.input.normalized;
    }

    private void OnDrawGizmos()
    {
        // Kiểm tra xem GameObject có tồn tại trong Scene không
        if (Application.isPlaying)
        {
            // Vẽ một đường từ vị trí hiện tại theo hướng di chuyển
            Gizmos.color = Color.red; // Màu sắc của đường  
            Gizmos.DrawLine(transform.parent.position, transform.parent.position + (Vector3)moveDir * 2f); // Vẽ đường dựa theo vector di chuyển

            // Vẽ mũi tên để hiển thị hướng
            Gizmos.DrawRay(transform.parent.position, (Vector3)moveDir * 2f);
        }
    }
}
