using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

//공격 분리 or 컨트롤러에서 공격처리

//플레이어 현재 상태(변신 상태)
//캐릭터 상태 변화 종류 : 커지고, 작아지고, 속도, 공격(맞으면 없어짐) // 스테이지를 리셋
public enum PlayerState
{
    Nomal,
    Bigger,
    Attacker
}

public class PlayerAction : MonoBehaviour
{
    private PlayerController controller;

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 currentDirection = Vector2.zero;
    private Rigidbody2D rigid;

    [SerializeField]
    private Transform aim;
    public GameObject bulletPrefab;
    private bool isFacingRight = true; // 방향 정보


    public PlayerState currenState = PlayerState.Nomal;


    public float defaultSpeed;
    public float maxSpeed;
    public float jumpPower;

    public float hitDistans;

    SpriteRenderer spriteRenderer; //있어야함
    Animator anim; //있어야함
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        controller.OnMoveEvent += Move;
        controller.OnAttackEvent += () => Shoot(isFacingRight);
        controller.OnJumpEvent += Jump;
    }

    private void Update()
    {
        PlayerFlip(moveDirection);
        Debug.DrawRay(rigid.position, Vector2.down * 2, Color.red);
    }
    private void FixedUpdate()
    {        
        ApplyMovent(moveDirection);
        isGround();
    }

    private void Move(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void PlayerFlip(Vector2 direction)
    {

        if (direction == Vector2.zero)
            return;

        if (moveDirection != currentDirection)
        {
            isFacingRight = direction.x > 0;

            currentDirection = moveDirection;
            spriteRenderer.flipX = currentDirection.x < 0;
            aim.localPosition = new Vector2(direction.x, 0);
        }
        
        //TODO : 에임 포지션 좌우 반전 추가(완)
    }
    private void ApplyMovent(Vector2 direction)
    {
        rigid.AddForce(Vector2.right * direction * defaultSpeed, ForceMode2D.Impulse);

        //Max Speed
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);           
        }

        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3f)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }

        //rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
    }

    private void Jump()
    {
        if (!anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
    }

    private void isGround()
    {
        //땅 판별
        if (rigid.velocity.y <= 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 2, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance <= hitDistans)
                {
                    anim.SetBool("isJump", false);
                }
            }
        }
    }
    private void Shoot(bool isFacingRight)
    {
        SetAttackerState();

        if (currenState != PlayerState.Attacker)
            return;

        GameObject bullet = Instantiate(bulletPrefab, aim.position, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().SetDirection(isFacingRight);
        //TODO : 공격 애니메이션 추가
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Attack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
            OnDamaged(collision.transform.position);



            //피해를 입을 때 노말 상태라면 -> 바로 사망
            //피해를 입을 때 노말 상태가 아니라면 -> 일시 무적 판정 + 노말 상태로 돌아감 -> 무적해제
        }

        //아이템 획득 : 임시 (태그 외에 다른걸로 해도 괜찮습니다.)
        if(collision.gameObject.tag == "Item")
        {
            SetBiggerState();
        }
        
        if(collision.gameObject.tag == "Item")
        {
            SetAttackerState();
        }

        //이 외 추가하고 싶은 아이템...





        //닿은 아이템에 따라 플레이어 상태 변화시켜주기(할 수 있는 행동 선택지 증/감)
        //아이템 먹고 맞으면 -> 노말 상태로 돌아감

        //커진 상태인 경우 변해야 할 값(상태, 레이 길이 [선택 : 점프력 & 이동속도])
        //공격 상태인 경우 변해야 할 값(상태만 바꾸면 됨)

        //다른 상태 -> 노말 상태로 돌아갈 경우 변해야 할 값(상태, 모든 값 기본으로 변경 = 메서드로 따로 생성)

    }
    private void OnDamaged(Vector2 targetPos)
    {
        if (currenState == PlayerState.Nomal)
        {
            Debug.Log("여기서 게임오버 호출"); //여기다 게임오버 호출
            return;
        }


        //Chage Layer
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        anim.SetTrigger("isDamaged");

        SetNomalState();

        Invoke("OffDamaged", 3);
    }

    private void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);        
    }

    
    void OnAttack(Transform enemy)
    {
        //Point
        
        //Reaction Force
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //EnemyDie
        Monster monster = enemy.GetComponent<Monster>();
        monster.OnDamaged();
    }


    //기본 상태+초기화용
    private void SetNomalState()
    {
        currenState = PlayerState.Nomal;
        transform.localScale = new Vector3(1f, 1f, 1f);
        defaultSpeed = 2;
        maxSpeed = 6;
        jumpPower = 25;
        hitDistans = 0.5f;
    }

    private void SetBiggerState()
    {
        currenState = PlayerState.Bigger;
        transform.localScale = new Vector3(2f, 2f, 2f);
        hitDistans = 2.0f;
    }
    
    private void SetAttackerState()
    {
        SetNomalState();

        spriteRenderer.color = new Color(255, 230, 0, 1);
        currenState = PlayerState.Attacker;
        //모습이 다른 뭔가가 필요하긴 함 -> 모자같은거? -> 그냥 색상변경 넣음
    }
}
