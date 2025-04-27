using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public float Health;
    public float MaxHealth;
    public RuntimeAnimatorController[] Animcom;
    public Rigidbody2D target;

    bool isLive = false;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive || target == null || Speed <= 0)
        {
            Debug.LogWarning($"[Enemy FixedUpdate] 상태체크 => isLive: {isLive}, target: {(target == null ? "null" : "ok")}, Speed: {Speed}");
            return;
        }

        Vector2 dirVec = (target.position - rigid.position).normalized;
        Vector2 nextVec = dirVec * Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }




    void LateUpdate()
    {
        if (!isLive) return; // 살아있을 때만 방향 전환!

        sprite.flipX = (target.position.x < rigid.position.x);
    }

    void OnEnable()
    {
        target = Gamemanager.instance.player.GetComponent<Rigidbody2D>();
        // isLive = true; // ⚡ 이걸 Init() 호출 후 켜자!
        Health = MaxHealth;
    }

    public void Init(SpawnData data)
    {
        Debug.Log($"[Enemy Init] Init 시작: spriteType={data.spriteType}, speed={data.speed}, Health={data.Health}");

        anim.runtimeAnimatorController = Animcom[data.spriteType];
        Speed = data.speed;
        MaxHealth = data.Health;
        Health = data.Health;

        isLive = true;

        Debug.Log($"[Enemy Init] Init 완료: isLive={isLive}");
    }

}
