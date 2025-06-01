using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public PoolManager Pools;
    public Player player;

    public float GameTime;
    public float MaxGameTime = 2 * 10f;


    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        GameTime += Time.deltaTime;

        if (GameTime > MaxGameTime)
        {
            GameTime = MaxGameTime;
        }
    }
}
