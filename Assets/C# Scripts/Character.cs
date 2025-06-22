using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Speed
    {
        get { return Gamemanager.instance.Playerid == 0 ? 1.1f : 1f; }
    }

    public static float WeaponSpeed
    {
        get { return Gamemanager.instance.Playerid == 0 ? 1.1f : 1f; }
    }

    public static float WeaponRate
    {
        get { return Gamemanager.instance.Playerid == 0 ? 1.1f : 1f; }
    }

    public static float Damage
    {
        get { return Gamemanager.instance.Playerid == 0 ? 1.1f : 1f; }
    }

    public static int Count
    {
        get { return Gamemanager.instance.Playerid == 0 ? 1 : 0; }
    }
}
