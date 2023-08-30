using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
  USING IT TO SET UP WHAT ACTION THE PLAYER DO 
  */


public class Player_Action_Manager : MonoBehaviour
{
    public Player_UI_ActionBTN player_UI;
    public Chara_SpellList spell_List;
    [SerializeField] Player_Control_Combat PCC;
    UI_PlayerTurn PTM;
    //BattleStateMachine BSM;
    Combat_Movement CM;
    Combat_Special_Action CSA;


    void Start()
    {
        PTM = UI_PlayerTurn.instance_PTM;
        CM = GetComponentInParent<Combat_Movement>();
        CSA = GetComponentInParent<Combat_Special_Action>();
        PCC = GetComponent<Player_Control_Combat>();
        //player_UI = FindObjectOfType<Player_UI_ActionBTN>();
    }

    public void SetUp(Chara_SpellList heroSpeel)
    {
        spell_List = heroSpeel;
    }


    #region Main BTN
    public void Do_Attack()
    {
        PTM.Hero_Input_Action(spell_List.attack);
        player_UI.ListNormal[3].IsActivated();
    }

    public void Do_Defense()
    {
        //Debug.Log("doing chara def" + spell_List.defense);
        PTM.Hero_Input_Action(spell_List.defense);
        player_UI.ListNormal[1].IsActivated();
    }
    public void Do_Move()
    {
        //Debug.Log("doing chara move " + spell_List.move);
        CM.PlayerMovement();
        player_UI.ListNormal[2].IsActivated();
    }
    public void Do_Item()
    {
        //Debug.Log("doing chara Do_Item" + spell_List.item);
        player_UI.ListNormal[0].IsActivated();
    }

    #endregion

    #region Spe_01

    public void DO_North01()
    {
        //Debug.Log("doing chara DO_North01" + spell_List.Set01[0].name);
        PTM.Hero_Input_Action(spell_List.Set01[0]);
        player_UI.ListSpe01[0].IsActivated();
    }

    public void DO_West01()
    {
        //Debug.Log("doing chara DO_West01" + spell_List.Set01[1].name);
        PTM.Hero_Input_Action(spell_List.Set01[1]);
        player_UI.ListSpe01[1].IsActivated();
    }
    public void DO_East01()
    {
        //Debug.Log("doing chara DO_East01" + spell_List.Set01[2].name);
        PTM.Hero_Input_Action(spell_List.Set01[2]);
        player_UI.ListSpe01[2].IsActivated();
    }
    public void DO_South01()
    {
        //Debug.Log("doing chara DO_South01" + spell_List.Set01[3].name);
        PTM.Hero_Input_Action(spell_List.Set01[3]);
        player_UI.ListSpe01[3].IsActivated();
    }

    #endregion

    #region Spe_02
    public void DO_North02()
    {
        //Debug.Log("doing chara DO_North02" + spell_List.Set02[0]);
        PTM.Hero_Input_Action(spell_List.Set02[0]);
    }

    public void DO_West02()
    {
        //Debug.Log("doing chara DO_West02" + spell_List.Set02[1]);
        PTM.Hero_Input_Action(spell_List.Set02[1]);
    }
    public void DO_East02()
    {
        //Debug.Log("doing chara DO_East02" + spell_List.Set02[2]);
        PTM.Hero_Input_Action(spell_List.Set02[2]);
    }
    public void DO_South02()
    {
        //Debug.Log("doing chara DO_South02" + spell_List.Set02[3]);
        PTM.Hero_Input_Action(spell_List.Set02[3]);
    }
    #endregion

    #region Special Trigger

    public void DO_Spe_L()
    {
        //Debug.Log("doing chara Spe_L");// + spell_List.item);
        player_UI.ShowSpe1();
        BattleCamManager.instance_BCam.PlayerChooseSpell(0f);

    }
    public void DO_Spe_R()
    {
        //Debug.Log("doing chara Spe_R");// + spell_List.item);
        BattleCamManager.instance_BCam.PlayerChooseSpell(-12f);
        player_UI.ShowSpe2();
    }
    public void DO_Spe_Cancel()
    {
        //Debug.Log("doing chara Spe_Cancel"); // + spell_List.item);
        BattleCamManager.instance_BCam.PlayerChooseSpell(-4f);
        player_UI.ShowSpeCancel();
    }

    #endregion

    #region Bump BTN

    public void Switch_Change_Character()
    {
        Debug.Log("doing chara Switch_Change_Character" + spell_List.changechara);
    }

    public void Switch_GiveTurn()
    {
        Debug.Log("doing chara Switch_GiveTurn" + spell_List.giveturn);
    }

    public void Do_Ultimate()
    {
        //Debug.Log("doing chara Ultimate" + spell_List.ultimate);
        CSA.Do_Ultimate(spell_List);
        //PTM.Hero_Input_Action(spell_List.ultimate);
    }

    public void Do_LimitBreak()
    {
        //Debug.Log("doing chara LimitBreak" + spell_List.limitBreak);
        CSA.Do_LimitBreak(spell_List);
        //PTM.Hero_Input_Action(spell_List.limitBreak);
    }

    #endregion

    #region Menu BTN
    public void Pause_Menu()
    {
        Debug.Log("doing chara Pause_Menu" + spell_List.item);
    }


    public void Scan_Enemy()
    {
        Debug.Log("doing chara Ultimate" + spell_List.item);
    }

    #endregion

}
