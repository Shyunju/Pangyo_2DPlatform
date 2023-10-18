using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }
    public void OnJump(InputValue value)
    {
        CallJumpEvent();
    }
    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}
