using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float moveSpeed = 5f;
    //private Rigidbody2D rb;
    public Vector3 moveInput;
    public Vector2 moveDir;

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveDir = moveInput.normalized;
        transform.position += moveSpeed * Time.deltaTime * moveInput;
    }


    private void OnDrawGizmos()
    {
        // Kiểm tra xem GameObject có tồn tại trong Scene không
        if (Application.isPlaying)
        {
            // Vẽ một đường từ vị trí hiện tại theo hướng di chuyển
            Gizmos.color = Color.red; // Màu sắc của đường  
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDir * 2f); // Vẽ đường dựa theo vector di chuyển

            // Vẽ mũi tên để hiển thị hướng
            Gizmos.DrawRay(transform.position, (Vector3)moveDir * 2f);
        }
    }
}
