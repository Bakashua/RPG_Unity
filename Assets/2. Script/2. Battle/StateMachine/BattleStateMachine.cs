using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


// clear handle turn qd on retourne dans le conbat brb bisous
public class BattleStateMachine : MonoBehaviour
{
    #region Data

    // USE PropertyDrawer !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    [Range(0.0f, 10.0f)]
    public float USEPropertyDrawer;

    [Header("EVENT")]
    public GameEvent onBattleWin;
    public GameEvent onBattleLoose;

    public static BattleStateMachine instance_BSM { get; private set; }
    [Header("CLASS")]
    //[HideInInspector] public static BattleStateMachine instance_BSM;
    private Battle_GUI_Manager battleGUIManager;
    private BattleCamManager battleCamManager;
    //private XpManager xpManager;



    public enum PerformAction
    { WAITING, TAKEACTION, PERFORMACTION, CHECKALIVE, WIN, LOSE, END }
    public enum HeroGUI { ACTIVATE, WAITING, INPUT1, INPUT2, DONE }

    [Space(20)]
    public PerformAction battleState;
    //public HeroGUI heroInput;

    [Header("Taking Action")]
    public List<HandleTurn> performList = new List<HandleTurn>();

    [Header("Character On BattleField")]
    public List<GameObject> herosInBattle = new List<GameObject>();
    public List<GameObject> enemyInBattle = new List<GameObject>();

    [HideInInspector] public List<GameObject> enemyToDestroy = new List<GameObject>();

    [Header("Hero Ready For Action")]
    public List<GameObject> herosToManage = new List<GameObject>();

    #endregion

    private void Awake()
    {
        if (instance_BSM != null && instance_BSM != this)
        {
            Destroy(this);
        }
        else
        {
            instance_BSM = this;
        }
        //Debug.Log(instance_BSM);
    }

    //for plat testing on start :
    //public void Start()
    public void Listener_StartBattle()
    {
        //onBattleWin.TriggerEvent();

        // auto setting
        battleGUIManager = Battle_GUI_Manager.instance_BUIM;
        battleCamManager = BattleCamManager.instance_BCam;
        //xpManager = XpManager.instance_XPM;

        battleState = PerformAction.WAITING;
    }

    public void SetUpList()
    {
        // Set Up List in Battle
        herosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        enemyInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    // test
    public void UpdateStateMachine()
    {
        #region BattleStateMachine method no update
        {
            //switch (battleState)
            //{
            //    case (PerformAction.WAITING):
            //        if (performList.Count > 0)
            //        {
            //            battleState = PerformAction.TAKEACTION;
            //            UpdateStateMachine();
            //        }
            //        break;


            //    case (PerformAction.TAKEACTION):
            //        GameObject performer = performList[0].attackerGameObject;

            //        if (performList[0].type == "Enemy")
            //        {
            //            EnemyTakeAction(performer);
            //        }
            //        if (performList[0].type == "Hero")
            //        {
            //            HeroTakeAction(performer);
            //        }
            //        battleState = PerformAction.PERFORMACTION; UpdateStateMachine();
            //        break;


            //    case (PerformAction.PERFORMACTION):
            //        UpdateStateMachine();
            //        break;

            //    case (PerformAction.CHECKALIVE):
            //        if (herosInBattle.Count < 1)
            //        {
            //            battleState = PerformAction.LOSE; UpdateStateMachine();
            //            // lose battle
            //        }
            //        else if (enemyInBattle.Count < 1)
            //        {
            //            battleState = PerformAction.WIN; UpdateStateMachine();
            //            // win battle
            //        }
            //        else
            //        {
            //            //ClearAttackPanel();
            //            heroInput = HeroGUI.ACTIVATE;
            //        }
            //        break;


            //    case (PerformAction.WIN):
            //        {
            //            for (int i = 0; i < herosInBattle.Count; i++)
            //            {
            //                herosInBattle[i].GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.WAITING;
            //                herosInBattle[i].GetComponent<HeroStateMachine>().playerTurn.heroInput = UI_PlayerTurn.HeroGUI.DONE;
            //                herosInBattle[i].GetComponent<HeroStateMachine>().battleIsOver = true;
            //            }
            //            StartCoroutine(ClearAllBattle());
            //            //Invoke("ClearAllBattle", 0.5f);
            //            battleState = PerformAction.WAITING; UpdateStateMachine();
            //            //ClearAllBattle();
            //        }
            //        break;


            //    case (PerformAction.LOSE):
            //        {
            //            //Call Lose GUI
            //            //battleGUIManager.BattleIsLost();
            //            onBattleLoose.TriggerEvent();
            //            //looseBattle();
            //            //battleCamManager.ActivateWinBattleCam();

            //            foreach (GameObject enemyGODestroy in enemyToDestroy)
            //            {
            //                //Destroy(enemyGODestroy);
            //                enemyGODestroy.SetActive(false);
            //            }
            //            foreach (GameObject heroInBattle in herosInBattle)
            //            {
            //                //Destroy(heroInBattle);
            //                heroInBattle.SetActive(false);
            //            }
            //            battleState = PerformAction.PERFORMACTION; UpdateStateMachine();
            //        }
            //        break;
            //}
            #endregion
        }

    }

    void Update()
    {
        #region BattleStateMachine
        {
            switch (battleState)
            {
                case (PerformAction.WAITING):
                    if (performList.Count > 0)
                    {
                        battleState = PerformAction.TAKEACTION;
                    }
                    break;


                case (PerformAction.TAKEACTION):
                    GameObject performer = performList[0].attackerGameObject;

                    if (performList[0].type == "Enemy")
                    {
                        EnemyTakeAction(performer);
                    }
                    if (performList[0].type == "Hero")
                    {
                        HeroTakeAction(performer);
                    }
                    battleState = PerformAction.PERFORMACTION;
                    break;


                case (PerformAction.PERFORMACTION):
                    //if (performList.Count > 0)
                    //{
                    //    battleState = PerformAction.TAKEACTION;
                    //}
                    break;


                case (PerformAction.CHECKALIVE):
                    if (herosInBattle.Count < 1)
                    {
                        battleState = PerformAction.LOSE;
                    }
                    else if (enemyInBattle.Count < 1)
                    {
                        battleState = PerformAction.WIN;
                    }
                    else
                    {
                        battleState = PerformAction.WAITING;
                        //heroInput = HeroGUI.ACTIVATE;
                    }
                    break;


                case (PerformAction.WIN):
                    {
                        for (int i = 0; i < herosInBattle.Count; i++)
                        {
                            herosInBattle[i].GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.WAITING;
                            herosInBattle[i].GetComponent<HeroStateMachine>().playerTurn.heroInput = UI_PlayerTurn.HeroGUI.DONE;
                            herosInBattle[i].GetComponent<HeroStateMachine>().playerTurn.DesactivateSpellBtn();
                            herosInBattle[i].GetComponent<HeroStateMachine>().playerCam.Desactivate_cam();
                            herosInBattle[i].GetComponent<HeroStateMachine>().battleIsOver = true;
                        }

                        StartCoroutine(ClearAllBattle(true));
                        //Invoke("ClearAllBattle", 0.5f);
                        battleState = PerformAction.WAITING;
                        //ClearAllBattle();


                    }
                    break;


                case (PerformAction.LOSE):
                    {
                        //Call Lose GUI
                        //battleGUIManager.BattleIsLost();
                        onBattleLoose.TriggerEvent();
                        //looseBattle();
                        //battleCamManager.ActivateWinBattleCam();

                        foreach (GameObject enemyGODestroy in enemyInBattle)
                        {
                            //Destroy(enemyGODestroy);
                            enemyGODestroy.SetActive(false);
                        }
                        foreach (GameObject heroInBattle in herosInBattle)
                        {
                            //Destroy(heroInBattle);
                            heroInBattle.SetActive(false);
                        }
                        battleState = PerformAction.PERFORMACTION;

                        StartCoroutine(ClearAllBattle(false));
                    }
                    break;

            }
        }
        #endregion

    }


    #region MANAGE GUI BTN and PANELS   

    void EnemyTakeAction(GameObject performer)
    {
        EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
        if (ESM.currentState != EnemyStateMachine.TurnState.DEAD)
        {
            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
            ESM.En_STATEMACHINE();
        }
        UpdateStateMachine();

    }

    void HeroTakeAction(GameObject performer)
    {
        // Debug.Log("4444444444444444");
        HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
        HSM.enemyToAttack = performList[0].attackerTarget;
        HSM.currentState = HeroStateMachine.TurnState.ACTION;
        HSM.STATEMACHINE();
        herosToManage.RemoveAt(0); UpdateStateMachine();
    }

    public void CollectAction(HandleTurn turn)
    {
        //Debug.Log(turn.attacker);
        //Debug.Log(turn.attackerGameObject);
        //Debug.Log(turn.attackerTarget);
        //Debug.Log(turn.choosenAttack);
        //Debug.Log(turn.type);
        performList.Add(turn); UpdateStateMachine();
    }

    public void Listener_StopAllProgressBar()
    {
        foreach (var item in herosInBattle)
        {
            item.GetComponent<HeroStateMachine>().StopProgressBar();
        }

        foreach (var item in enemyInBattle)
        {
            item.GetComponent<EnemyStateMachine>().StopProgressBar();
            //item.GetComponent<EnemyStateMachine>().StopCoroutine(UpgradeProgressBarEnemy());            
        }
        //UpdateStateMachine();
    }

    public void Listener_RestartProgressBar()
    {
        foreach (var item in herosInBattle)
        {
            item.GetComponent<HeroStateMachine>().StartProgressBar();
        }

        foreach (var item in enemyInBattle)
        {
            item.GetComponent<EnemyStateMachine>().StartProgressBar();
        }
        UpdateStateMachine();
    }


    IEnumerator ClearAllBattle(bool win)
    {
        Listener_StopAllProgressBar();

        if (win)
        {
        onBattleWin.TriggerEvent();
        }


        // Clean Battle Field
        foreach (GameObject enemyGODestroy in enemyToDestroy)
        {
            // + on donne xp au manager
            //Destroy(enemyGODestroy);
            enemyGODestroy.SetActive(false);
            //enemyInBattle.Remove(enemyGODestroy);
            //enemyInBattle.Clear();
        }
        yield return new WaitForSeconds(3f);

        foreach (GameObject heroInBattle in herosInBattle)
        {
            //Destroy(heroInBattle);
            heroInBattle.SetActive(false);
            //herosInBattle.Remove(heroInBattle);
        }
        enemyInBattle.Clear();
        herosInBattle.Clear();

        // Give xp
        //xpManager.XpEndBattle(); UpdateStateMachine();
    }
    #endregion
}

