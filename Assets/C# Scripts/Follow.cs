using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    
    void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(Gamemanager.instance.player.transform.position);
    }
}
