using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum Infotype {Exp, Level, Kill, Time, Health };
    public Infotype type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case Infotype.Exp:
                float curExp = Gamemanager.instance.Exp;
                float maxExp = Gamemanager.instance.nextExp[Gamemanager.instance.Level];
                mySlider.value = curExp / maxExp;
                break;
            case Infotype.Level:
                myText.text = string.Format("Lv.{0:F0}", Gamemanager.instance.Level);
                break;
            case Infotype.Kill:
                myText.text = string.Format("{0:F0}", Gamemanager.instance.Kill);
                break;
            case Infotype.Time:
                float remainTime = Gamemanager.instance.MaxGameTime - Gamemanager.instance.GameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}: {1:D2}", min, sec);
                break;
            case Infotype.Health:
                float curHeal = Gamemanager.instance.Health;
                float maxHeal = Gamemanager.instance.MaxHealth;
                mySlider.value = curHeal / maxHeal;
                break;
        }
    }
}
