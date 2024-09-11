using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base
{
    private float speed;
    public float Speed { get => speed; set => speed = value; }

    public abstract void Move();
}
