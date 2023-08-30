using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Combat_AutoSelectBTN : MonoBehaviour
{

    public GameObject primaryButton;
    //public bool Override;    //public bool Override;

    public void OnEnable()
    {
        // clear selected object
        //EventSystem.current.SetSelectedGameObject(null);

        // Adding new selected object
        if (!primaryButton)
        {
            //        gameObject.GetComponent<Button>().
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        else
        {
            //primaryButton.Select();  
            EventSystem.current.SetSelectedGameObject(primaryButton);
        }        // Adding new selected object

        //if (Override)
        //{
        //    EventSystem.current.SetSelectedGameObject(primaryButton);
        //}

    }

}
