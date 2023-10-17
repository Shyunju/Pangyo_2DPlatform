using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//플레이어 현재 상태(변신 상태)
//캐릭터 상태 변화 종류 : 커지고, 작아지고, 속도, 공격(맞으면 없어짐) // 스테이지를 리셋
public enum Type
{
    TEMP1,
    TEMP2,
    TEMP3
}

public class PlayerMovement : MonoBehaviour
{
    private PlayerController controller;

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 currentDirection = Vector2.zero;
    private Rigidbody2D rigid;

    [SerializeField]
    private Transform aim;
    public GameObject bulletPrefab;

    public Type currenType = Type.TEMP1;



    public float maxSpeed;
    public float jumpPower;
    public float playerSpeed;

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
        //controller.OnMoveEvent += PlayerFlip;
        controller.OnMoveEvent += Move;
        controller.OnAttackEvent += Shoot;
        controller.OnJumpEvent += Jump;
    }

    private void Update()
    {
        PlayerFlip(moveDirection);
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
            currentDirection = moveDirection;    
            spriteRenderer.flipX = currentDirection.x < 0;
            aim.localPosition = new Vector2(direction.x, 0);
        }

        
        //TODO : 에임 포지션 좌우 반전 추가
    }
    private void ApplyMovent(Vector2 direction)
    {
        rigid.AddForce(Vector2.right * direction * speed, ForceMode2D.Impulse);

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
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance <= 0.5f)
                {
                    anim.SetBool("isJump", false);
                }
            }
        }
    }
    private void Shoot()
    {
        Instantiate(bulletPrefab, aim.position, Quaternion.identity);
    }
    /*
    private void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }


        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3f)
        {
            anim.SetBool("isWalk", false);
        }      
        else
        {
            anim.SetBool("isWalk", true);
        }
    }

    private void FixedUpdate()
    {
        //Move
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max Speed
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        //Landing Platform

        if(rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJump", false);
                }
            }
        }
    }

    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            /*
            //Attack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            */

            //Damaged
            //else
            OnDamaged(collision.transform.position);
        }
    }
    private void OnDamaged(Vector2 targetPos)
    {
        //Chage Layer
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        anim.SetTrigger("isDamaged");

        Invoke("OffDamaged", 3);
    }

    private void OffDamaged()
    {
        gameObject.layer = 11;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    /*
    void OnAttack(Transform enemy)
    {
        //Point
        
        //Reaction Force
        //rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //EnemyDie
        Enemy enemy = enemy.GetComponent<Enemy>();
        enemyMove.OnDamaged();
    }
    */
}
