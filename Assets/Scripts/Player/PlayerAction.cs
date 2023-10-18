using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

//���� �и� or ��Ʈ�ѷ����� ����ó��

//�÷��̾� ���� ����(���� ����)
//ĳ���� ���� ��ȭ ���� : Ŀ����, �۾�����, �ӵ�, ����(������ ������) // ���������� ����
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
    private bool isFacingRight = true; // ���� ����


    public PlayerState currenState = PlayerState.Nomal;


    public float defaultSpeed;
    public float maxSpeed;
    public float jumpPower;

    public float hitDistans;

    SpriteRenderer spriteRenderer; //�־����
    Animator anim; //�־����
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
        
        //TODO : ���� ������ �¿� ���� �߰�(��)
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
        //�� �Ǻ�
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
        //TODO : ���� �ִϸ��̼� �߰�
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



            //���ظ� ���� �� �븻 ���¶�� -> �ٷ� ���
            //���ظ� ���� �� �븻 ���°� �ƴ϶�� -> �Ͻ� ���� ���� + �븻 ���·� ���ư� -> ��������
        }

        //������ ȹ�� : �ӽ� (�±� �ܿ� �ٸ��ɷ� �ص� �������ϴ�.)
        if(collision.gameObject.tag == "Item")
        {
            SetBiggerState();
        }
        
        if(collision.gameObject.tag == "Item")
        {
            SetAttackerState();
        }

        //�� �� �߰��ϰ� ���� ������...





        //���� �����ۿ� ���� �÷��̾� ���� ��ȭ�����ֱ�(�� �� �ִ� �ൿ ������ ��/��)
        //������ �԰� ������ -> �븻 ���·� ���ư�

        //Ŀ�� ������ ��� ���ؾ� �� ��(����, ���� ���� [���� : ������ & �̵��ӵ�])
        //���� ������ ��� ���ؾ� �� ��(���¸� �ٲٸ� ��)

        //�ٸ� ���� -> �븻 ���·� ���ư� ��� ���ؾ� �� ��(����, ��� �� �⺻���� ���� = �޼���� ���� ����)

    }
    private void OnDamaged(Vector2 targetPos)
    {
        if (currenState == PlayerState.Nomal)
        {
            Debug.Log("���⼭ ���ӿ��� ȣ��"); //����� ���ӿ��� ȣ��
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


    //�⺻ ����+�ʱ�ȭ��
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
        //����� �ٸ� ������ �ʿ��ϱ� �� -> ���ڰ�����? -> �׳� ���󺯰� ����
    }
}
