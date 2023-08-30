using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyStateMachine : MonoBehaviour
{
    #region DATA
    [Header("EVENT")]
    public GameEvent Event_Update_En_UI;
    public GameEvent Event_Update_He_UI;

    [Header("Class")]
    private BattleStateMachine BSM;
    private Battle_GUI_Manager battleGUIManager;
    //public BaseEnemy enemy;// = new BaseEnemy();
    public Chara_BaseStats enemy;
    public XpManager xpManager;
    private Status_Effect_Manager SEM;
    private Posture_Manager posture_Manager;
    public IntentSystem Intent_System;
    public VFX_Spawner SFX_Dead;
    //
    //[SerializeField]
    public HandleTurn En_Action;

    public enum TurnState
    { PROCESSING, CHOOSEACTION, WAITING, ACTION, DEAD }

    public TurnState currentState;

    [Header("TIME TO ACTION")]
    public bool UsingBrain;
    public float curentCooldown = 0f;
    public float maxCooldown = 5f;
    bool StopATB;
    bool isCoroutineRunning = false;

    [Header("UI")]
    public UI_en_Info enemy_ui;

    [Header("GAMEOBJECT")]
    public Image Chara_Body;
    public GameObject selector;
    public GameObject heroToAttack;
    public GameObject enemyPanel;
    public MeshRenderer mesh;
    //[HideInInspector] public MeshRenderer mesh;
    //private Transform enemyHpPanel;

    private Vector3 startPosition;
    private bool actionStarted = false;
    private float animSpeed = 35;
    //private bool alive = true;

    private EnemyInfoDisplay statsUI;

    [Header("art")]
    public Material Deadcolor;

    //[Header("art")]


    #endregion
    private void Awake()
    {
        SEM = gameObject.GetComponent<Status_Effect_Manager>();
        enemy.Status.SEM = SEM;
    }

    void Start()
    {
        BSM = BattleStateMachine.instance_BSM;
        battleGUIManager = Battle_GUI_Manager.instance_BUIM;
        xpManager = XpManager.instance_XPM;
        posture_Manager = gameObject.GetComponent<Posture_Manager>();

        Chara_Body.sprite = enemy.general_Setting.CharaBody;

        currentState = TurnState.PROCESSING;
        curentCooldown = 0;
        //enemyHpPanel = GameObject.Find("enemyHpPanel").GetComponent<Transform>();
        startPosition = transform.position;
        selector.SetActive(false);

        // Set stats
        enemy.SetUP();
        En_STATEMACHINE();
        StartCoroutine(UpgradeProgressBarEnemy());
    }

    #region State Machine
    //void Update()
    public void En_STATEMACHINE()
    {
        //Debug.Log("000000000");
        bool condition = true;

        switch (currentState)
        {
            // we charge the action gauge
            case (TurnState.PROCESSING):
                if (condition == true)
                {
                    Intent_System.ClearIntent();
                    SEM.ApplyStatusEffect(StatusState.TickEffect);
                    SEM.ApplyStatusEffect(StatusState.OnLeaveEffect);
                    condition = false;

                    StartCoroutine(CreateAction());
                    //CreateAction();

                }
                //else { condition = true; Debug.Log("condition = " + condition); }
                break;


            // ai pick which action to do 
            case (TurnState.CHOOSEACTION):
                //Debug.Log("__xxx__" + condition);
                if (condition == true)
                {
                    ChooseAction();
                    SetUp_Shield_StartTurn();
                    //SEM.ApplyStatusEffect(StatusState.Onaction);
                    BlindGiveShield();
                }
                //else { condition = true; Debug.Log("condition = " + condition); }
                currentState = TurnState.WAITING;
                break;

            // wait for his turn
            case (TurnState.WAITING):
                if (condition == true)
                {
                    SEM.ApplyStatusEffect(StatusState.Onaction);
                }
                //else { condition = true; Debug.Log("condition = " + condition); }
                break;


            // BSM let the action plays
            case (TurnState.ACTION):
                if (condition == true)
                {
                    StartCoroutine(TimeForAction());
                    SEM.ApplyStatusEffect(StatusState.Onendturn);
                }
                //else { condition = true; Debug.Log("condition = " + condition); }
                break;


            // check when is dead
            case (TurnState.DEAD):
                {
                    //
                    SEM.ApplyStatusEffect(StatusState.OnLeaveEffect);
                    //
                    gameObject.tag = "DeadEnemy";
                    BSM.enemyInBattle.Remove(gameObject);
                    BSM.enemyToDestroy.Add(gameObject);
                    selector.SetActive(false);
                    //curentCooldown = 0;
                    //StopCoroutine(UpgradeProgressBarEnemy());

                    // Send xp to Xp Manager
                    //xpManager.XpEndBattle(enemy.exp); !!! ICI POUR L'XP A METTRE DANS UN SCRIPT A PART !!! Event Gain XP et un SO qui stock l'xp qu'on donne ?


                    // debug, on empecher d atk un personnage mort / remove de la liste des actions qd meurt
                    if (BSM.enemyInBattle.Count > 0)
                    {
                        for (int i = 0; i < BSM.performList.Count; i++)
                        //foreach (var item in BSM.performList)
                        {
                            {
                                //if (item.attackerGameObject == gameObject)
                                if (BSM.performList[i].attackerGameObject == gameObject)
                                {
                                    BSM.performList.Remove(BSM.performList[i]);
                                }
                                else if (BSM.performList[i].attackerTarget == gameObject)
                                {
                                    BSM.performList[i].attackerTarget = BSM.enemyInBattle[Random.Range(0, BSM.enemyInBattle.Count)];
                                }
                            }
                        }
                    }

                    // change color / play dead animation
                    //gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105, 105, 105, 255);
                    //mesh.material.color = new Color32(105, 105, 105, 255);
                    //alive = false;
                    //reset enemy buttons
                    BSM.battleState = BattleStateMachine.PerformAction.CHECKALIVE;
                }
                break;
        }
    }
    #endregion

    public IEnumerator UpgradeProgressBarEnemy()
    {
        // create a factor between speed * time
        curentCooldown = curentCooldown + enemy.Battle_Stats.currentSpeed / 100;
        yield return new WaitForSeconds(0.1f);

        if (curentCooldown >= maxCooldown)
        {
            if (posture_Manager.postureState == Posture_Manager.PostureState.BREAK)
            {
                curentCooldown = 0;
                Debug.Log("State Machine = skip turn bcz break");

                posture_Manager.EnterNeutral();
            }
            else if (posture_Manager.postureState != Posture_Manager.PostureState.BREAK)
            {
                currentState = TurnState.CHOOSEACTION;
                En_STATEMACHINE();
                //Debug.Log("State Machine = choose action");
            }

        }
        else
        {
            if (!StopATB)
            {
            StartCoroutine(UpgradeProgressBarEnemy());
            }
        }
        isCoroutineRunning = false;
        yield break;
    }

    public void StopProgressBar()
    {
        StopATB = true;
        isCoroutineRunning = false;
        StopCoroutine(UpgradeProgressBarEnemy());
    }

    public void StartProgressBar()
    {
        if (!isCoroutineRunning)
        {
            isCoroutineRunning = true;
            StopATB = false;
            StartCoroutine(UpgradeProgressBarEnemy());
        }
    }

    #region Action Choice 

    // here we create the action we will keep in handle turn var to show in intent system
    IEnumerator CreateAction()
    {
        yield return new WaitForSeconds(1f);
        IA_Brain brain = GetComponent<IA_Brain>();
        En_Action = brain.Do_Action(enemy);
        Intent_System.ShowIntent(brain.myAttack);
        //Intent_System.ShowIntent(brain.myAttack.choosenAttack.attackType.ToString());
        heroToAttack = En_Action.attackerTarget;
    }

    // we sed the previously created action to the bsm in order to proced turn
    void ChooseAction()
    {
        IA_Brain brain = GetComponent<IA_Brain>();

        #region old brain
        //// we use handle turn to store all date we need on this character action
        //HandleTurn myAttack = new HandleTurn();
        //myAttack.attacker = enemy.CharaName;
        //myAttack.type = "Enemy";
        //myAttack.attackerGameObject = gameObject;
        //myAttack.attackerTarget = BSM.herosInBattle[Random.Range(0, BSM.herosInBattle.Count)]; // ICI coef d'aggro des joueurs
        //// here we choose the attack the ennemy will use //its here we should imput IA logic?
        //int num = Random.Range(0, enemy.attacks.Count);
        //myAttack.choosenAttack = enemy.attacks[num];
        #endregion

        if (UsingBrain)
        {
            BSM.CollectAction(En_Action);
            /// here old version of the choose target + spell
            //BSM.CollectAction(brain.Do_Action(enemy));
            //Intent_System.ShowIntent(brain.myAttack.choosenAttack.attackType.ToString());
            //heroToAttack = brain.myAttack.attackerTarget;
        }
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        Vector3 heroPosition = new Vector3(heroToAttack.transform.position.x + 2f, heroToAttack.transform.position.y, heroToAttack.transform.position.z);
        while (MoveTowardEnemy(heroPosition)) { yield return null; }

        yield return new WaitForSeconds(0.5f);

        //do damage
        BSM.performList[0].choosenAttack.DoDamage(enemy, BSM.performList[0].attackerTarget.GetComponent<HeroStateMachine>().hero);


        // reset all the action for next turn
        Vector3 firstPosition = startPosition;
        while (MoveTowardStart(firstPosition)) { yield return null; }

        BSM.performList.RemoveAt(0);

        BSM.battleState = BattleStateMachine.PerformAction.WAITING;

        actionStarted = false;
        curentCooldown = 0f;
        currentState = TurnState.PROCESSING;
        En_STATEMACHINE();
    }
    #endregion

    #region Damage Manager

    public void DoDamage(float calc_damage, float breakDamage, string usedOn)
    {
        //Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        BaseAttack atk = BSM.performList[0].choosenAttack;
        StartCoroutine(BSM.performList[0].choosenAttack.ResetTargetTiles());

        // do damage to ennemy hp
        foreach (GameObject target in atk.targetInAOE)
        {
            if (target.tag == "Hero")
            {
                Chara_BaseStats stats = target.GetComponent<HeroStateMachine>().hero;
                target.GetComponent<HeroStateMachine>().TakeDamage(atk.DamageDealt(enemy, stats, target), usedOn);
            }
            else if (target.tag == "Enemy")
            {
                Chara_BaseStats stats = target.GetComponent<EnemyStateMachine>().enemy;
                target.GetComponent<EnemyStateMachine>().TakeDamage(atk.DamageDealt(enemy, stats, target), usedOn);
            }
        }

        // do damage to ennemy posture        
        foreach (GameObject target in atk.targetInAOE)
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
                if (esm.curentCooldown >= esm.maxCooldown * 0.65 && esm.curentCooldown <= esm.maxCooldown * 0.90  && atk.BaseAttackdata.elementMultiplicateur > 1)
                {
                    esm.enemy.life_Stats.postureCurr = target.GetComponent<Posture_Manager>().TakeHit(1000, breakDamage);
                }
                else
                {
                    esm.enemy.life_Stats.postureCurr = target.GetComponent<Posture_Manager>().TakeHit(0, breakDamage);
                }
            }
        }

        // EVENT 
        Event_Update_En_UI.TriggerEvent();
        Event_Update_He_UI.TriggerEvent();

        //// PREVIOUS VERSION
        //StartCoroutine(BSM.performList[0].choosenAttack.ResetTargetTiles());
        //// we use the public float of the compétence ATK
        //heroToAttack.GetComponent<HeroStateMachine>().TakeDamage(calc_damage, "hp");
        //SEM.ApplyStatusEffect(StatusState.OnAttack);

        //// do damage to ennemy posture
        //enemy.life_Stats.postureCurr = heroToAttack.GetComponent<Posture_Manager>().TakeHit(breakDamage);
    }

    public void TakeDamage(float GetDamageAmount, string stat)
    {
        //Debug.Log("__1111  " + stat);        Debug.Log("__2222  " + GetDamageAmount);
        //stat = BSM.her
        TakeDamageShake();
        // damage on shield
        //         { HP, SH, MP, TP, Post, Buff }
        switch (stat)
        {
            case "HP":
                float reste = enemy.life_Stats.currentShield - GetDamageAmount;
                enemy.life_Stats.currentShield = enemy.life_Stats.currentShield - GetDamageAmount;
                // if shield is destroyed damage on health
                if (reste <= 0)
                {
                    enemy.life_Stats.currentShield = 0;
                    if (0 <= enemy.life_Stats.currentBlnd)
                    { LoosingBlind(1); }
                    enemy.life_Stats.currentHP = enemy.life_Stats.currentHP + reste;
                    //SEM.ApplyStatusEffect();
                }
                if (enemy.life_Stats.currentHP <= 0)
                {
                    //Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaa");
                    //alive = false;
                    enemy.life_Stats.currentHP = 0;

                    // gain xp player
                    //XpManager.instance_XPM.xpGain += enemy.IA.reward.xpReceived;
                    //foreach (GameObject hero in BSM.herosInBattle)
                    //{
                    //    Debug.Log(hero.GetComponent<XpManager>());
                    //    //hero.GetComponent<XpManager>().xpGain += enemy.reward.xpReceived;
                    //}
                    StartCoroutine(IsDead());
                }
                //Debug.Log(enemy.general_Setting.CharaName + " ____________ hp = " + enemy.life_Stats.currentHP);

                break;

            case "SH":

                break;

            case "MP":

                break;

            case "TP":

                break;

            case "Heal":
                enemy.life_Stats.currentHP = enemy.life_Stats.currentHP + GetDamageAmount;
                if (enemy.life_Stats.currentHP >= enemy.life_Stats.baseHP) { enemy.life_Stats.currentHP = enemy.life_Stats.baseHP; }
                break;

        }



        // status effect
        SEM.ApplyStatusEffect(StatusState.Onhit);
        SEM.ApplyStatusEffect(StatusState.OneTime);
        // EVENT 
        Event_Update_En_UI.TriggerEvent();
        Event_Update_He_UI.TriggerEvent();
    }




    // allow character to maintain some shield between turn with some abilities
    void SetUp_Shield_StartTurn()
    {
        // dont work on &st turn of combat
        bool fst_turn = true;
        if (fst_turn) { }
        else { enemy.life_Stats.currentShield = enemy.life_Stats.KeepShield; }
    }

    public void LoosingBlind(float damage)
    {
        // happens when dealing critical, weak or breaking shield
        enemy.life_Stats.currentBlnd -= damage;
        // add something that desactivate blind for next turn ???
    }

    void BlindGiveShield()
    {
        bool fst_turn = true;
        if (fst_turn) { }
        else
        {
            //if (0 <= enemy.currentShield)
            //{
            enemy.life_Stats.currentShield += enemy.life_Stats.currentBlnd;
            //}
        }
    }

    #endregion

    IEnumerator IsDead()
    {
        //    Debug.Log("0000000");
        //    Debug.Log(GetComponent<Transform>().position);
        SFX_Dead.Target = this.GetComponent<Transform>();
        yield return new WaitForSecondsRealtime(0.5f);
        SFX_Dead.SpawnFX();
        //GetComponentInChildren<MeshRenderer>().material = Deadcolor;
        //qqqq
        Chara_Body.color = new Color32(105, 105, 105, 255);
        yield return new WaitForSecondsRealtime(1f);

        currentState = TurnState.DEAD;
        //alive = false;
        En_STATEMACHINE();
    }

    #region Character Anim on attack

    #region move to target and back
    private bool MoveTowardEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    #endregion
    //other like distance and all..

    void TakeDamageShake()
    {
        //Time.timeScale = 0f;
        ////Time.timeScale = Mathf.Lerp(Time.timeScale, 0, Time.unscaledDeltaTime / 0.5f);
        //StartCoroutine(RestartTimeAfterDelay(0.1f));
        ToggleSlowMotion(false, 0.1f);

        // dur, force, vibrationn randomness
        gameObject.transform.DOShakePosition(0.5f, 1, 10, 50, false, true).
        SetEase(Ease.OutElastic).
        SetDelay(1 / 3);

        // Basic camera shake when getting hit
        BattleCamManager.instance_BCam.Cam.transform.DOShakePosition(1f, 0.5f, 10, 90, false, true).
        SetEase(Ease.OutElastic);
    }

    private IEnumerator RestartTimeAfterDelay(float stopTimeDuration)
    {
        yield return new WaitForSecondsRealtime(stopTimeDuration);
        Time.timeScale = 1f;
    }


    #endregion

    public void StopCoroutine()
    {
        StopAllCoroutines();
    }

    public void StartCoroutine()
    {
        StartCoroutine(UpgradeProgressBarEnemy());
    }
    void StopSlowMo()
    {
        ToggleSlowMotion(false, 1);
    }

    public void ToggleSlowMotion(bool isSlowMotion, float slowMotionTimeScale)
    {
        Invoke("StopSlowMo", 1.15f);
        if (isSlowMotion)
        {
            // Enable slow motion
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = slowMotionTimeScale * 0.02f; // Adjust fixed delta time for physics
        }
        else
        {
            // Disable slow motion
            Time.timeScale = 1;
            Time.fixedDeltaTime = 1 * 0.02f; // Reset fixed delta time
        }
    }

}

//public void DoDamage(float breakDamage, string usedOn)
//{
//    BaseAttack atk = BSM.performList[0].choosenAttack;
//    //StartCoroutine(BSM.performList[0].choosenAttack.ResetTargetTiles());

//    // do damage to ennemy hp
//    foreach (GameObject target in BSM.performList[0].choosenAttack.targetInAOE)
//    {

//        if (target.tag == "Hero")
//        {
//            Chara_BaseStats stats = target.GetComponent<HeroStateMachine>().hero;
//            target.GetComponent<HeroStateMachine>().TakeDamage(atk.DamageDealt(enemy, stats), usedOn);
//        }
//        else if (target.tag == "Enemy")
//        {
//            Chara_BaseStats stats = target.GetComponent<EnemyStateMachine>().enemy;
//            target.GetComponent<EnemyStateMachine>().TakeDamage(atk.DamageDealt(enemy, stats), usedOn);
//        }
//    }

//    // do damage to ennemy posture        
//    foreach (GameObject target in BSM.performList[0].choosenAttack.targetInAOE)
//    {
//        if (target.tag == "Hero")
//        {
//            target.GetComponent<HeroStateMachine>().hero.life_Stats.postureCurr = target.GetComponent<Posture_Manager>().TakeHit(breakDamage);
//        }
//        else if (target.tag == "Enemy")
//        {
//            target.GetComponent<EnemyStateMachine>().enemy.life_Stats.postureCurr = target.GetComponent<Posture_Manager>().TakeHit(breakDamage);
//        }
//    }
