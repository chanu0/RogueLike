using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public int per;

    public void Init(float Damage, int per)
    {
        this.Damage = Damage;
        this.per = per;
    }
}
