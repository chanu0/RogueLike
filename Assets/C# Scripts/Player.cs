using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float Speed;
    public Scanner scanner;
    Rigidbody2D Rigid;
    SpriteRenderer Spriter;
    [SerializeField] private List<RuntimeAnimatorController> animatorControllers;
    Animator Anim;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Spriter = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    public void SwitchController(int index)
    {
        Anim = GetComponent<Animator>();
        Anim.runtimeAnimatorController = animatorControllers[index];
    }

    void OnEnable()
    {
        Speed *= Character.Speed;
    }
    void Update()
    {
        if (!Gamemanager.instance.isLive)
            return;
        // inputVec.x = Input.GetAxisRaw("Horizontal");
        // inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (!Gamemanager.instance.isLive)
            return;

        Vector2 nextVec = inputVec * Speed * Time.deltaTime;
        Rigid.MovePosition (Rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (!Gamemanager.instance.isLive)
            return;

        Anim.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
        {
            Spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!Gamemanager.instance.isLive)
            return;

        Gamemanager.instance.Health -= Time.deltaTime * 10;

        if(Gamemanager.instance.Health < 0)
        {
            for(int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            Anim.SetTrigger("Dead");
            Gamemanager.instance.GameOver();
        }
    }
}
