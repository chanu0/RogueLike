using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public PoolManager Pools;
    public Player player;

    void Awake()
    {
        instance = this;
    }

}
