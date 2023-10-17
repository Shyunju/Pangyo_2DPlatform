using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnJumpEvent;
    public event Action OnAttackEvent;

    private float AttackDelay = float.MaxValue;
    protected bool IsAttacking { get; set; }


    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    //���� ������ ��� => ���� �׼��� ���⼭ ���۵�
    private void HandleAttackDelay()
    {
        //���� �Ǻ� -> ���� ����(Call) ���� �߻�� ������ �޼��尡 ����

        if(AttackDelay <= 0.2f)
        {
            AttackDelay += Time.deltaTime;
        }

        if(IsAttacking && AttackDelay > 0.2f)
        {
            AttackDelay = 0;
            CallAttackEvent();
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallJumpEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
}
