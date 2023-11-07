using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu_MainPageHero : MonoBehaviour
{
    [SerializeField] Hero_Party hp;
    [SerializeField] List<Image> characterimg = new();
    int i = 0;

    private void Start()
    {
        foreach (var item in hp.HeroInParty_Data)
        {
            characterimg[i].sprite = item.Stats.general_Setting.CharaBody;
            i++;
        }
    }

}
