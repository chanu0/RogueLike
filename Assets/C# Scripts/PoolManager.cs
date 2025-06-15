using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ... ������ ������ ����
    public GameObject[] prefabs;

    // ... Ǯ ����� ����
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

        // ... ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) ���� ������Ʈ ����
        foreach(GameObject item in pool[index])
        {
            if (!item.activeSelf)
            {
                // ... �߰��ϸ� select ������ �Ҵ� 
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... �� ã������?
        
        if (!select)
        {
            // ... ���Ӱ� ���� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pool[index].Add(select);
        }

        return select;
    }
}
