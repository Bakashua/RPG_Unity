using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class OpenStartMenu : MonoBehaviour
{
    [SerializeField] GameEvent Open_StartMenu_Event;
    [SerializeField] GameEvent Close_StartMenu_Event;
    private Input_ActionMap_RPG playerControls;
    int i = 0;

    private void OnEnable()
    {
        playerControls.UI_Gen.OpenStartMenu.performed += OpenStartMenuu;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerControls = new Input_ActionMap_RPG();
        playerControls.Enable();

    }


    private void OpenStartMenuu (InputAction.CallbackContext context)
    {
        if (i==0)
        {
            Open_StartMenu_Event.TriggerEvent();
            i++;
        }
        else
        {
            Close_StartMenu_Event.TriggerEvent();
            i = 0;
        }

    }





}
