using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 *  DESIGN 
 *  ======================
 *  cold = mo bonus
 *  warm = + 10 % sur toutes les stats  // + de speed // de crit ?d eva? // 
 *  burning = +30 % MAIS -30% sur les defensives ? // drain des hp/mp/tp ? // tous les heros rentre en Break apres 3 tours ? 
 * 
 */



public class HeatSystem_Manager : MonoBehaviour
{
    [SerializeField] PowerPoint_Manager PPM;

    [SerializeField] TextMeshProUGUI TextHeat;
    [SerializeField] int coldThreshold;
    [SerializeField] int warmThreshold;
    [SerializeField] int burnThreshold;

    [SerializeField] BattleStateMachine BSM;

    [SerializeField] List<Chara_BaseStats> hero = new();


    [Header("ANIMATED UI")]
    [SerializeField] TextMeshProUGUI Textanim;
    [SerializeField] GameObject AnimObj;

    public void Initialisation()
    {
        foreach (var item in BSM.herosInBattle)
        {
            hero.Add(item.GetComponent<HeroStateMachine>().hero);
        }
    }

    // fonctionne parfaitement
    public void UpdateHeatBonus(float PP)
    {
        if (PP < coldThreshold)
        {
            TextHeat.gameObject.SetActive(false);
        }
        if (PP >= coldThreshold)
        {
            TextHeat.gameObject.SetActive(true);
            //print("is cold");
            TextHeat.text = "WARM";

            AnimObj.SetActive(true);
            Textanim.text = "WARM";
        }
        if (PP >= warmThreshold)
        {
            TextHeat.gameObject.SetActive(true);
            // print("is warm");
            TextHeat.text = "BURNING";

            AnimObj.SetActive(true);
            Textanim.text = "BURNING";
        }
        if (PP >= burnThreshold)
        {
            TextHeat.gameObject.SetActive(true);
            TextHeat.text = "OVERHEAT";

            AnimObj.SetActive(true);
            Textanim.text = "OVERHEAT";
            //print("is burning");
        }

    }


    private void ActivateAnimation()
    {



    }


    void HeatStatEffect(Chara_BaseStats target, float bonus)
    {
        target.Battle_Stats.currentAtk = target.Battle_Stats.currentAtk * bonus;
        target.Battle_Stats.currentDef = target.Battle_Stats.currentDef * bonus;
        target.Battle_Stats.currentMatk = target.Battle_Stats.currentMatk * bonus;
        target.Battle_Stats.currentMdef = target.Battle_Stats.currentMdef * bonus;
        target.Battle_Stats.currentAcc = target.Battle_Stats.currentAcc * bonus;
        target.Battle_Stats.currentLuck = target.Battle_Stats.currentLuck * bonus;
        target.Battle_Stats.currentSpeed = target.Battle_Stats.currentSpeed * bonus;
        target.Battle_Stats.currentEva = target.Battle_Stats.currentEva * bonus;
        target.Battle_Stats.currentCritEva = target.Battle_Stats.currentCritEva * bonus;
        target.Battle_Stats.currentCritMult = target.Battle_Stats.currentCritMult * bonus;
        target.Battle_Stats.currentCritRate = target.Battle_Stats.currentCritRate * bonus;
    }

    void RemoveHeatStatsEffect(Chara_BaseStats target, float bonus)
    {
        target.Battle_Stats.currentAtk = target.Battle_Stats.currentAtk * bonus;
        target.Battle_Stats.currentDef = target.Battle_Stats.currentDef * bonus;
        target.Battle_Stats.currentMatk = target.Battle_Stats.currentMatk * bonus;
        target.Battle_Stats.currentMdef = target.Battle_Stats.currentMdef * bonus;
        target.Battle_Stats.currentAcc = target.Battle_Stats.currentAcc * bonus;
        target.Battle_Stats.currentLuck = target.Battle_Stats.currentLuck * bonus;
        target.Battle_Stats.currentSpeed = target.Battle_Stats.currentSpeed * bonus;
        target.Battle_Stats.currentEva = target.Battle_Stats.currentEva * bonus;
        target.Battle_Stats.currentCritEva = target.Battle_Stats.currentCritEva * bonus;
        target.Battle_Stats.currentCritMult = target.Battle_Stats.currentCritMult * bonus;
        target.Battle_Stats.currentCritRate = target.Battle_Stats.currentCritRate * bonus;
    }
}
