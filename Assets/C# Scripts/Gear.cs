using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.Itemtype type;
    public float rate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.Itemid;
        transform.parent = Gamemanager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.itemtype;
        rate = data.Damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.Itemtype.Glove:
                RateUp();
                    break;
            case ItemData.Itemtype.Shoe:
                SpeedUp();
                break;
        }
    }

    public void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    weapon.Speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.Speed = 0.5f + (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float Speed = 3;
        Gamemanager.instance.player.Speed = Speed + Speed * rate;
    }
}
