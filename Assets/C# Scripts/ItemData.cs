using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble Object")]
public class ItemData : ScriptableObject
{
    public enum Itemtype { Melee, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]
    public Itemtype itemtype;
    public int Itemid;
    public string ItemName;
    public string ItemDesc;
    public Sprite Itemicon;

    [Header("# Main Info")]
    public float BaseDamage;
    public int BaseCount;
    public float[] Damages;
    public float[] Counts;

    [Header("# Main Info")]
    public GameObject projectile;
}

