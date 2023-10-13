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
        rigidbody.velocity = new Vector2(monsterSo.speed * nextMove, rigidbody.velocity.y);
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
