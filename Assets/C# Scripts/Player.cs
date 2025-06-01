using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float Speed;
    public Scanner scanner;
    Rigidbody2D Rigid;
    SpriteRenderer Spriter;
    Animator Anim;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Spriter = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * Speed * Time.deltaTime;
        Rigid.MovePosition (Rigid.position + nextVec);
    }

    void LateUpdate()
    {
        Anim.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
        {
            Spriter.flipX = inputVec.x < 0;
        }
    }
}
