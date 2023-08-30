using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Combat_NeutralInput : MonoBehaviour
{
    public void OnPress_North()
    {
        Debug.Log("North is pressed");
    }

    public void OnPress_West()
    {
        Debug.Log("West is pressed");
    }

    public void OnPress_East()
    {
        Debug.Log("East is pressed");
    }

    public void OnPress_South()
    {
        Debug.Log("South is pressed");
    }

}
