using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bounceForce;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();       
    }

    public void SetDirection(bool isFacingRight)
    {
        if (isFacingRight)
        {
            rb.velocity = Vector2.right * bulletSpeed;
        }
        else
        {
            rb.velocity = Vector2.left * bulletSpeed;
        }


        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Platform")) // ∂•ø° ∫Œµ˙»˙ ∂ß
        {
            // ≈Î≈Î ∆¢∞‘ ∏∏µÈ±‚
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }
    }
}
