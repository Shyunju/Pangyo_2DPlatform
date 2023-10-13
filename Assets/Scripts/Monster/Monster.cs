using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    private SpriteRenderer renderer;
    private Rigidbody2D rigid;
    private Animator anim;
    private int nextMove;
    [SerializeField] private MonsterSO monsterSO;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Moving();
    }

    void FixedUpdate()
    {
        //기본 동작
        rigid.velocity = new Vector2(monsterSO.speed * nextMove, rigid.velocity.y);

        //지형 감지 
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform")); //땅의 레이어가 Platform
        if(rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke(nameof(Moving), monsterSO.delay);
        }
    }

    private void Moving()
    {
        nextMove = Random.Range(-1, 2);
        if(nextMove > 0) 
        {
            //anim.SetBool("isWalking", true);
            renderer.flipX = true;
        }else if(nextMove < 0)
        {
            renderer.flipX = false;
            //anim.SetBool("isWalking", true);
        }
        else
        {
            //anim.SetBool("isWalking", false);
        }
        Invoke(nameof(Moving), monsterSO.delay);
    }
}
