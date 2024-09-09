using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance { get => instance; set => instance = value; }

    [SerializeField] public Vector3 input;

    private void Awake()
    {
        InputManager.instance = this;
    }

    private void FixedUpdate()
    {
        this.GetInput();
    }

    protected virtual void GetInput()
    {
        this.input.x = Input.GetAxis("Horizontal");
        this.input.y = Input.GetAxis("Vertical");
    }
}
