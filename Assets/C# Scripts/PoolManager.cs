using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Header("�� ������ �迭")]
    public GameObject[] Enemy_Frefabs;
    private List<GameObject>[] enemyPools;

    [Header("��Ÿ ������ �迭")]
    public GameObject[] Other_Prefabs;
    private List<GameObject>[] otherPools;

    void Awake()
    {
        // �� Ǯ �ʱ�ȭ
        enemyPools = new List<GameObject>[Enemy_Frefabs.Length];
        for (int i = 0; i < enemyPools.Length; i++)
        {
            enemyPools[i] = new List<GameObject>();
        }

        // ��Ÿ Ǯ �ʱ�ȭ
        otherPools = new List<GameObject>[Other_Prefabs.Length];
        for (int i = 0; i < otherPools.Length; i++)
        {
            otherPools[i] = new List<GameObject>();
        }
    }

    /// <summary>
    /// �� ������ Ǯ���� �������� (Spawner���� ���)
    /// </summary>
    public GameObject Get(int index)
    {
        return GetFromPool(Enemy_Frefabs, enemyPools, index);
    }

    /// <summary>
    /// ��Ÿ ������ Ǯ���� �������� (������, �Ѿ� ��)
    /// </summary>
    public GameObject GetOther(int index)
    {
        return GetFromPool(Other_Prefabs, otherPools, index);
    }

    /// <summary>
    /// ���� Ǯ ó�� ����
    /// </summary>
    private GameObject GetFromPool(GameObject[] prefabs, List<GameObject>[] pools, int index)
    {
        if (index < 0 || index >= pools.Length)
        {
            Debug.LogError($"PoolManager: �ε��� {index}�� ������ ������ϴ�!");
            return null;
        }

        // ��Ȱ��ȭ�� ������Ʈ ����
        foreach (GameObject obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // ������ ���� ����
        GameObject newObj = Instantiate(prefabs[index], transform);
        pools[index].Add(newObj);
        return newObj;
    }
}
