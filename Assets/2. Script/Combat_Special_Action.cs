using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// on prend l input
// on met le jeu en pause
// on cree l ui d action extra 1) ssj 2) super power 3) ?give turn
// on applique l action
// on redonne le tour au current joueur
// on remet le jeu en play

// ssj spawn particule sur player
// ultimate ui qui slide 

public class Combat_Special_Action : MonoBehaviour
{
    public HeroStateMachine hsm;
    UI_PlayerTurn PTM;

    public GameObject LimitBreak_VFX1;
    public GameObject LimitBreak_VFX2;

    void Start()
    {   
        PTM = UI_PlayerTurn.instance_PTM;
    }

    public void Do_LimitBreak(Chara_SpellList spellList)
    {
        if (hsm.hero.life_Stats.currentTP >= 200)
        {
            LimitBreak_VFX2.SetActive(true);
            PTM.Hero_Input_Action(spellList.limitBreak2);
        }
        else if (hsm.hero.life_Stats.currentTP >= 100)
        {
            LimitBreak_VFX1.SetActive(true);
            PTM.Hero_Input_Action(spellList.limitBreak);
        }
    }

    public void Do_Ultimate(Chara_SpellList spellList)
    {
        PTM.Hero_Input_Action(spellList.ultimate);
    }



}
