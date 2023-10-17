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

    //공격 딜레이 계산 => 공격 액션은 여기서 시작됨
    private void HandleAttackDelay()
    {
        //실행 판별 -> 공격 실행(Call) 실제 발사는 구독된 메서드가 실행

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
