using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    enum Achive { UnlockPotato, UnlockApple }
    Achive[] achives;

    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach(Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
        }
    }

    void Start()
    {
        
    }

    void UnlockCharacter()
    {
        for(int index = 0; index < lockCharacter.Length; index++)
        {
            string achivename = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achivename) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach(Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockPotato:
                isAchive = Gamemanager.instance.Kill >= 50;
                break;
            case Achive.UnlockApple:
                isAchive = Gamemanager.instance.GameTime == Gamemanager.instance.MaxGameTime;
                break;
        }

        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
        }
    }
}
