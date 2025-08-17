using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgrade : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.Itemicon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.ItemName;
    }

    void OnEnable()
    {
        textLevel.text = "Lv. " + (level + 1);
        
        switch (data.itemtype) {
            case ItemData.Itemtype.Melee:
            case ItemData.Itemtype.Range:
                textDesc.text = string.Format(data.ItemDesc, data.Damages[level] * 100, data.Counts[level]);
            break;
            case ItemData.Itemtype.Glove:
            case ItemData.Itemtype.Shoe:
                textDesc.text = string.Format(data.ItemDesc, data.Damages[level] * 100);
            break;
            default:
                textDesc.text = string.Format(data.ItemDesc);
                break;
        }
    }

    public void OnClick()
    {
        switch (data.itemtype)
        {
            case ItemData.Itemtype.Melee:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    newWeapon.transform.SetParent(Gamemanager.instance.player.transform);
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.BaseDamage;
                    int nextCount = 0;

                    nextDamage += data.BaseDamage * data.Damages[level];
                    nextCount += (int)data.Counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;
                break;
            case ItemData.Itemtype.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    newWeapon.transform.SetParent(Gamemanager.instance.player.transform);
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.BaseDamage;
                    int nextCount = 0;

                    nextDamage += data.BaseDamage * data.Damages[level];
                    nextCount += (int)data.Counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;
                break;
            case ItemData.Itemtype.Glove:    
            case ItemData.Itemtype.Shoe:
                if(level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.Damages[level];
                    gear.LevelUp(nextRate);
                }

                    level++;
                    break;
            case ItemData.Itemtype.Heal:
                Gamemanager.instance.Health = Gamemanager.instance.MaxHealth;
                break;
        }

        if(level == data.Damages.Length)
        {
            GetComponent<Button>().interactable = true;
        }
    }
}
