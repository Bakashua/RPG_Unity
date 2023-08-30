using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// enregistre les actions que le joueur va faire pendant son tour
// les tilt de cam sur les buper ce font dans le script : player action manager

public class UI_PlayerTurn : MonoBehaviour
{
    #region Data

    public Player_UI_ActionBTN test;

    [Header("CLASS")]
    [HideInInspector] public static UI_PlayerTurn instance_PTM;
    BattleStateMachine BSM;
    Battle_GUI_Manager BUIM;
    public Player_Camera_Combat playerCam;

    [Header("EVENT")]
    //public GameEvent UpdateBSM;
    public GameEvent CancelInput;

    public List<GameObject> herosToManage = new List<GameObject>();
    public List<Chara_SpellList> HeroSpells = new List<Chara_SpellList>();
    public HandleTurn heroChoice;

    public enum HeroGUI { ACTIVATE, WAITING, INPUT1, INPUT2, DONE }
    public HeroGUI heroInput;


    public GameObject UI_BTN;
    public GameObject UI_BTN_Parent;
    public GameObject current_ui_btn;

    GameObject Test_target;

    #endregion

    private void Awake()
    {
        if (instance_PTM != null && instance_PTM != this)
        { Destroy(this); }
        else
        { instance_PTM = this; }
        //Debug.Log(instance_PTM);
    }

    void Start()
    {
        //Debug.Log("_____________________________" + UI_BTN_Parent);
        BSM = BattleStateMachine.instance_BSM;
        BUIM = Battle_GUI_Manager.instance_BUIM;

        heroInput = HeroGUI.ACTIVATE;
    }

    #region Player Region.
    public void PlayerUI_State()
    {
        playerCam = BSM.herosToManage[0].GetComponent<Player_Camera_Combat>();
        switch (heroInput)
        {            
            case (HeroGUI.ACTIVATE):
                if (herosToManage.Count > 0)
                {
                    // show which player is doing action
                    BSM.herosToManage[0].transform.transform.GetChild(0).gameObject.SetActive(true);


                    // new handle turn instance                    
                    heroChoice = new HandleTurn();
                    playerCam.Setup_PlayerCam();

                    // show panel of action && populate action button
                    Instantiate_HeroDCombatBTN(BSM.herosToManage[0]);
                    heroInput = HeroGUI.WAITING;

                    //BSM.UpdateStateMachine();
                }
                break;

            case (HeroGUI.WAITING):
                break;

            case (HeroGUI.DONE):
                HeroInputDone();
                playerCam.Do_Action();
                BattleCamManager.instance_BCam.Desactivate_TacticalCam();
                BUIM.ClearSelectList();

                //BSM.UpdateStateMachine();
                break;
        }
    }

    public void Instantiate_HeroDCombatBTN(GameObject hero)
    {
        SetUpSpellIcon();
        UI_BTN.SetActive(true);
        hero.transform.transform.GetChild(1).gameObject.SetActive(true);
    }

    // for using spell
    public void Hero_Input_Action(BaseAttack choosenAttack)
    {
        HeroStateMachine hero = herosToManage[0].GetComponent<HeroStateMachine>();

        if (hero.CanUseSpell(choosenAttack))
        {
            heroChoice.attacker = herosToManage[0].name;
            heroChoice.attackerGameObject = herosToManage[0];
            heroChoice.type = "Hero";
            heroChoice.choosenAttack = choosenAttack;
            //Debug.Log(heroChoice.choosenAttack.Formula);
            DesactivateSpellBtn();

            if (choosenAttack.Enumsetting.targetType == TargetType.ally)
            {
                BUIM.CreateTargetButtons(choosenAttack.Enumsetting.targetType);
            }
            if (choosenAttack.Enumsetting.targetType == TargetType.self)
            {
                heroChoice.attackerTarget = herosToManage[0];
                heroInput = HeroGUI.DONE;
                PlayerUI_State();
            }
            if (choosenAttack.Enumsetting.targetType == TargetType.enemy)
            {
                BUIM.CreateTargetButtons(choosenAttack.Enumsetting.targetType);
            }
            if (choosenAttack.Enumsetting.targetType == TargetType.random)
            {
                int r = Random.Range(0, BSM.enemyInBattle.Count);
                heroChoice.attackerTarget = BSM.enemyInBattle[r];
            }
        }
        else
        {
            Debug.Log("not enough ressources to use spell ||||| trigger event cant use spell");
        }
    }

    public void DesactivateSpellBtn()
    {
        StartCoroutine(playAnim());
    }

    // playing button click animation in the spell_Btn Class
    IEnumerator playAnim()
    {
        // desactivating player control to choose a target
        herosToManage[0].transform.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.20f);
        if(current_ui_btn != null)
        {
        current_ui_btn.SetActive(false);
        }
    }

    public void Hero_Input_ChooseTarget(GameObject choosenEnemy) // enemy selection
    {
        Test_target = choosenEnemy;
        heroChoice.attackerTarget = choosenEnemy;
        // changing state machine to send all info to BSM
        heroInput = HeroGUI.DONE;
        PlayerUI_State();
        BSM.UpdateStateMachine();
    }

    // avec un event ? ca fonctionne sans ref...
    public void Hero_Input_Cancel()
    {
        Debug.Log("cancel btn, should activate only when player can select target");
        clear_input_choice();
        Battle_GUI_Manager.instance_BUIM.ClearSelectList();
        //
        // Move the Camera
        BattleCamManager.instance_BCam.ResetPlayerCamera();
        BattleCamManager.instance_BCam.Desactivate_TacticalCam();
        playerCam.Desactivate_cam();
        playerCam.Setup_PlayerCam();

        // clear movement Btn
        CancelInput.TriggerEvent();

        // show spell btn again
        current_ui_btn.SetActive(true);
        herosToManage[0].transform.transform.GetChild(1).gameObject.SetActive(true);
    }

    void clear_input_choice()
    {
        heroChoice.attacker = null;
        heroChoice.attackerGameObject = null;
        heroChoice.type = null;
        heroChoice.choosenAttack = null;
    }

    void HeroInputDone()
    {
        // Debug.Log("11111111111111111111111111111111111");
        BSM.performList.Add(heroChoice);

        //CALL EVENT
        //UpdateBSM.TriggerEvent();
        //ClearAttackPanel();

        ClearHeroToManage();

        // clear ui
        if (current_ui_btn != null)
        {
            Destroy(current_ui_btn);
        }
    }

    public void ClearHeroToManage()
    {
        herosToManage[0].transform.transform.GetChild(0).gameObject.SetActive(false);
        herosToManage[0].transform.transform.GetChild(1).gameObject.SetActive(false);
        HeroSpells.RemoveAt(0);
        herosToManage.RemoveAt(0);

        heroInput = HeroGUI.ACTIVATE;
    }


    /* method player input a ajouter 
    on prend le perso dans la list
    on spawn les btn de de spell
    on met les icon des spell sur les btn    
    ============= create atck BTN lol ez===
    ============= clear atk BTN     
     */
    public void SetUpSpellIcon()
    {
        // set up all ui btn of spell for the character in list
        GameObject spellbtn = Instantiate(UI_BTN, transform);
        spellbtn.transform.parent = UI_BTN_Parent.transform;
        spellbtn.GetComponent<Player_UI_ActionBTN>().SpellList = HeroSpells[0];

        // on decremente les cooldown des spells
        herosToManage[0].GetComponent<HeroStateMachine>().RefreshCooldown(HeroSpells[0]);

        // on set up speel icon
        spellbtn.GetComponent<Player_UI_ActionBTN>().SetUpSpellIcon(herosToManage[0].GetComponent<HeroStateMachine>().hero);
        spellbtn.SetActive(true);
        current_ui_btn = spellbtn;

        // set up new UI for player control
        //Debug.Log(herosToManage[0].GetComponent<HeroStateMachine>()._Action_Manager);
        herosToManage[0].GetComponent<HeroStateMachine>()._Action_Manager.player_UI = current_ui_btn.GetComponent<Player_UI_ActionBTN>();

    }


    #endregion
}

