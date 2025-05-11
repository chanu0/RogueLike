using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Header("적 프리팹 배열")]
    public GameObject[] Enemy_Frefabs;
    private List<GameObject>[] enemyPools;

    [Header("기타 프리팹 배열")]
    public GameObject[] Other_Prefabs;
    private List<GameObject>[] otherPools;

    void Awake()
    {
        // 적 풀 초기화
        enemyPools = new List<GameObject>[Enemy_Frefabs.Length];
        for (int i = 0; i < enemyPools.Length; i++)
        {
            enemyPools[i] = new List<GameObject>();
        }

        // 기타 풀 초기화
        otherPools = new List<GameObject>[Other_Prefabs.Length];
        for (int i = 0; i < otherPools.Length; i++)
        {
            otherPools[i] = new List<GameObject>();
        }
    }

    /// <summary>
    /// 적 프리팹 풀에서 가져오기 (Spawner에서 사용)
    /// </summary>
    public GameObject Get(int index)
    {
        return GetFromPool(Enemy_Frefabs, enemyPools, index);
    }

    /// <summary>
    /// 기타 프리팹 풀에서 가져오기 (아이템, 총알 등)
    /// </summary>
    public GameObject GetOther(int index)
    {
        return GetFromPool(Other_Prefabs, otherPools, index);
    }

    /// <summary>
    /// 공통 풀 처리 로직
    /// </summary>
    private GameObject GetFromPool(GameObject[] prefabs, List<GameObject>[] pools, int index)
    {
        if (index < 0 || index >= pools.Length)
        {
            Debug.LogError($"PoolManager: 인덱스 {index}가 범위를 벗어났습니다!");
            return null;
        }

        // 비활성화된 오브젝트 재사용
        foreach (GameObject obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // 없으면 새로 생성
        GameObject newObj = Instantiate(prefabs[index], transform);
        pools[index].Add(newObj);
        return newObj;
    }
}
