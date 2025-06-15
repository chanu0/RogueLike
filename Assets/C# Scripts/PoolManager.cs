using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ... 프리펩 보관용 변수
    public GameObject[] prefabs;

    // ... 풀 담당의 변수
    List<GameObject>[] pool;

    void Awake()
    {
        pool = new List<GameObject>[prefabs.Length];

        for(int index = 0; index < pool.Length; index++)
        {
            pool[index] = new List<GameObject>();
        }
    }

    public GameObject get(int index)
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 있는(비활성화 된) 게임 오브젝트 선택
        foreach(GameObject item in pool[index])
        {
            if (!item.activeSelf)
            {
                // ... 발견하면 select 변수에 할당 
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... 못 찾았으면?
        
        if (!select)
        {
            // ... 새롭게 생성 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pool[index].Add(select);
        }

        return select;
    }
}
