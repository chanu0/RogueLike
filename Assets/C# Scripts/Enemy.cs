using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public RuntimeAnimatorController[] Animcon; 
    public float Speed;
    public float Health;
    public float MaxHealth;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D Rigid;
    Collider2D Coll;
    Animator Anim;
    SpriteRenderer Spriter;
    WaitForFixedUpdate Wait;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Spriter = GetComponent<SpriteRenderer>();
        Coll = GetComponent<Collider2D>();
        Wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!Gamemanager.instance.isLive)
            return;

        if (!isLive || Anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - Rigid.position;
        Vector2 nextVec = dirVec.normalized * Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position +  nextVec);
        Rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!Gamemanager.instance.isLive)
            return;

        if (!isLive)
            return;

        Spriter.flipX = target.position.x < Rigid.velocity.x;
    }

    void OnEnable()
    {
        target = Gamemanager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        Coll.enabled = true;
        Rigid.simulated = true;
        Spriter.sortingOrder = 2;
        Anim.SetBool("Dead", false);
        Health = MaxHealth;
    }

    public void Init(SpwanData data)
    {
        // Anim.runtimeAnimatorController = Animcon[data.SpriteType];
        Speed = data.Speed;
        Health = data.Health;
        MaxHealth = data.Health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        Health -= collision.GetComponent<Bullet>().Damage;
        StartCoroutine(KnockBack());

        if (Health > 0)
        {
            Anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else{
            isLive = false;
            Coll.enabled = false;
            Rigid.simulated = false;
            Spriter.sortingOrder = 1;
            Anim.SetBool("Dead", true);
            Gamemanager.instance.Kill++;
            Gamemanager.instance.GetExp();

            if(Gamemanager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }
    }

    IEnumerator KnockBack()
    {
        yield return Wait; // 다음 하나의 물리 프레임 딜레이
        Vector3 Playerpos = Gamemanager.instance.player.transform.position;
        Vector3 dirVec = transform.position - Playerpos;
        Rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

}
