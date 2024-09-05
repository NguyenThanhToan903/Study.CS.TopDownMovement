using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float moveSpeed = 5f;
    //private Rigidbody2D rb;
    public Vector3 moveInput;
    public Vector3 moveDir;
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveDir = new Vector3(moveInput.x, moveInput.y).normalized;
        transform.position += moveSpeed * Time.deltaTime * moveInput;
    }
}
