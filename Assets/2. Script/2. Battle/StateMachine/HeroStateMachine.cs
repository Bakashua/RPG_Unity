using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HeroStateMachine : MonoBehaviour
{
    #region DATA

    [Header("CLASS")]
    public Chara_BaseStats hero;
    //public Chara_Spell_List Hero_Spells;
    //[HideInInspector] 
    public Player_Camera_Combat playerCam;
    public Player_Action_Manager _Action_Manager;
    private BattleStateMachine BSM;
    public UI_PlayerTurn playerTurn;
    private Status_Effect_Manager SEM;
    private Posture_Manager posture_Manager;
    private Attack_Type_Manager attack_Type_Manager;

    [Header("EVENT")]
    public GameEvent Event_Update_En_UI;
    public GameEvent Event_Update_He_UI;
    public GameEvent Event_UsePP;
    public GameEvent Event_BeginTurn;
    public GameEvent Event_EndTurn;

    public enum TurnState
    { PROCESSING, ADDTOLIST, WAITING, SELECTING, ACTION, DEAD }
    public TurnState currentState;

    [Header("TIME TO ACTION")]
    public float curentCooldown = 2f;
    public float maxCooldown = 5f;
    bool StopATB;
    bool isCoroutineRunning = false;
    //public Image progressBar;

    [Header("GAMEOBJECT")]
    public Image Chara_Body;
    public GameObject enemyToAttack;
    private bool actionStarted = false;
    private Vector3 startPosition;
    private float animSpeed = 50f;
    private float delayBeforeMoving = 0.3f;
    private float timeCharaStop = 0.3f;
    private bool alive = true;
    public bool battleIsOver = false;

    //
    bool fst_turn = true;
    #endregion

    public void Awake()
    {
        SEM = gameObject.GetComponent<Status_Effect_Manager>();
        hero.Status.SEM = SEM;
        //hero = ScriptableObject.CreateInstance<Chara_BaseStats>();
        //Hero_Spells = ScriptableObject.CreateInstance<Chara_Spell_List>();
        //Set stats
        hero.SetUP();
    }

    void Start()
    {
        BSM = BattleStateMachine.instance_BSM;
        playerTurn = UI_PlayerTurn.instance_PTM;

        _Action_Manager.SetUp(hero.general_Setting.CharaHero.SpellList);
        posture_Manager = gameObject.GetComponent<Posture_Manager>();
        attack_Type_Manager = gameObject.GetComponent<Attack_Type_Manager>();
        hero.general_Setting.CharaHero.BarkData.source = GetComponent<AudioSource>();

        Chara_Body.sprite = hero.general_Setting.CharaBody;
        //damagePoppupSpawner = GameObject.Find("Dmg_Ppup_Spwr");
        //heroPanelSpacer = GameObject.Find("HeroPanel").transform.GetChild(0).transform.GetChild(0);
        //CreateHeroPanel();
        startPosition = transform.position;
        curentCooldown = Random.Range(0, maxCooldown / 10) + hero.Battle_Stats.currentSpeed / 10;
        currentState = TurnState.PROCESSING;

        STATEMACHINE();
        StartProgressBar();
        //Debug.Log("hero stats = " + hero.currentDef);
    }

    #region State Machine
    //void Update()
    public void STATEMACHINE()
    {
        switch (currentState)
        {
            // we charge action gauge
            case (TurnState.PROCESSING):
                SEM.ApplyStatusEffect(StatusState.TickEffect);
                SEM.ApplyStatusEffect(StatusState.OnLeaveEffect);
                //StartCoroutine(UpgradeProgressBar());
                break;

            // once charge we add hero to list              
            case (TurnState.ADDTOLIST):
                SetUp_Shield_StartTurn();
                if (BSM.herosToManage.Count <= BSM.herosInBattle.Count)
                {
                    //Debug.Log("___1___" + BSM.herosToManage.Count);
                    //Debug.Log("___2___" + BSM.herosInBattle.Count);
                    BSM.herosToManage.Add(gameObject);
                }
                //BSM.UpdateStateMachine();
                BlindGiveShield();

                // add player to ui when it is managed
                playerTurn.herosToManage.Add(gameObject);
                playerTurn.HeroSpells.Add(hero.general_Setting.CharaHero.SpellList);
                playerTurn.PlayerUI_State();
                currentState = TurnState.WAITING;
                // play even begin turn
                hero.general_Setting.CharaHero.BarkData.Play_Bark_Start();
                Event_BeginTurn.TriggerEvent();
                break;

            // We wait before performing the action
            case (TurnState.WAITING):
                //Debug.Log("TurnState.WAITING");
                SEM.ApplyStatusEffect(StatusState.Onaction);
                break;

            // The player choose an action from panel
            case (TurnState.ACTION):

                StartCoroutine(TimeForAction());
                SEM.ApplyStatusEffect(StatusState.Onendturn);
                curentCooldown = Random.Range(0, maxCooldown / 1000);
                Event_EndTurn.TriggerEvent();
                break;

            // Check if alive or dead
            case (TurnState.DEAD):
                if (alive) { return; }
                else
                {
                    //gameObject.tag = "DeadHero";
                    SEM.ApplyStatusEffect(StatusState.OnLeaveEffect);
                    BSM.herosInBattle.Remove(gameObject);
                    //Debug.Log("wwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
                    //BSM.herosToManage.Remove(gameObject);
                    //BSM.UpdateStateMachine();
                    //BSM.attackPanel.SetActive(false);
                    //BSM.enemySelectPanel.SetActive(false);

                    if (BSM.herosInBattle.Count > 0)
                    {
                        for (int i = 0; i < BSM.performList.Count; i++)
                        {
                            if (BSM.performList[i].attackerGameObject == this.gameObject)
                            {
                                Debug.Log("aaaaaaaaaaaaaaaaa");
                                BSM.performList.Remove(BSM.performList[i]); BSM.UpdateStateMachine();
                            }
                            else if (BSM.performList[i].attackerTarget == gameObject)
                            {
                                BSM.performList[i].attackerTarget = BSM.herosInBattle[Random.Range(0, BSM.herosInBattle.Count)];
                                //BSM.UpdateStateMachine();
                            }
                        }
                    }
                    //this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105, 105, 105, 255);

                    Chara_Body.color = new Color32(105, 105, 105, 255); ;
                    BSM.battleState = BattleStateMachine.PerformAction.CHECKALIVE;
                    //BSM.UpdateStateMachine();
                    alive = false;
                }
                break;
        }
    }
    #endregion

    public IEnumerator UpgradeProgressBar()
    {
        if (!battleIsOver)
        {
            //Debug.Log(hero.general_Setting.CharaName + "    " + curentCooldown);
            curentCooldown = curentCooldown + hero.Battle_Stats.currentSpeed / 100;
            //float calcCooldown = curentCooldown / maxCooldown;
            yield return new WaitForSeconds(0.1f);

            if (curentCooldown >= maxCooldown)
            {
                if (posture_Manager.postureState == Posture_Manager.PostureState.BREAK)
                {
                    curentCooldown = 0;
                    posture_Manager.EnterNeutral();
                }
                else if (posture_Manager.postureState != Posture_Manager.PostureState.BREAK) //&& BSM.herosToManage[0] == gameObject)
                {
                    currentState = TurnState.ADDTOLIST;
                    //curentCooldown = Random.Range(0, maxCooldown / 1000); // + hero.Battle_Stats.currentSpeed / 10;
                    //curentCooldown = 0;
                    STATEMACHINE();
                    //Debug.Log("State Machine = choose action");
                }

            }
            else
            {
                //if (!StopATB && !isCoroutineRunning)
                if (!StopATB)
                {
                    yield return StartCoroutine(UpgradeProgressBar());
                    //isCoroutineRunning = false;
                }
            }
            isCoroutineRunning = false;
            yield break;
        }
    }

    // some to send to ui manager
    #region ACTION TIME 
    public void StopProgressBar()
    {
        if (isCoroutineRunning)
        {
            StopATB = true;
            isCoroutineRunning = false;
            StopCoroutine(UpgradeProgressBar());
            //StopAllCoroutines();
        }
    }

    public void StartProgressBar()
    {
        if (!isCoroutineRunning)
        {
            isCoroutineRunning = true;
            StopATB = false;
            StartCoroutine(UpgradeProgressBar());
        }
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        // chara pause before moving (showing ability)
        yield return new WaitForSeconds(delayBeforeMoving);

        // Select position of the target // Check if ranged if spell cast particle etc ??? or just if moving or not all fx on diff script
        if (BSM.performList[0].choosenAttack.Enumsetting.targetType == TargetType.self)
        {
            Vector3 enemyPosition = new Vector3(enemyToAttack.transform.position.x, enemyToAttack.transform.position.y, enemyToAttack.transform.position.z);
            // Send character to en position
            while (MoveTowardEnemy(enemyPosition)) { yield return null; }
        }
        else if (BSM.performList[0].choosenAttack.IsRanged)
        {

        }
        else
        {
            Vector3 enemyPosition = new Vector3(enemyToAttack.transform.position.x - 2f, enemyToAttack.transform.position.y, enemyToAttack.transform.position.z);
            // Send character to en position
            while (MoveTowardEnemy(enemyPosition)) { yield return null; }
        }

        yield return new WaitForSeconds(timeCharaStop);

        // CHECK IF WORKING WELL !!! // peut etre on créer un event ishot plutot ?
        //// stop time when hiting ???????????
        //Time.timeScale = 0f;
        //yield return new WaitForSecondsRealtime(0.25f);
        //Time.timeScale = 1f;

        //do damage
        if (BSM.performList[0].choosenAttack.Enumsetting.targetType == TargetType.self || BSM.performList[0].choosenAttack.Enumsetting.targetType == TargetType.ally)
        {
            Chara_BaseStats target = BSM.performList[0].attackerTarget.GetComponent<HeroStateMachine>().hero;
            BSM.performList[0].choosenAttack.DoDamage(hero, target);
        }
        else
        {
            Chara_BaseStats target = BSM.performList[0].attackerTarget.GetComponent<EnemyStateMachine>().enemy;
            BSM.performList[0].choosenAttack.DoDamage(hero, target);
        }

        // Reset position and perform list
        Vector3 firstPosition = startPosition;
        while (MoveTowardStart(firstPosition)) { yield return null; }

        BSM.performList.RemoveAt(0);
        if (BSM.battleState != BattleStateMachine.PerformAction.WIN && BSM.battleState != BattleStateMachine.PerformAction.LOSE)
        {
            BSM.battleState = BattleStateMachine.PerformAction.WAITING;
            actionStarted = false;
            //curentCooldown = 0f;
        }
        else
        {
            currentState = TurnState.WAITING;
            STATEMACHINE();
        }
        currentState = TurnState.PROCESSING;

        // reset cam player
        playerCam.Desactivate_cam();

        STATEMACHINE();
    }

    #endregion

    #region Damage Manager
    public void DoDamage(float breakDamage, string usedOn)
    {
        hero.general_Setting.CharaHero.BarkData.Play_Bark_Attack();

        BaseAttack atk = BSM.performList[0].choosenAttack;
        StartCoroutine(BSM.performList[0].choosenAttack.ResetTargetTiles());

        // Apply PP Bonus
        Event_UsePP.TriggerEvent();

        // do damage to ennemy hp
        foreach (GameObject target in BSM.performList[0].choosenAttack.targetInAOE)
        {
            if (target.tag == "Hero")
            {
                Chara_BaseStats stats = target.GetComponent<HeroStateMachine>().hero;
                target.GetComponent<HeroStateMachine>().TakeDamage(atk.DamageDealt(hero, stats, target), usedOn);
            }
            else if (target.tag == "Enemy")
            {
                Chara_BaseStats stats = target.GetComponent<EnemyStateMachine>().enemy;

                //Debug.Log("__1111  " + usedOn);
                //Debug.Log("atk.DamageDealt  " + atk.DamageDealt(hero, stats, target));
                target.GetComponent<EnemyStateMachine>().TakeDamage(atk.DamageDealt(hero, stats, target), usedOn);
            }
        }

        // do damage to ennemy posture        
        foreach (GameObject target in BSM.performList[0].choosenAttack.targetInAOE)
        {
            if (target.tag == "Hero")
            {
                HeroStateMachine hsm = target.GetComponent<HeroStateMachine>();
                if (hsm.curentCooldown >= hsm.maxCooldown * 0.65 && hsm.curentCooldown <= hsm.maxCooldown * 0.90 && atk.BaseAttackdata.elementMultiplicateur > 1)
                {
                    hsm.hero.life_Stats.postureCurr = target.GetComponent<Posture_Manager>().TakeHit(1000, breakDamage);
                }
                else
                {
                    hsm.hero.life_Stats.postureCurr = target.GetComponent<Posture_Manager>().TakeHit(0, breakDamage);
                }
            }
            else if (target.tag == "Enemy")
            {
                EnemyStateMachine esm = target.GetComponent<EnemyStateMachine>();
                //if (esm.curentCooldown <= esm.maxCooldown) // * 0.65 && atk.elementMultiplicateur > 1)
                if (esm.curentCooldown >= esm.maxCooldown * 0.65 && esm.curentCooldown <= esm.maxCooldown * 0.90 && atk.BaseAttackdata.elementMultiplicateur > 1)
                {
                    esm.enemy.life_Stats.postureCurr = target.GetComponent<Posture_Manager>().TakeHit(1000, breakDamage);
                }
                else
                {
                    esm.enemy.life_Stats.postureCurr = target.GetComponent<Posture_Manager>().TakeHit(0, breakDamage);
                }
            }
        }

        StartCoroutine(attack_Type_Manager.RoolLinkAttack(BSM.performList[0].choosenAttack.targetInAOE));

        // Remove PP Bonus



        // EVENT 
        Event_Update_En_UI.TriggerEvent();
        Event_Update_He_UI.TriggerEvent();

        if (BSM.performList[0].choosenAttack.Enumsetting.targetType != TargetType.self)
        { GainTP(true); }

    }

    public void TakeDamage(float GetDamageAmount, string stat)
    {
        hero.general_Setting.CharaHero.BarkData.Play_Bark_GetHits();
        //Debug.Log(GetDamageAmount);
        //Debug.Log(stat);
        //stat = BSM.her
        TakeDamageShake();
        // damage on shield
        //         { HP, SH, MP, TP, Post, Buff }
        switch (stat)
        {
            case "HP":
                //Debug.Log("qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
                float reste = hero.life_Stats.currentShield - GetDamageAmount;
                hero.life_Stats.currentShield = hero.life_Stats.currentShield - GetDamageAmount;
                // if shield is destroyed damage on health
                if (reste <= 0)
                {
                    hero.life_Stats.currentShield = 0;
                    if (0 <= hero.life_Stats.currentBlnd)
                    { LoosingBlind(1); }
                    hero.life_Stats.currentHP = hero.life_Stats.currentHP + reste;
                    //Debug.Log(hero.life_Stats.currentHP);
                    //SEM.ApplyStatusEffect();
                }
                if (hero.life_Stats.currentHP <= 0)
                {
                    alive = false;
                    currentState = TurnState.DEAD;
                    hero.life_Stats.currentHP = 0;
                    STATEMACHINE();
                }
                break;

            case "SH":
                hero.life_Stats.currentShield = hero.life_Stats.currentShield + GetDamageAmount;
                //Debug.Log(hero.life_Stats.currentShield);
                //if (hero.currentShield >= hero.baseShield) { hero.currentShield = hero.baseShield; }
                break;

            case "MP":

                break;

            case "TP":

                break;

            case "Heal":
                hero.life_Stats.currentHP = hero.life_Stats.currentHP + GetDamageAmount;
                if (hero.life_Stats.currentHP >= hero.life_Stats.baseHP) { hero.life_Stats.currentHP = hero.life_Stats.baseHP; }
                break;

        }

        GainTP(false);

        // status effect
        SEM.ApplyStatusEffect(StatusState.Onhit);
        SEM.ApplyStatusEffect(StatusState.OneTime);
        // EVENT 
        Event_Update_En_UI.TriggerEvent();
        Event_Update_He_UI.TriggerEvent();
    }

    void SetUp_Shield_StartTurn()
    {
        // dont work on &st turn of combat
        if (fst_turn) { }
        // if (!fst_turn) { hero.life_Stats.currentShield = hero.life_Stats.KeepShield; } //Debug.Log("remove shield begin turn"); }
        fst_turn = false;

        // EVENT 
        Event_Update_He_UI.TriggerEvent();
    }

    public void LoosingBlind(float damage)
    {
        // happens when dealing critical, weak or breaking shield
        hero.life_Stats.currentBlnd -= damage;
        // add something that desactivate blind for next turn ???
    }

    void BlindGiveShield()
    {
        if (fst_turn)
        {
            fst_turn = false;
        }
        else
        {
            hero.life_Stats.currentShield += hero.life_Stats.currentBlnd;
            Event_Update_En_UI.TriggerEvent();
            Event_Update_He_UI.TriggerEvent();
        }
    }

    #endregion        


    #region Spell Uses Management

    public void RefreshCooldown(Chara_SpellList spells)
    {
        foreach (BaseAttack obj in spells.Set01)
        {
            if (obj != null)
            {
                if (obj.BaseAttackdata.CurrentCooldown > 0) { obj.BaseAttackdata.CurrentCooldown--; }
                else { obj.BaseAttackdata.CurrentCooldown = 0; }
            }
        }
        foreach (BaseAttack obj in spells.Set02)
        {
            if (obj != null)
            {
                if (obj.BaseAttackdata.CurrentCooldown > 0) { obj.BaseAttackdata.CurrentCooldown--; }
                else { obj.BaseAttackdata.CurrentCooldown = 0; }
            }
        }
    }

    public bool CanUseSpell(BaseAttack spell)
    {
        //print(spell.attackMPCost + " ____ " + hero.currentMP);
        //print(spell.attackTPCOST + " ____ " + hero.currentTP);
        if (spell.BaseAttackdata.attackMPCost <= hero.life_Stats.currentMP && spell.Enumsetting.attackCategory == AttackCategory.magic)
        {
            hero.life_Stats.currentMP = hero.life_Stats.currentMP - spell.BaseAttackdata.attackMPCost;
            // Debug.Log("this Attck cost mp");
            return true;
        }
        if (spell.BaseAttackdata.attackTPCost <= hero.life_Stats.currentTP && spell.Enumsetting.attackCategory == AttackCategory.tech)
        {
            hero.life_Stats.currentTP = hero.life_Stats.currentTP - spell.BaseAttackdata.attackTPCost;
            Debug.Log("this Attack cost tp");
            return true;
        }
        if (spell.Enumsetting.attackCategory == AttackCategory.basic)
        {
            return true;
        }

        Debug.Log("cannot pay this attack");
        return false;
    }

    void GainTP(bool isCaster)
    {
        BaseAttack atk = BSM.performList[0].choosenAttack;
        // bonus depending of doing the attack or receiving it

        //GAIN TP
        float var = Random.Range(0.9f, 1.4f);
        if (isCaster)
        { hero.life_Stats.currentTP = hero.life_Stats.currentTP + Mathf.Round(atk.InvocationSetting.TPGain * var); }
        else
        { hero.life_Stats.currentTP = hero.life_Stats.currentTP + Mathf.Round((atk.InvocationSetting.TPGain / 2) * var); }
        if (hero.life_Stats.currentTP >= 200) { hero.life_Stats.currentTP = 200; }

        // GAIN MP
        hero.life_Stats.currentMP = hero.life_Stats.currentMP + atk.InvocationSetting.MPGain;
        if (hero.life_Stats.currentMP >= hero.life_Stats.baseMP)
        { hero.life_Stats.currentMP = hero.life_Stats.baseMP; }
    }

    #endregion


    #region ANIMATION /// move to target and back
    private bool MoveTowardEnemy(Vector3 target)
    //private void MoveTowardEnemy(Vector3 target)
    {
        //transform.DOMove(target, 0.51f).SetEase(Ease.InOutCubic);
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardStart(Vector3 target)
    //private void MoveTowardStart(Vector3 target)
    {
        //transform.DOMove(target, 0.51f).SetEase(Ease.InOutCubic);
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    void TakeDamageShake()
    {
        // dur, force, vibrationn randomness
        gameObject.transform.DOShakePosition(0.5f, 1, 10, 50, false, true).
        SetEase(Ease.OutElastic).
        SetDelay(1 / 3);

        //gameObject.

        // Basic camera shake when getting hit
        BattleCamManager.instance_BCam.Cam.transform.DOShakePosition(1f, 0.5f, 10, 90, false, true).
        SetEase(Ease.OutElastic);
    }

    #endregion

}
