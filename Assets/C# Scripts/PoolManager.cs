using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ... 프리펩 보관용 변수
    public GameObject[] prefabs;

    // ... 풀 담당의 변수
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 있는(비활성화 된) 게임 오브젝트 선택
            // ... 발견하면 select 변수에 할당 


        // ... 못 찾았으면?
            // ... 새롭게 생성 select 변수에 할당


        return select;
    }
}
