using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    SpriteRenderer spriter;
    [Header("# Game Control")]
    public bool isLive;
    public float GameTime;
    public float MaxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int Playerid;
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
    public Result uiResult;
    public GameObject EmenyCleaner;


    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        instance = this;
    }

    public void GameStart(int id)
    {
        Playerid = id;
        Health = MaxHealth;

        player.SwitchController(Playerid % 2);
        player.gameObject.SetActive(true);
        // 임시 코드
        uiLevelUp.Select(Playerid % 2);
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        EmenyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("Main");

    }

    void Update()
    {
        if (!isLive)
            return;

        GameTime += Time.deltaTime;

        if (GameTime > MaxGameTime)
        {
            GameTime = MaxGameTime;
            GameVictory();
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

