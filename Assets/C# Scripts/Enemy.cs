using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D Rigid;
    SpriteRenderer Spriter;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - Rigid.position;
        Vector2 nextVec = dirVec.normalized * Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position +  nextVec);
        Rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        Spriter.flipX = target.position.x < Rigid.velocity.x;
    }
}
