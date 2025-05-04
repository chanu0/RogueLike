using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour 
{
    public GameObject[] Enemy_Frefabs; // 프리팹 배열
    List<GameObject>[] pools; // 각각의 프리팹마다 풀

    void Awake()
    {
        if (Enemy_Frefabs == null || Enemy_Frefabs.Length == 0)
        {
            Debug.LogError("PoolManager: Enemy_Frefabs가 비어있습니다!");
            return;
        }

        pools = new List<GameObject>[Enemy_Frefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        if (index < 0 || index >= pools.Length)
        {
            Debug.LogError($"PoolManager: 인덱스 {index}가 잘못되었습니다!");
            return null;
        }

        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(Enemy_Frefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
