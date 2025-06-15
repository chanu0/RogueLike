using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    [Header("# Game Control")]
    public bool isLive;
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
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Health = MaxHealth;

        // 임시 코드
        uiLevelUp.Select(0);
    }

    void Update()
    {
        if (!isLive)
            return;

        GameTime += Time.deltaTime;

        if (GameTime > MaxGameTime)
        {
            GameTime = MaxGameTime;
        }
    }

    public void GetExp()
    {
        Exp++;

        if (Exp == nextExp[Mathf.Min(Level, nextExp.Length - 1)])
        {
            Level++;
            Exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

