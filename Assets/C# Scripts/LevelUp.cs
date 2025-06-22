using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    ItemUpgrade[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<ItemUpgrade>(true);
    }

    void Start()
    {
        Next();
    }

    // Update is called once per frame
    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        Gamemanager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        Gamemanager.instance.Resume();
    }
    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // 1. 모든 아이템 비활성화 
        foreach(ItemUpgrade item in items)
        {
            item.gameObject.SetActive(false);
        }
        // 2. 그 중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for(int index = 0; index < ran.Length; index++)
        {
            ItemUpgrade ranitem = items[ran[index]];

            // 3. 만렙 아이템의 경우 소비 아이템으로 대체
            if (ranitem.level == ranitem.data.Damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranitem.gameObject.SetActive(true);
            }
                
        }
        
    }
}
