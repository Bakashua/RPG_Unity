using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// dans l exemple les traits vont seulement sur le caster
public class Trait
{
    //public List<TraitAttribute> listTraitElement = new List<TraitAttribute>();
    //public Chara_BaseStats caster;
    //public Chara_BaseStats target;
    //public TraitAttribute TraitElement;
    public enum Stat { Posture, Accuracy, Atk, Def, Eva, HP, Luck, Matk, Mdef, MP, Sh, Speed, evaCrit, Crit, CritD, Aggro }
    public Stat ChoosenStat;
    public bool Target;
    public bool Removable;
    public float Mult = 1;
    public float FlatBonus = 0;

    //public Combat_String_to_Formula CSF;

    //Dictionary<string, float> dicoData = new Dictionary<string, float>();

    //void SetupDico()
    //{
    //    dicoData.Add("a.currPost", caster.postureCurr);
    //    dicoData.Add("a.currAcc", caster.currentAcc);
    //    dicoData.Add("a.currAtk", caster.currentAtk);
    //    dicoData.Add("a.currDef", caster.currentDef);
    //    dicoData.Add("a.currEva", caster.currentEva);
    //    dicoData.Add("a.currHP", caster.currentHP);
    //    dicoData.Add("a.currLuck", caster.currentLuck);
    //    dicoData.Add("a.currMatk", caster.currentMatk);
    //    dicoData.Add("a.currMdef", caster.currentMdef);
    //    dicoData.Add("a.currMP", caster.currentMP);
    //    dicoData.Add("a.currShield", caster.currentShield);
    //    dicoData.Add("a.currSpeed", caster.currentSpeed);
    //    dicoData.Add("a.currMult", caster.currentCritMult);
    //    dicoData.Add("a.currEvaCrit", caster.currentCritEva);
    //    dicoData.Add("a.currRate", caster.currentCritRate);

    //    dicoData.Add("b.currPost", target.postureCurr);
    //    dicoData.Add("b.currAcc", target.currentAcc);
    //    dicoData.Add("b.currAtk", target.currentAtk);
    //    dicoData.Add("b.currDef", target.currentDef);
    //    dicoData.Add("b.currEva", target.currentEva);
    //    dicoData.Add("b.currHP", target.currentHP);
    //    dicoData.Add("b.currLuck", target.currentLuck);
    //    dicoData.Add("b.currMatk", target.currentMatk);
    //    dicoData.Add("b.currMdef", target.currentMdef);
    //    dicoData.Add("b.currMP", target.currentMP);
    //    dicoData.Add("b.currShield", target.currentShield);
    //    dicoData.Add("b.currSpeed", target.currentSpeed);
    //    dicoData.Add("b.currMult", target.baseCritMult);
    //    dicoData.Add("b.currEvaCrit", target.baseCritEva);
    //    dicoData.Add("b.currRate", target.baseCritRate);

    //}

}

