using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


/*
  USING IT ONLY TO RECEIVE CONTROL INPUT 
script finit on y touche plus

les actions sont set up dans la class player_action_manager
  */

public class Player_Control_Combat : MonoBehaviour
{
    private Input_ActionMap_RPG playerControls;
    //Player_Action_Manager action;
    bool spe1;
    bool spe2;
    #region Event Action
    public List<UnityEvent> playerBaseAction = new List<UnityEvent>();
    public List<UnityEvent> playerSpe01 = new List<UnityEvent>();
    public List<UnityEvent> playerSpe02 = new List<UnityEvent>();
    public List<UnityEvent> playerSpeTrigger = new List<UnityEvent>();
    public List<UnityEvent> playerBump = new List<UnityEvent>();
    public List<GameEvent> playerArrow = new List<GameEvent>();
    public List<UnityEvent> menuBtn = new List<UnityEvent>();

    #endregion


    private void Awake()  { playerControls = new Input_ActionMap_RPG(); }
    
    private void OnEnable()  
    {    
        playerControls.Enable();
        //
        playerControls.UI_Combat.NorthBTN.performed += North_BTN;
        playerControls.UI_Combat.WestBTN.performed += West_BTN;
        playerControls.UI_Combat.EastBTN.performed += EAST_BTN;
        playerControls.UI_Combat.SouthBTN.performed += South_BTN;
        //
        playerControls.UI_Combat.Spe_L.performed += Spe_L;
        playerControls.UI_Combat.Spe_L.canceled += Spe_Cancel;
        playerControls.UI_Combat.Spe_R.performed += Spe_R;
        playerControls.UI_Combat.Spe_R.canceled += Spe_Cancel;
        //
        playerControls.UI_Combat.Switch_Change_Character.performed += Switch_Change_Character;
        playerControls.UI_Combat.Switch_GiveTurn.performed += Switch_GiveTurn;
        playerControls.UI_Combat.Ultimate.performed += Ultimate;
        playerControls.UI_Combat.LimitBreak.performed += LimitBreak;

        //
        playerControls.UI_Combat.Pause_Menu.performed += Pause_Menu;
        playerControls.UI_Combat.Scan_Enemy.performed += Scan_Enemy;

        //
        playerControls.UI_Combat.UpArrow.performed += UpArrow;
        playerControls.UI_Combat.DownArrow.performed += DownArrow;
        playerControls.UI_Combat.LeftArrow.performed += LeftArrow;
        playerControls.UI_Combat.RightArrow.performed += RightArrow;
    }

    private void OnDisable() 
    { 
        playerControls.Disable();
        //
        playerControls.UI_Combat.NorthBTN.performed -= North_BTN;
        playerControls.UI_Combat.WestBTN.performed -= West_BTN;
        playerControls.UI_Combat.EastBTN.performed -= EAST_BTN;
        playerControls.UI_Combat.SouthBTN.performed -= South_BTN;
        //
        playerControls.UI_Combat.Spe_L.performed -= Spe_L;
        playerControls.UI_Combat.Spe_R.performed -= Spe_R;
        //
        playerControls.UI_Combat.Switch_Change_Character.performed -= Switch_Change_Character;
        playerControls.UI_Combat.Switch_GiveTurn.performed -= Switch_GiveTurn;
        playerControls.UI_Combat.Ultimate.performed -= Ultimate;
        playerControls.UI_Combat.LimitBreak.performed -= LimitBreak;
        //
        playerControls.UI_Combat.Pause_Menu.performed -= Pause_Menu;
        playerControls.UI_Combat.Scan_Enemy.performed -= Scan_Enemy;

        //
        playerControls.UI_Combat.UpArrow.performed -= UpArrow;
        playerControls.UI_Combat.DownArrow.performed -= DownArrow;
        playerControls.UI_Combat.LeftArrow.performed -= LeftArrow;
        playerControls.UI_Combat.RightArrow.performed -= RightArrow;
    }

    #region main btn

    private void North_BTN (InputAction.CallbackContext context)
    {
        if (spe1)
        {
            playerSpe01[0].Invoke();
        }
        else if (spe2)
        {
            playerSpe02[0].Invoke();
        }
        else
        {
            playerBaseAction[0].Invoke();
        }
    }
    private void West_BTN (InputAction.CallbackContext context)
    {
        if (spe1)
        {
            playerSpe01[1].Invoke();
        }
        else if (spe2)
        {
            playerSpe02[1].Invoke();
        }
        else
        {
            playerBaseAction[1].Invoke();
        }
    }
    private void EAST_BTN (InputAction.CallbackContext context)
    {
        if (spe1)
        {
            playerSpe01[2].Invoke();
        }
        else if (spe2)
        {
            playerSpe02[2].Invoke();
        }
        else
        {
            playerBaseAction[2].Invoke();
        }
    }
    private void South_BTN (InputAction.CallbackContext context)
    {
        if (spe1)
        {
            playerSpe01[3].Invoke();
        }
        else if (spe2)
        {
            playerSpe02[3].Invoke();
        }
        else
        {
            playerBaseAction[3].Invoke();
        }
    }
    #endregion

    #region Special Trigger
    private void Spe_L (InputAction.CallbackContext context)
    {
        playerSpeTrigger[0].Invoke();
        spe1 = true;
    }
    private void Spe_R (InputAction.CallbackContext context)
    {
        playerSpeTrigger[1].Invoke();
        spe2 = true;
    }   
    private void Spe_Cancel (InputAction.CallbackContext context)
    {
        playerSpeTrigger[2].Invoke();
        spe1 = false;
        spe2 = false;
    }

    #endregion

    #region Bump Btn

    private void Switch_Change_Character (InputAction.CallbackContext context)
    {
        playerBump[0].Invoke();
    }
    
    private void Switch_GiveTurn (InputAction.CallbackContext context)
    {
        playerBump[1].Invoke();
    }

    private void Ultimate (InputAction.CallbackContext context)
    {
        playerBump[2].Invoke();
    }
    private void LimitBreak (InputAction.CallbackContext context)
    {
        playerBump[3].Invoke();
    }


    #endregion

    #region ArrowBtn
    private void UpArrow(InputAction.CallbackContext context)
    {
        playerArrow[0].TriggerEvent();
    }

    private void DownArrow(InputAction.CallbackContext context)
    {
        playerArrow[1].TriggerEvent();
    }

    private void LeftArrow(InputAction.CallbackContext context)
    {
        playerArrow[2].TriggerEvent();
    }

    private void RightArrow(InputAction.CallbackContext context)
    {
        playerArrow[3].TriggerEvent();
    }

    #endregion

    #region Menu Btn

    private void Pause_Menu (InputAction.CallbackContext context)
    {
        menuBtn[2].Invoke();
    }

    private void Scan_Enemy (InputAction.CallbackContext context)
    {
        menuBtn[2].Invoke();
    }

    #endregion



    private void Update()
    {
        //float buttonEast = playerControls.UI_Combat.EastBTN.ReadValue<float>();

        //if(playerControls.UI_Combat.EastBTN.triggered)
        //{
        //Debug.Log("pressing B");
        //}
    }
}
