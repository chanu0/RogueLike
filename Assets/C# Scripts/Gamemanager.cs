using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    [Header("# Game Control")]
    public float GameTime;
    public float MaxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public float Health;
    public float MaxHealth = 100;
    public int Level;
    public int Kill;
    public int Exp;
    public int[] nextExp = {3, 5, 10, 100, 150, 210, 280, 360, 450, 600};

    [Header("# Game Object")]
    public PoolManager Pools;
    public Player player;

    


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Health = MaxHealth;
    }

    void Update()
    {
        GameTime += Time.deltaTime;

        if (GameTime > MaxGameTime)
        {
            GameTime = MaxGameTime;
        }
    }

    public void GetExp()
    {
        Exp++;

        if(Exp == nextExp[Level])
        {
            Level++;
            Exp = 0;
        }
    }
}
