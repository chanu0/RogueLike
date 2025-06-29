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
        player = Gamemanager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Gamemanager.instance.isLive)
            return;

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
    }

    public void LevelUp(float Damage, int Count)
    {
        this.Damage = Damage * Character.Damage;
        this.Count = Count * Character.Count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.Itemid;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.Itemid;
        Damage = data.BaseDamage * Character.Damage;
        Count = data.BaseCount * Character.Count;

        for(int index = 0; index < Gamemanager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == Gamemanager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }


        switch (id)
        {
            case 0:
                Speed = 150 * Character.Speed;
                Batch();
                break;
            default:
                Speed = 0.5f * Character.Speed;
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
                Bullet  = Gamemanager.instance.pool.get(prefabId).transform;
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

        Transform Bullet = Gamemanager.instance.pool.get(prefabId).transform;
        Bullet.position = transform.position;
        Bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        Bullet.GetComponent<Bullet>().Init(Damage, Count, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
