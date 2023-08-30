using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Status_Library", menuName = "My Game/Status_Weakness/New Status_Library")]
public class EffectLibrary : ScriptableObject
{
    public Chara_BaseStats caster;
    public Chara_BaseStats target;
     
    public void FrozenEffect() // Chara_BaseStats target , Chara_BaseStats caster
    {
        Debug.Log("this is the frozen test");
        caster.life_Stats.currentHP -= 10f;
    }

    public void BuffTest() // Chara_BaseStats target , Chara_BaseStats caster
    {

        Debug.Log("this is the BuffTest test");
        //Debug.Log(caster.life_Stats.currentHP);
        caster.life_Stats.currentHP += 100f;
        //Debug.Log(caster.life_Stats.currentHP);
    }

    public void DebuffTest() // Chara_BaseStats target , Chara_BaseStats caster
    {
        Debug.Log("this is the DebuffTest test");
        target.Battle_Stats.currentAtk -= 10f;
    }

    public void P_TestLux()
    {
        // on hit + first atk giving the stattus
        // if atk againt by basic activate the mark
        // on fait ca sale

        bool firstActivation = true;

        if (firstActivation)
        {
            Debug.Log("Mark is set");
        }
        if (!firstActivation)
        {
            Debug.Log("Mark deals =" + caster.Battle_Stats.baseMatk + " damage");
            target.life_Stats.currentHP -= caster.Battle_Stats.baseMatk;
        }




    }
}
