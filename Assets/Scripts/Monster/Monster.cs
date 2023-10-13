using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    private SpriteRenderer renderer;
    private Rigidbody2D rigidbody;
    private int nextMove;
    [SerializeField] private MonsterSO monsterSo;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        Moving();
    }

    void FixedUpdate()
    {
        //기본 동작
        rigidbody.velocity = new Vector2(monsterSo.speed * nextMove, rigidbody.velocity.y);

        //지형 감지 
        Vector2 frontVec = new Vector2(rigidbody.position.x + nextMove * 0.3f, rigidbody.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform")); //땅의 레이어가 Platform
        if(rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke(nameof(Moving), monsterSo.delay);
        }
    }

    private void Moving()
    {
        nextMove = Random.Range(-1, 2);
        if(nextMove > 0) 
        {
            renderer.flipX = true;
        }else if(nextMove < 0)
        {
            renderer.flipX = false;
        }
        Invoke(nameof(Moving), monsterSo.delay);
    }
}
