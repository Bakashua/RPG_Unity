using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Effect_Manager : MonoBehaviour
{
    public List<Status_Effect_SO> StatusEffectOnCharacter = new List<Status_Effect_SO>();
    List<Status_Effect_SO> StatusToDelete = new List<Status_Effect_SO>();
    public List<Status_Effect_SO> ElementForCombo = new List<Status_Effect_SO>();
    Status_Combo combo; // = new Status_Combo;
    public EffectLibrary lib;
    Chara_BaseStats chara;
    enum MathOperation { Add, Subtract, Multiply, Divide }

    Dictionary<string, int> dicoCombo = new Dictionary<string, int>();
    public float[,] matriceCombo = new float[,]
    {
    //null || chill || wet || warm || oil || poison || frozen || vapo || shock || burn || explosion || cryst || life || melt
/*null*/   {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
/*wet*/    {0f, 1f, 1f, 0f, 0f, 0f, 0f, 0f, 9f, 0f, 0f, 0f, 0f, 0f},
/*chill*/  {0f, 1f, 1f, 2f, 4f, 0f, 0f, 0f, 9f, 0f, 0f, 0f, 0f, 0f},
/*warm*/   {0f, 0f, 2f, 3f, 5f, 5f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
/*oil*/    {0f, 0f, 4f, 5f, 6f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
/*poison*/ {0f, 0f, 0f, 5f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
/*frozen*/ {0f, 0f, 0f, 7f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
/*vapo*/   {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
/*shock*/  {0f, 9f, 9f, 0f, 0f, 0f, 0f, 0f, 8f, 0f, 0f, 0f, 0f, 0f},
/*burn*/   {0f, 0f, 2f, 0f, 5f, 5f, 4f, 0f, 0f, 0f, 0f, 0f, 0f, 0f}, 
/*explosion*/   {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 9f, 0f, 0f, 0f, 0f, 0f},
/*cryst*/   {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
/*life*/    {0f, 0f, 0f, 3f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
/*melt*/    {0f, 1f, 1f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
    };


    private void Start()
    {
        if (gameObject.tag == "Hero")
        {
            chara = GetComponent<HeroStateMachine>().hero;
        }
        if (gameObject.tag == "Enemy")
        {
            chara = GetComponent<EnemyStateMachine>().enemy;
        }

        combo = new Status_Combo();
        SetUpDico();
    }

    public void AddStatus(Status_Effect_SO effect)
    {
        bool containsObject = false;
        Status_Effect_SO obj = effect;

        foreach (Status_Effect_SO old in StatusEffectOnCharacter)
        {
            // we check if a combo is activated !!! CA FONCTIONNE 
            if (effect.Combo != null)
            {
                float i = 0;
                i = ComputeElement(effect.Combo, old.Combo);
                if (i != 0)
                {
                    // status combo not name
                    ElementForCombo.Add(old);
                    ElementForCombo.Add(effect);
                    combo.PlayCombo(i);

                    StartCoroutine(MyFunctionWithDelay(old, effect));
                    // Trigger BREAK
                    gameObject.GetComponent<Posture_Manager>().EnterBreak();
                }
            }
            containsObject = ContainEffect(obj);
        }

        if (containsObject)
        {
            // we can increse its effect
            if (effect.canStack)
            {
                effect.numberOfStacks++;
                Debug.Log(effect.name + "can stack");
                // increse effect (on fait une list effect to apply et c est eux quon joue, quand on element sort de status effect on enleve toutes ses instances dans effect to apply 
            }
            // if cant stack increase its nbr of turn
            if (effect.canIncrementTurn)
            {
                Debug.Log(effect.name + " can canIncrementTurn");
                effect.numberTurnLeft = effect.numberOfTurn;

            }
        }
        // if not contained we add the effect
        else
        {
            StatusEffectOnCharacter.Add(effect);
            //effect.isUIon = true;
            GameObject vfx = Instantiate(effect.VFX_Text, gameObject.transform.position + new Vector3(0, 2, 0), gameObject.transform.rotation);
            vfx.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }


    IEnumerator MyFunctionWithDelay(Status_Effect_SO effect, Status_Effect_SO old)
    {
        yield return new WaitForSeconds(0.1f);
        ClearCombo(effect, old);
    }

    void ClearCombo(Status_Effect_SO effect, Status_Effect_SO old)
    {
        StatusEffectOnCharacter.Remove(effect);
        StatusEffectOnCharacter.Remove(old);
        ElementForCombo.Clear();
    }


    public bool ContainEffect(Status_Effect_SO status)
    {
        //Debug.Log(status.name);
        foreach (Status_Effect_SO obj in StatusEffectOnCharacter)
        {
            //Debug.Log(obj.name);
            if (status.name == obj.name)
            {
                return true;
            }
        }
        return false;
    }

    #region Manage Status

    public void ApplyStatusEffect(StatusState type)
    {
        foreach (Status_Effect_SO effect in StatusEffectOnCharacter)
        {
            if (effect.statusState == type)
            {
                if (effect.numberTurnLeft <= 0)
                {
                    StatusToDelete.Add(effect);
                    //StatusEffectOnCharacter.Remove(effect);
                }
                else
                {
                    lib.caster = effect.caster;
                    lib.target = effect.target;
                    effect.ApplyEffect();
                    CheckTrait(effect);
                }
            }
        }
        DeleteEffect();
    }

    void DeleteEffect()
    {
        foreach (Status_Effect_SO item in StatusToDelete)
        {
            if (StatusEffectOnCharacter.Contains(item))
            {
                StatusEffectOnCharacter.Remove(item);
            }
        }
        StatusToDelete.Clear();
    }

    public void ActivateTraitFirstTime(Status_Effect_SO effect)
    {
        if (!ContainEffect(effect))
        {
            lib.caster = effect.caster;
            lib.target = effect.target;
            effect.ApplyEffect();
            CheckTrait(effect);
        }
    }

    public void CheckTrait(Status_Effect_SO status)
    {
        //Debug.Log(status.numberOfTurn);
        //Debug.Log(status.numberTurnLeft);
        if (status.HasTrait && status.numberOfTurn == status.numberTurnLeft + 1)
        {
            foreach (Trait ttt in status.trait)
            {
                //Debug.Log("wwwwwwwwwwwwwww");
                AddTrait(ttt);
                //Debug.Log("             " + GetComponent<EnemyStateMachine>().enemy.Battle_Stats.currentDef);
            }
        }
        if (status.numberTurnLeft <= 0)
        {
            foreach (Trait ttt in status.trait)
            {
                RemoveTrait(ttt);
            }
        }
    }

    void AddTrait(Trait trait)
    {
        if (trait.FlatBonus <= 0)
        {
            ApplyToStat(trait.ChoosenStat.ToString(), trait.FlatBonus, MathOperation.Subtract);
        }
        if (trait.FlatBonus >= 0)
        {
            ApplyToStat(trait.ChoosenStat.ToString(), trait.FlatBonus, MathOperation.Add);
        }
        if (trait.Mult != 1)
        {
            ApplyToStat(trait.ChoosenStat.ToString(), trait.Mult, MathOperation.Multiply);
        }
    }

    void RemoveTrait(Trait trait)
    {
        if (trait.FlatBonus >= 0)
        {
            ApplyToStat(trait.ChoosenStat.ToString(), trait.FlatBonus, MathOperation.Add);
        }
        if (trait.FlatBonus <= 0)
        {
            ApplyToStat(trait.ChoosenStat.ToString(), trait.FlatBonus, MathOperation.Subtract);
        }
        if (trait.Mult != 1)
        {
            ApplyToStat(trait.ChoosenStat.ToString(), trait.Mult, MathOperation.Divide);
        }
    }

    void ApplyToStat(string ChoosenStat, float value, MathOperation operation)
    {
        //{ Atk, Def, Eva, HP, Luck, , , MP, Sh, Speed, evaCrit, Crit }
        switch (ChoosenStat)
        {
            case "Posture":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.life_Stats.postureCurr += value;
                        break;
                    case MathOperation.Subtract:
                        chara.life_Stats.postureCurr -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.life_Stats.postureCurr *= value;
                        break;
                    case MathOperation.Divide:
                        chara.life_Stats.postureCurr /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Accuracy":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentAcc += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentAcc -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentAcc *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentAcc /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Atk":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentAtk += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentAtk -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentAtk *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentAtk /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Def":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentDef += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentDef -= Mathf.Abs(value);
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentDef *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentDef /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Matk":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentMatk += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentMatk -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentMatk *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentMatk /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Mdef":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentMdef += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentMdef -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentMdef *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentMdef /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Eva":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentAcc += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentAcc -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentAcc *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentAcc /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "HP":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.life_Stats.baseHP += value;
                        break;
                    case MathOperation.Subtract:
                        chara.life_Stats.baseHP -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.life_Stats.baseHP *= value;
                        break;
                    case MathOperation.Divide:
                        chara.life_Stats.baseHP /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Luck":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentLuck += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentLuck -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentLuck *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentLuck /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Sh":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.life_Stats.currentShield += value;
                        break;
                    case MathOperation.Subtract:
                        chara.life_Stats.currentShield -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.life_Stats.currentShield *= value;
                        break;
                    case MathOperation.Divide:
                        chara.life_Stats.currentShield /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Speed":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentSpeed += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentSpeed -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentSpeed *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentSpeed /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "evaCrit":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentCritEva += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentCritEva -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentCritEva *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentCritEva /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Crit":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentCritRate += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentCritRate -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentCritRate *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentCritRate /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "CritD":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentCritMult += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentCritMult -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentCritMult *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentCritMult /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;

            case "Aggro":
                switch (operation)
                {
                    case MathOperation.Add:
                        chara.Battle_Stats.currentAggro += value;
                        break;
                    case MathOperation.Subtract:
                        chara.Battle_Stats.currentAggro -= value;
                        break;
                    case MathOperation.Multiply:
                        chara.Battle_Stats.currentAggro *= value;
                        break;
                    case MathOperation.Divide:
                        chara.Battle_Stats.currentAggro /= value;
                        break;
                    default:
                        Debug.LogError("Invalid math operation!");
                        break;
                }
                break;
        }
    }
    #endregion

    void SetUpDico()
    {
        // chill || wet || warm || oil || poison || frozen || vapo || shock || burn || explosion || cryst || life || melt
        //dicoData.Add("a.baseAcc", caster.baseAcc);
        dicoCombo.Add("no", 0);
        dicoCombo.Add("chill", 1);
        dicoCombo.Add("wet", 2);
        dicoCombo.Add("warm", 3);
        dicoCombo.Add("oil", 4);
        dicoCombo.Add("poison", 5);
        dicoCombo.Add("frozen", 6);
        dicoCombo.Add("vapo", 7);
        dicoCombo.Add("shock", 8);
        dicoCombo.Add("burn", 9);
        dicoCombo.Add("explosion", 10);
        dicoCombo.Add("cryst", 11);
        dicoCombo.Add("life", 12);
        dicoCombo.Add("melt", 13);
    }

    public float ComputeElement(string newStatus, string oldStatus)
    {
        // le float qu on va envoyer sur le status combo
        return matriceCombo[dicoCombo[newStatus], dicoCombo[oldStatus]];
    }

    // qd on add on calcul si combo si combo on l active puis les elements sont supprimés
}
