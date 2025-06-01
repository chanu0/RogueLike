using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public RuntimeAnimatorController[] Animcon; 
    public float Speed;
    public float Health;
    public float MaxHealth;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D Rigid;
    Animator Anim;
    SpriteRenderer Spriter;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
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

    void OnEnable()
    {
        target = Gamemanager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        Health = MaxHealth;
    }

    public void Init(SpwanData data)
    {
        Anim.runtimeAnimatorController = Animcon[data.SpriteType];
        Speed = data.Speed;
        Health = data.Health;
        MaxHealth = data.Health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        Health -= collision.GetComponent<Bullet>().Damage;

        if(Health > 0)
        {
            // .. Live, Hit Action
        }
        else{
            // .. Die
            Dead();
        }

        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}
