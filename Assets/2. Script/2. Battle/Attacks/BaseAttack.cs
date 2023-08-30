using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// condition class
// if condition = true alors on ajoute l effet
[System.Serializable]
public class TileInRange
{
    public int x;
    public int z;
}

[System.Serializable]
public class Condition_Extra
{
    public bool Higher;
    public bool Isstatus;
    public bool DoRefill;
    public bool UnlockStatus;
    public string ConditionStats;
    public string ConditionStatsMax;
    public float threshold = 0.5f;
    public float refill = 0.5f;
    public Status_Effect_SO statusTest;
    public string FormulaC;
}

[CreateAssetMenu(fileName = "BaseSkill", menuName = "My Game/Skills/New Skill")]

[System.Serializable]
public class GENERALSETTING
{
    public string attackName;
    public Sprite icon;
    [TextArea]
    [Tooltip("a.baseAcc || a.currAcc || a.currPost || a.currAtk" + "a.currMatk || a.currMdef || a.currMP || a.currShield" +
        "a.baseAcc || a.currAcc || a.currPost || a.currAtk" + "a.currentDef || a.currentEva || a.currentHP || a.currentLuck" +
        "a.currMult || a.currEvaCrit || a.currRate")]
    public string Formula;
    public string FormulaCrit;
    public bool UsingCondition;
    public Condition_Extra condition;
    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string attackDescription;
    //public image icon
}

#region enum
public enum AttackCategory
{ basic, defense, magic, tech }
public enum UsedOn
{ HP, SH, MP, TP, Heal, Buff }
//dalge or recover type of stats on attack
public enum AttackType
{ Atk, Atk_Buff, Atk_Debuff, Atk_Defend, Buff, Debuff, Defend, DefendBuff, Stun, Unknown, }
public enum TargetType
{ enemy, self, ally, random, }
public enum Occasion
{ always, battlescreen, menuscreen, never }
public enum Elements
{ Slash, Bash, Pierce, Earth, Wind, Fire, Water, Light, Dark }
public enum ComboType
{ Starter, Follow, Finisher }
#endregion
[System.Serializable]
public class EnumSetting
{
    public UsedOn usedOn;
    public AttackCategory attackCategory;
    public AttackType attackType;
    public TargetType targetType;
    public Occasion occasion;
    public Elements elements;
    public ComboType comboType;
}

[System.Serializable]
public class BaseAttackData
{
    public float attackDamage = 20f; // ici on prend les stat attaquant A et defenseur B pour créer formule de dégats
    public float attackMPCost;
    public float attackTPCost;
    public float MaxCooldown;
    public float CurrentCooldown;
    public float varianceMin = 1.1f; // % de variation de dégats
    public float varianceMax = 1.1f; // % de variation de dégats
    public float breakDamage = 5; // % de variation de dégats
    public float elementMultiplicateur;
}

[System.Serializable]
public class Invocation
{
    public float critChance = 5;
    public float critMultiplier;
    public float speed; // add to character speed when taking turn (je dois changer ordre de passage des joueurs
    public float atkPres = 1;
    public float atkMissRate = 1;
    public float MPGain;
    public float TPGain;
}

[System.Serializable]
public class StatusEffect
{
    [SerializeField] bool canApplyStatus = true;
    [SerializeField] bool selfStatus = false;
    public List<Status_Effect_SO> StatusEffectAffliction = new();
}

[System.Serializable]
public class RangeAttack
{
    public bool IsRanged;
    public List<GameObject> targetInAOE = new();
    public List<GameObject> targetTile = new List<GameObject>();
    List<GameObject> targetTile_Preview = new List<GameObject>();
    public List<TileInRange> TilesInRange = new();
    public float range;
    public float aoeMaxDistance = 1;
}





[System.Serializable]
public class BaseAttack : ScriptableObject
{
    #region Data

    [Header("CLASS")]
    [HideInInspector] public BattleStateMachine BSM;
    [HideInInspector] public Damage_Matrice damageMatrice;
    Combat_String_to_Formula StringToFormula = new();
    [SerializeField] GameEvent GainPP;

    [Header("FEEDBACK")]
    public GameEvent Event_DomagePopUp;
    public UI_SO_DamagePopUp UI_SO_DamagePopUp;
    public VFX_Spawner VFX;
    [Header("_______________________________________________________________________________")]

    //private int popUpType = 0;
    [Header("GENERAL SETTING")]
    public GENERALSETTING GeneralSetting;

    [Header("_______________________________________________________________________________")]

    [Header("Enum")]
    public EnumSetting Enumsetting;

    [Header("_______________________________________________________________________________")]

    [Header("Base Attack")]
    public BaseAttackData BaseAttackdata;

    [Header("_______________________________________________________________________________")]

    [Header("Invocation")]
    public Invocation InvocationSetting;

    [Header("_______________________________________________________________________________")]

    [Header("Status Effect")]
    [SerializeField] bool canApplyStatus = true;
    [SerializeField] bool selfStatus = false;
    public List<Status_Effect_SO> StatusEffectAffliction = new();

    [Header("_______________________________________________________________________________")]
    [Header("Range Attack")]
    public bool IsRanged;
    public List<GameObject> targetInAOE = new();
    public List<GameObject> targetTile = new List<GameObject>();
    List<GameObject> targetTile_Preview = new List<GameObject>();
    public List<TileInRange> TilesInRange = new();
    public float range;
    public float aoeMaxDistance = 1;

    [Header("INTERFACE")]

    // UI
    // text cast/use/does quand on lance l'attaque + juice (???)
    // atk icon
    //atk effect 

    [Header("ANIMATION")]

    // see if character move to attack or not
    private string deleteme2;

    [Header("SOUND EFFECT")]
    private string deleteme3;

    //HeroStateMachine attacker;
    //EnemyStateMachine target;

    //private HeroStateMachine target2;
    //private EnemyStateMachine attacker2;
    float FormulaDamage = 0;

    #endregion



    public void DoDamage(Chara_BaseStats attacker, Chara_BaseStats target)
    {

        BSM = BattleStateMachine.instance_BSM;

        //only to show preview of atk damage on EnemySelectBtnScript coucou c kieran
        if (BSM.performList[0].type == "Hero")
        {
            attacker = BSM.performList[0].attackerGameObject.GetComponent<HeroStateMachine>().hero;
            if (Enumsetting.targetType == TargetType.self || Enumsetting.targetType == TargetType.ally)
            { target = BSM.performList[0].attackerTarget.GetComponent<HeroStateMachine>().hero; }
            else { target = BSM.performList[0].attackerTarget.GetComponent<EnemyStateMachine>().enemy; }
        }

        // COMPUTE RANGE HERE
        if (Enumsetting.targetType == TargetType.self)
        {
            ComputeRange(BSM.performList[0].attackerGameObject);
        }
        else
        {
            ComputeRange(BSM.performList[0].attackerTarget);
            //atk.ComputeRange(enemyPrefab)
        }

        //get damage Matrice
        damageMatrice = BSM.performList[0].attackerTarget.GetComponent<Damage_Matrice>();



        ////UI
        //UI_SO_DamagePopUp.TargetGO = BSM.performList[0].attackerTarget;
        //UI_SO_DamagePopUp.TargetPos = BSM.performList[0].attackerTarget.transform;
        //UI_SO_DamagePopUp.Damage = attackDamage;

        //vfx
        VFX.Target = BSM.performList[0].attackerTarget.GetComponentInParent<Transform>();
        VFX.SpawnFX();

        //Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + Enumsetting.usedOn);
        // UsedOn --- send damage to the target 
        if (BSM.performList[0].type == "Hero")
        {
            BSM.performList[0].attackerGameObject.GetComponent<HeroStateMachine>().DoDamage(BaseAttackdata.breakDamage, Enumsetting.usedOn.ToString());
        }
        if (BSM.performList[0].type == "Enemy")
        {
            BSM.performList[0].attackerGameObject.GetComponent<EnemyStateMachine>().DoDamage(BaseAttackdata.attackDamage, BaseAttackdata.breakDamage, Enumsetting.usedOn.ToString());
        }

        //apply stqtus effect          
        ApplyStatusEffect(attacker, target);

        // LES COOLDOWN
        if (BaseAttackdata.CurrentCooldown == 0)
        {
            BaseAttackdata.CurrentCooldown = BaseAttackdata.MaxCooldown;
        }

        // clear script
        targetInAOE.Clear();

        #region delete when cleaning
        //}
        //if (BSM.performList[0].type == "Enemy")
        //{
        //    attacker2 = BSM.performList[0].attackerGameObject.GetComponent<EnemyStateMachine>();
        //    target2 = BSM.performList[0].attackerTarget.GetComponent<HeroStateMachine>();

        //    //get damage Matrice
        //    damageMatrice = BSM.performList[0].attackerTarget.GetComponent<Damage_Matrice>();
        //    Debug.Log(damageMatrice);
        //    float CritRoll = Random.Range(0, 100) + attacker2.enemy.currentCritRate - target2.hero.currentCritEva + critChance;
        //    if (CritRoll <= 5)
        //    {
        //        Debug.Log("CRITICAL STRIKE");
        //        // DoCritical()
        //        float FormulaDamage = StringToFormula.StartSetFormula(FormulaCrit, attacker2.enemy, target2.hero);

        //        //damageMatrice.ComputeElement 
        //        float elementMultiplicateur = damageMatrice.ComputeElement(elements.ToString(), target.elements.ToString());

        //        //Debug.Log("Formule = " + FormulaDamage);   //ajout de vulnérabitilité selon type d'atk, de défense, ajout d'état (?)
        //        attackDamage = FormulaDamage * trueVariance;
        //        Debug.Log("attack formula 1 = " + attackDamage);

        //        attackDamage = FormulaDamage * trueVariance * elementMultiplicateur;
        //        Debug.Log("attack formula 2 = " + attackDamage);

        //        //UI 
        //        UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.CRITICAL;

        //    }
        //    else
        //    {
        //        float AccRoll = Random.Range(0, 100) + attacker2.enemy.currentAcc + atkPres - target2.hero.currentEva;
        //        if (AccRoll >= 1)
        //        {
        //            //Debug.Log("attack success");
        //            float FormulaDamage = StringToFormula.StartSetFormula(Formula, attacker2.enemy, target2.hero);

        //            //damageMatrice.ComputeElement 
        //            float elementMultiplicateur = damageMatrice.ComputeElement(elements.ToString(), target.elements.ToString());

        //            //Debug.Log("Formule = " + FormulaDamage);   //ajout de vulnérabitilité selon type d'atk, de défense, ajout d'état (?)
        //            attackDamage = FormulaDamage * trueVariance;
        //            Debug.Log("attack formula 1 = " + attackDamage);

        //            attackDamage = FormulaDamage * trueVariance * elementMultiplicateur;
        //            Debug.Log("attack formula 2 = " + attackDamage);

        //            //UI 
        //            if (elementMultiplicateur > 1)
        //            {
        //                UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.EFFECTIVE;
        //            }
        //            else if (elementMultiplicateur == 1)
        //            {
        //                UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.NORMAL;
        //            }
        //            else if (elementMultiplicateur < 1)
        //            {
        //                UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.UNEFFECTIVE;
        //            }
        //            //UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.NORMAL;
        //        }
        //        else if (AccRoll < 1)
        //        {
        //            //Debug.Log("attack miss");
        //            //DoMiss()
        //            //UI 
        //            UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.MISS;
        //        }
        //    }
        //    //apply stqtus effect
        //    ApplyStatusEffect();
        //    //ApplyStatusEffect(BSM.performList[0].attackerTarget.GetComponent<Status_Effect_Manager>());

        //    //UI_SO_DamagePopUp.currentState = popUpType;
        //    UI_SO_DamagePopUp.Damage = attackDamage;
        //    UI_SO_DamagePopUp.TargetPos = BSM.performList[0].attackerTarget.transform;

        //    // EVENT
        //    Event_DomagePopUp.TriggerEvent();

        //    attacker2.DoDamage(attackDamage, breakDamage);

        //    // LES COOLDOWN
        //    if (CurrentCooldown <= 0)
        //    {
        //        CurrentCooldown = MaxCooldown;
        //    }
        //}
        #endregion

    }

    public float DamageDealt(Chara_BaseStats attacker, Chara_BaseStats target, GameObject targetGO)
    {
        //Debug.Log("TRIGGER TWICE");
        float trueVariance = Random.Range(BaseAttackdata.varianceMin, BaseAttackdata.varianceMax);
        trueVariance = Mathf.Round(trueVariance * 100f) * 0.01f;

        // we do CRIT roll
        float CritRoll = Random.Range(0, 100) + attacker.Battle_Stats.currentCritRate + InvocationSetting.critChance - target.Battle_Stats.currentCritEva + InvocationSetting.critChance;
        if (CritRoll >= 100)
        {
            if (GeneralSetting.UsingCondition)
            {
                if (IsConditionMet(attacker, target, targetGO))
                {
                    FormulaDamage = StringToFormula.StartSetFormula(GeneralSetting.condition.FormulaC, attacker, target);
                    if (GeneralSetting.condition.UnlockStatus) { canApplyStatus = true; };
                    Debug.Log("condition is met");
                }
                else
                {
                    FormulaDamage = StringToFormula.StartSetFormula(GeneralSetting.FormulaCrit, attacker, target);
                }
            }
            else
            {
                FormulaDamage = StringToFormula.StartSetFormula(GeneralSetting.FormulaCrit, attacker, target);
            }

            BaseAttackdata.attackDamage = FormulaDamage * trueVariance * InvocationSetting.critMultiplier;

            #region all extra stuff
            //damageMatrice.ComputeElement 
            BaseAttackdata.elementMultiplicateur = damageMatrice.ComputeElement(Enumsetting.elements.ToString(), target.elements.ToString());

            //Debug.Log("Formule = " + FormulaDamage);   //ajout de vulnérabitilité selon type d'atk, de défense, ajout d'état (?)
            BaseAttackdata.attackDamage = FormulaDamage * trueVariance;
            //Debug.Log("attack formula 1 = " + attackDamage);

            BaseAttackdata.attackDamage = FormulaDamage * trueVariance * BaseAttackdata.elementMultiplicateur;

            //UI 
            if (BaseAttackdata.elementMultiplicateur > 1)
            {
                UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.EFFECTIVE;
            }
            else if (BaseAttackdata.elementMultiplicateur == 1)
            {
                UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.NORMAL;
            }
            else if (BaseAttackdata.elementMultiplicateur < 1)
            {
                UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.UNEFFECTIVE;
            }
            //UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.NORMAL;0
            #endregion
            UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.CRITICAL;
            //UI
            UI_SO_DamagePopUp.TargetGO = BSM.performList[0].attackerTarget;
            UI_SO_DamagePopUp.TargetPos = BSM.performList[0].attackerTarget.transform;
            UI_SO_DamagePopUp.Damage = BaseAttackdata.attackDamage;
            // EVENT
            Event_DomagePopUp.TriggerEvent();
            GainPP.TriggerEvent();

            return BaseAttackdata.attackDamage;

        }
        // we do ACC roll
        else
        {
            float AccRoll = Random.Range(0, 100) + attacker.Battle_Stats.currentAcc + InvocationSetting.atkPres - InvocationSetting.atkMissRate - target.Battle_Stats.currentEva;
            if (AccRoll >= InvocationSetting.atkMissRate)
            {
                if (GeneralSetting.UsingCondition)
                {
                    if (IsConditionMet(attacker, target, targetGO))
                    {
                        FormulaDamage = StringToFormula.StartSetFormula(GeneralSetting.condition.FormulaC, attacker, target);
                        if (GeneralSetting.condition.UnlockStatus) { canApplyStatus = true; };
                        //Debug.Log("condition is met");
                    }
                    else
                    {
                        //Debug.Log("not condition is met");
                        FormulaDamage = StringToFormula.StartSetFormula(GeneralSetting.Formula, attacker, target);
                    }
                }
                else
                {
                    FormulaDamage = StringToFormula.StartSetFormula(GeneralSetting.Formula, attacker, target);
                }

                // Not let fornula get under 0
                if (FormulaDamage <= 0) { FormulaDamage = 0; }

                //damageMatrice.ComputeElement 
                BaseAttackdata.elementMultiplicateur = damageMatrice.ComputeElement(Enumsetting.elements.ToString(), target.elements.ToString());

                //Debug.Log("_________________ " + elementMultiplicateur);

                //Debug.Log("Formule = " + FormulaDamage);   //ajout de vulnérabitilité selon type d'atk, de défense, ajout d'état (?)
                BaseAttackdata.attackDamage = FormulaDamage * trueVariance;
                //Debug.Log("attack formula 1 = " + attackDamage);

                BaseAttackdata.attackDamage = FormulaDamage * trueVariance * BaseAttackdata.elementMultiplicateur;
                //Debug.Log("attack formula + elementmultiplicateur = " + attackDamage);


                //UI 
                if (BaseAttackdata.elementMultiplicateur > 1)
                {
                    UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.EFFECTIVE;
                }
                else if (BaseAttackdata.elementMultiplicateur == 1)
                {
                    UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.NORMAL;
                }
                else if (BaseAttackdata.elementMultiplicateur < 1)
                {
                    UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.UNEFFECTIVE;
                }
                //UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.NORMAL;

                //UI
                UI_SO_DamagePopUp.TargetGO = BSM.performList[0].attackerTarget;
                UI_SO_DamagePopUp.TargetPos = BSM.performList[0].attackerTarget.transform;
                UI_SO_DamagePopUp.Damage = BaseAttackdata.attackDamage;

                // EVENT
                Event_DomagePopUp.TriggerEvent();

                //Debug.Log("attackDamage = " + attackDamage);
                return BaseAttackdata.attackDamage;
            }
            else if (AccRoll <= InvocationSetting.atkMissRate)
            {
                //DoMiss()
                UI_SO_DamagePopUp.currentState = UI_SO_DamagePopUp.AtkEffect.MISS;
                return 0;
            }
        }
        return 0;
    }

    bool IsConditionMet(Chara_BaseStats attacker, Chara_BaseStats target, GameObject targetGO)
    {
        // return stat from target to compare
        float test = StringToFormula.StartSetFormula(GeneralSetting.condition.ConditionStats, attacker, target);
        float testMax = StringToFormula.StartSetFormula(GeneralSetting.condition.ConditionStatsMax, attacker, target);


        if (GeneralSetting.condition.Isstatus)
        {
            Status_Effect_SO effectC = ScriptableObject.Instantiate(GeneralSetting.condition.statusTest);
            if (targetGO.GetComponent<Status_Effect_Manager>().ContainEffect(effectC))
            {
                return true;
            }
        }

        if (GeneralSetting.condition.Higher)
        {
            if (test >= (testMax * GeneralSetting.condition.threshold))
            {
                return true; // condition is met if the formula damage is below the threshold
            }

        }
        else if (!GeneralSetting.condition.Higher)
        {
            //Debug.Log("TEST ____________    " + test);
            //Debug.Log("COMPUTE _____________    " + test * condition.threshold);
            if (test <= (testMax * GeneralSetting.condition.threshold))
            {
                return true; // condition is met if the formula damage is below the threshold
            }
        }

        return false;
    }

    /* on envoit un status
     *  si status a des traits
     * foreach trait
     * si trait target true alors on add trait a la target 
     * si trait target false alors on add au caster
     * ===============
     *  
    */
    public void ApplyStatusEffect(Chara_BaseStats attacker, Chara_BaseStats target)
    {
        // used to active trait after getting hit
        List<Status_Effect_Manager> statusreceiver = new();

        if (canApplyStatus)
        {
            if (selfStatus)
            {
                foreach (Status_Effect_SO effect in StatusEffectAffliction)
                {
                    Status_Effect_SO effect2 = ScriptableObject.Instantiate(effect);
                    effect2.target = target;
                    effect2.caster = attacker;
                    BSM.performList[0].attackerGameObject.GetComponent<Status_Effect_Manager>().AddStatus(effect2);
                    statusreceiver.Add(BSM.performList[0].attackerGameObject.GetComponent<Status_Effect_Manager>());
                    // CHECK IF STQKC BLQBLQBQ
                }
            }
            else
            {
                foreach (GameObject enemy in targetInAOE)
                {
                    //Debug.Log(enemy);
                    //enemy.GetComponents<Status_Effect_Manager>();
                    foreach (Status_Effect_SO effect in StatusEffectAffliction)
                    {
                        Status_Effect_SO effect2 = ScriptableObject.Instantiate(effect);
                        effect2.target = target;
                        effect2.caster = attacker;
                        enemy.GetComponent<Status_Effect_Manager>().AddStatus(effect2);
                        statusreceiver.Add(enemy.GetComponent<Status_Effect_Manager>());
                    }
                }
            }
        }


        // active trait
        foreach (var item in statusreceiver)
        {
            foreach (Status_Effect_SO effect in StatusEffectAffliction)
            {
                Status_Effect_SO effect2 = ScriptableObject.Instantiate(effect);
                effect2.target = target;
                effect2.caster = attacker;
                item.ActivateTraitFirstTime(effect2);
            }
        }

        //// Add Trait to chara
        //foreach (var status in StatusEffectAffliction)
        //{
        //    foreach (var t in status.trait)
        //    {
        //        if (t.Target)
        //        {
        //            // on met le trait sur la target DONC on a besoin de recevoir la target et sur chaque target on add le trait 
        //            Status_Effect_SO effect2 = ScriptableObject.Instantiate(status);
        //            effect2.target = target;
        //            effect2.caster = attacker;
        //            BSM.performList[0].attackerGameObject.GetComponent<Status_Effect_Manager>().AddStatus(effect2);
        //            statusreceiver.Add(BSM.performList[0].attackerGameObject.GetComponent<Status_Effect_Manager>());

        //        }
        //        else
        //        {
        //            // on met la stat sur le caster donc pas besoin d avoir son id
        //            Status_Effect_SO effect2 = ScriptableObject.Instantiate(status);
        //            effect2.target = target;
        //            effect2.caster = attacker;
        //            BSM.performList[0].attackerGameObject.GetComponent<Status_Effect_Manager>().AddStatus(effect2);
        //            statusreceiver.Add(BSM.performList[0].attackerGameObject.GetComponent<Status_Effect_Manager>());
        //        }

        //    }
        //}


        statusreceiver.Clear();
    }

    void ApplyTrait(Status_Effect_Manager attacker, Status_Effect_Manager target)
    {
        foreach (var status in StatusEffectAffliction)
        {
            foreach (var t in status.trait)
            {
                if (t.Target)
                {
                    // on met le trait sur la target DONC on a besoin de recevoir la target et sur chaque target on add le trait 
                }
                else
                {
                    // on met la stat sur le caster donc pas besoin d avoir son id
                }

            }
        }

    }


    #region Compute Range
    public void ComputeRange(GameObject target)
    {
        targetTile.Add(target.GetComponent<Combat_Movement>().currTiles);

        //=======================================================
        // Compute Area Of Effect
        // instantier des items centré sur la target, tous les éléments ennemy dans les items sont envoyé dans la liste des target in AOE
        int offsetX = target.GetComponent<Combat_Movement>().currTiles.GetComponent<Combat_Tiles>().x;
        int offsetZ = target.GetComponent<Combat_Movement>().currTiles.GetComponent<Combat_Tiles>().z;
        Vector2 offset = new Vector2(offsetX, offsetZ);

        if (target.tag == "Hero")
        {
            foreach (GameObject tile in target.GetComponent<Combat_Movement>().LDM.TilesAllied)
            {
                //if (TilesInRange.Contains(tile.GetComponent<Combat_Tiles>().x))
                {
                    //tile.GetComponent<Combat_Tiles>().x
                }
            }
        }

        if (target.tag == "Enemy" || target.tag == "Hero")
        {
            targetInAOE.Add(target);
            foreach (var tiles in TilesInRange)
            {
                // pour chaque tile de range on check si il y a des tiles atour si cest le car on les coloris en rouge
                GameObject focus = GetMatchedTile(target.GetComponent<Combat_Movement>().currTiles, tiles, target.GetComponent<Combat_Movement>().LDM.TilesEnemy);
                //targetInAOE.Add(AddTargetInAOE(focus));
                //Debug.Log(focus.GetComponent<Combat_Tiles>());

                //if (focus != null)
                //Debug.Log(focus);
                if (focus != null)
                {
                    if (AddTargetInAOE(focus) != null)
                    {
                        //Debug.Log("_________" + focus);
                        //Debug.Log("===========================");
                        //Debug.Log(AddTargetInAOE(focus));

                        if (AddTargetInAOE(focus).tag == "Enemy")
                        {
                            targetInAOE.Add(AddTargetInAOE(focus));
                        }
                    }
                }

                //focus.GetComponent<MeshRenderer>().material.SetColor(1, color)
            }
            //foreach (var item in target.GetComponent<Combat_Movement>().LDM.TilesEnemy)
            //{

            //}
        }

        if (Enumsetting.targetType == TargetType.self)
        {
            // if target allies
            foreach (GameObject hero in BSM.herosInBattle)
            {
                {
                    float dist = Vector3.Distance(hero.transform.position, target.transform.position);
                    //if (dist >= new Vector3(target.transform.position.x + aoeXPlus, target.transform.position.y, target.transform.position.z + aoeZPlus)
                    if (dist <= aoeMaxDistance)
                    {
                        targetInAOE.Add(hero);
                    }
                }
            }
        }

        // if target en __ add target in aoe to deal damages in hero state machine script 
        //foreach (GameObject enemy in BSM.enemyInBattle)
        //{
        //float dist = Vector3.Distance(enemy.transform.position, target.transform.position);
        //    //if (dist >= new Vector3(target.transform.position.x + aoeXPlus, target.transform.position.y, target.transform.position.z + aoeZPlus)
        //    if (dist <= aoeMaxDistance)
        //    {
        //        targetInAOE.Add(enemy);
        //    }
        //}
    }

    GameObject GetMatchedTile(GameObject currentTile, TileInRange tilesInRange, List<GameObject> targetTiles)
    {
        Combat_Tiles currentCombatTile = currentTile.GetComponent<Combat_Tiles>();
        Vector2Int currentTileCoord = currentCombatTile.Coord;

        Vector2Int offsetTileCoord = new Vector2Int(currentTileCoord.x + tilesInRange.x, currentTileCoord.y + tilesInRange.z);

        foreach (GameObject enemyTile in targetTiles)
        {
            Combat_Tiles enemyCombatTile = enemyTile.GetComponent<Combat_Tiles>();
            Vector2Int enemyTileCoord = enemyCombatTile.Coord;

            // le bug viens de cette fonction !!!
            if (enemyTileCoord == offsetTileCoord)
            {
                currentTile.GetComponent<Renderer>().material.color = Color.red;
                enemyTile.GetComponent<Renderer>().material.color = Color.red;
                targetTile.Add(enemyTile);
                //return enemyTile;
                return enemyTile;
            }
        }

        return null;
    }


    public void FindPreviewTile(GameObject target)
    {
        foreach (var tiles in TilesInRange)
        {
            GameObject focus = GetMatchedTile(target.GetComponent<Combat_Movement>().currTiles, tiles, target.GetComponent<Combat_Movement>().LDM.TilesEnemy);
        }
    }

    GameObject AddTargetInAOE(GameObject tiles)
    {
        if (tiles.GetComponent<Combat_Tiles>().currentHolder != null)
        {
            //Debug.Log("__________________________");
            return tiles.GetComponent<Combat_Tiles>().currentHolder;
        }
        return null;
    }

    public IEnumerator ResetTargetTiles()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var item in targetTile)
        {
            item.GetComponent<Combat_Tiles>().ResetColor();
            //Material neutral = item.GetComponent<Combat_Tiles>().NeutralColor;
            //item.GetComponent<Renderer>().material = neutral;
            //targetTile.Remove(item);
        }
        targetTile.Clear();
    }
    #endregion

    #region Preview
    // compute preview of damages 
    public float ComputePreview(Chara_BaseStats attacker, Chara_BaseStats target, Damage_Matrice matrice)
    {
        float trueVariance = Random.Range(BaseAttackdata.varianceMin, BaseAttackdata.varianceMax);
        trueVariance = Mathf.Round(trueVariance * 100f) * 0.01f;
        //Debug.Log("attack success");
        float FormulaDamage = StringToFormula.StartSetFormula(GeneralSetting.Formula, attacker, target);

        //damageMatrice = UI_PlayerTurn.instance_PTM.herosToManage[0].GetComponent<Damage_Matrice>();
        //damageMatrice.ComputeElement 
        float elementMultiplicateur = matrice.ComputeElement(Enumsetting.elements.ToString(), target.elements.ToString());

        //Debug.Log("Formule = " + FormulaDamage);   //ajout de vulnérabitilité selon type d'atk, de défense, ajout d'état (?)
        BaseAttackdata.attackDamage = FormulaDamage * trueVariance;
        //Debug.Log("attack formula 1 = " + attackDamage);

        BaseAttackdata.attackDamage = FormulaDamage * trueVariance * elementMultiplicateur;

        return BaseAttackdata.attackDamage;
    }

    // clear tile preview after showing the aoe
    public void ClearTilePreview()
    {
        foreach (var item in targetTile)
        {
            item.GetComponent<Combat_Tiles>().ResetColor();
        }
        targetTile.Clear();
    }
    #endregion

}


//IEnumerator ShowDebug()
//{
//    yield return new WaitForSeconds(5f);

//    if (BSM.performList[0].type == "Hero")
//    {
//        attacker = BSM.performList[0].attackerGameObject.GetComponent<HeroStateMachine>();
//        target = BSM.performList[0].attackerTarget.GetComponent<EnemyStateMachine>();

//        Debug.Log("hero tag attack " + attacker.hero.theName);
//        Debug.Log("hero tag target " + target.enemy.theName);
//    }

//    if (BSM.performList[0].type == "Enemy")
//    {
//        attacker2 = BSM.performList[0].attackerGameObject.GetComponent<EnemyStateMachine>();
//        target2 = BSM.performList[0].attackerTarget.GetComponent<HeroStateMachine>();

//        Debug.Log("enemy tag target " + attacker2.enemy.theName);
//        Debug.Log("enemy tag target " + target2.hero.theName);
//    }

//    StartCoroutine(ShowDebug());
//}
#region trying with physics
//Vector3 halfExtents = new Vector3(aoeXPlus, 1f, aoeZPlus);
//hitDetect = Physics.BoxCast(target.transform.position, halfExtents, target.transform.forward, out hitInfo, target.transform.rotation, aoeMaxDistance);
//if (hitDetect)
//{
//    //Output the name of the Collider your Box hit
//    Debug.Log("Hit : " + hitInfo.collider.name);
//    Debug.Log("test Collision");

//    targetInAOE.Add(hitInfo.collider.gameObject);
//}
//OnDrawGizmos();

//void OnDrawGizmos()
//{
//    Gizmos.color = Color.red;

//    //Check if there has been a hit yet
//    if (hitDetect)
//    {
//        //Draw a Ray forward from GameObject toward the hit
//        Gizmos.DrawRay(target.transform.position, target.transform.forward * hitInfo.distance);
//        //Draw a cube that extends to where the hit exists
//        Gizmos.DrawWireCube(target.transform.position + target.transform.forward * hitInfo.distance, target.transform.localScale);
//    }
//    //If there hasn't been a hit yet, draw the ray at the maximum distance
//else
//{
//    //Draw a Ray forward from GameObject toward the maximum distance
//    Gizmos.DrawRay(target.transform.position, target.transform.forward * aoeMaxDistance);
//    //Draw a cube at the maximum distance
//    Gizmos.DrawWireCube(target.transform.position + target.transform.forward * aoeMaxDistance, target.transform.localScale);
//}
#endregion
// add x et z deouis position
// add degat sur cibles