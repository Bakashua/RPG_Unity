using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class UI_Return_BTN : MonoBehaviour
{
    private ActionMap_RPG playerControls;
    public UnityEvent EventToInvoke_East;
    public UnityEvent EventToInvoke_Nord;

    private void Awake() { playerControls = new ActionMap_RPG(); }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.UI_Combat.NorthBTN.performed += NORTH_BTN;
        //playerControls.UI_Combat.WestBTN.performed += West_BTN;
        playerControls.UI_Combat.EastBTN.performed += EAST_BTN;
        //playerControls.UI_Combat.SouthBTN.performed += South_BTN;
    }
    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.UI_Combat.NorthBTN.performed -= NORTH_BTN;
        //playerControls.UI_Combat.WestBTN.performed -= West_BTN;
        playerControls.UI_Combat.EastBTN.performed -= EAST_BTN;
        //playerControls.UI_Combat.SouthBTN.performed -= South_BTN;
    }

    private void EAST_BTN(InputAction.CallbackContext context)
    {
        EventToInvoke_East.Invoke();
    }
    private void NORTH_BTN(InputAction.CallbackContext context)
    {
        EventToInvoke_Nord.Invoke();
    }
}
