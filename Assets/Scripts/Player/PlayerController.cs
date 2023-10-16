using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;


    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallJumpEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    public void CallFireEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
}
