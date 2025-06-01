using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float Damage;
    public int Count;
    public float Speed;

    float timer;
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * Speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if(timer > Speed)
                {
                    timer = 0f;
                    fire();
                }
                break;
        }

        // Test Code...
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(200, 10);
        }
    }

    public void LevelUp(float Damage, int Count)
    {
        this.Damage = Damage;
        this.Count = Count;

        if (id == 0)
            Batch();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                Speed = 150;
                Batch();
                break;
            default:
                Speed = 0.5f;
                break;
        }
    }

    void Batch()
    {
        for(int index = 0; index < Count; index++)
        {
            Transform Bullet;
                
            if(index < transform.childCount)
            {
                Bullet = transform.GetChild(index);
            }
            else
            {
                Bullet  = Gamemanager.instance.Pools.get(prefabId).transform;
                Bullet.parent = transform;
            }

            Bullet.localPosition = Vector3.zero;
            Bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / Count;
            Bullet.Rotate(rotVec);
            Bullet.Translate(Bullet.up * 1.5f, Space.World);
            Bullet.GetComponent<Bullet>().Init(Damage, -1, Vector3.zero); // -1 is Infinity Per.
        }
    }

    void fire()
    {
        if (!player.scanner.NearestTarget)
            return;

        Vector3 targetPos = player.scanner.NearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform Bullet = Gamemanager.instance.Pools.get(prefabId).transform;
        Bullet.position = transform.position;
        Bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        Bullet.GetComponent<Bullet>().Init(Damage, Count, dir);
    }
}
