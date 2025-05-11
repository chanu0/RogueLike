using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Id;
    public int Prefabid;
    public float Damage;
    public int Count;
    public float Speed;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        switch (Id)
        {
            case 0:
                Speed = -150;
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for(int index = 0; index < Count; index++)
        {
            Transform Bullet = Gamemanager.instance.pool.GetOther(Prefabid).transform;
            Bullet.parent = transform;
            Bullet.GetComponent<Bullet>().Init(Damage, -1); // -1 is infinity per
        }
    }
}
