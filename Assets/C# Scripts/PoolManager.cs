using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ... ������ ������ ����
    public GameObject[] prefabs;

    // ... Ǯ ����� ����
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

        // ... ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) ���� ������Ʈ ����
            // ... �߰��ϸ� select ������ �Ҵ� 


        // ... �� ã������?
            // ... ���Ӱ� ���� select ������ �Ҵ�


        return select;
    }
}
