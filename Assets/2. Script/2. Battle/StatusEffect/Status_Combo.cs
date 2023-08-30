using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Status_Combo 
{
    // receive status
    // check their combo
    // apply combo 

    public void PlayCombo(float f)
    {
        if (f == 0)
        {
            Debug.Log("combo null");
        }
        if (f == 1)
        {
            Debug.Log("combo frozen");
        }
        if (f == 2)
        {
            Debug.Log("combo vaporisation");
        }
        if (f == 3)
        {
            Debug.Log("combo burn");
        }
        if (f == 4)
        {
            Debug.Log("combo life");
        }
        if (f == 5)
        {
            Debug.Log("combo explosion");
        }
        if (f == 6)
        {
            Debug.Log("combo crystalisation");
        }
        if (f == 7)
        {
            Debug.Log("combo melt");
        }
        if (f == 8)
        {
            Debug.Log("combo stun");
        }
        if (f == 9)
        {
            Debug.Log("combo shock");
        }
        if (f == 10)
        {
            Debug.Log("combo nothing yet");
        }
        if (f == 11)
        {
            Debug.Log("combo nothing yet");
        }
    }

    void Status01()
    {
        Debug.Log("00000");
    }

    void Status02()
    {
        Debug.Log("11111");
    }

    void Status03()
    {
        Debug.Log("22222");
    }

    void Status04()
    {
        Debug.Log("33333");
    }
}
