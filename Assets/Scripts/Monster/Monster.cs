using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigid;
    private Animator _anim;
    private int _nextMove;
    [SerializeField] private MonsterSO monsterSO;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        Moving();
    }

    void FixedUpdate()
    {
        //기본 동작
        _rigid.velocity = new Vector2(monsterSO.speed * _nextMove, _rigid.velocity.y);

        //지형 감지 
        Vector2 frontVec = new Vector2(_rigid.position.x + _nextMove * 0.3f, _rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform")); //땅의 레이어가 Platform
        if(rayHit.collider == null)
        {
            _nextMove *= -1;
            CancelInvoke();
            Invoke(nameof(Moving), monsterSO.delay);
        }
    }

    private void Moving()
    {
        _nextMove = Random.Range(-1, 2);
        if(_nextMove > 0) 
        {
            _anim.SetBool("isWalking", true);
            _renderer.flipX = true;
        }else if(_nextMove < 0)
        {
            _renderer.flipX = false;
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);
        }
        Invoke(nameof(Moving), monsterSO.delay);
    }

    private void Die()
    {
        _rigid.velocity = Vector2.zero;
        StartCoroutine(nameof(DieCo));
        gameObject.SetActive(false);
    }

    private IEnumerator DieCo()
    {
        yield return new WaitForSecondsRealtime(1f);
        //todo 몬스터 반투명 후 사라짐
    }
}
