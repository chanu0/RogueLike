using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public SpwanData[] SpwanData;

    int level;
    float timer;
    void Awake()
    {
        SpawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(Gamemanager.instance.GameTime / 10f), SpwanData.Length - 1);

        if (timer > SpwanData[level].SpawnTime)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject Enemy = Gamemanager.instance.Pools.get(0);
        Enemy.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
        Enemy.GetComponent<Enemy>().Init(SpwanData[level]);
    }
}

[System.Serializable]
public class SpwanData
{
    public int Health;
    public float Speed;
    public int SpriteType;
    public float SpawnTime;
}
