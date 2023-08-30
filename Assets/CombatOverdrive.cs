using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CombatOverdrive : MonoBehaviour
{

    /*
     slider
     player position
     --player action

     check position ()
     gives remove bomus ()
     () +
     () -
     () previews

    ==============

    spawn type d action
    complete action = ()- && spawn action

    =======

        list btn pour power point
    add pp
    remove pp
    use pp
    pp gives bonus

    */

    public Slider Slider_ov;
    public GameObject Icon;
    public GameObject Icon_Preview;
    float currentCooldown = 0;


    private void Start()
    {
        //StartCoroutine(UpdateIconPos(10f));
        //MoveIconPos();
    }

    void MoveIconPos()
    {
        // min 10 ou  son transformP
        // max 570

        float transformP = Icon.transform.localPosition.x;
        Icon.transform.DOLocalMoveX(transformP + 570, 10, false);
    }

}


