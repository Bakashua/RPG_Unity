using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// get float for xp gain
// increment xpgain with every monster xp
// get bonus multipler to all xp
// add xp to all hero
// !!! on peut prendre les objectifs de combat pour donner des bonus d xp !!!

public class XpManager : MonoBehaviour
{
    [HideInInspector] public static XpManager instance_XPM;
    private BattleStateMachine BSM;
    public float xpGain;


    private void Awake()
    {
        if (instance_XPM != null && instance_XPM != this)
        { Destroy(this); }
        else
        { instance_XPM = this; }
        //Debug.Log(instance_XPM);
    }

    void Start()
    {
        BSM = BattleStateMachine.instance_BSM;
    }


    // ici on incremente l xp qu on donne au hero
    public void XpReceived(float xp)
    {
        //Debug.Log("compute on death monster xp");
        xpGain = xpGain + xp;
        //Debug.Log("xp from battle = " + xp);
        Debug.Log("xp add = " + xpGain);
    }


    // ici on donne l xp a la fin du combat
    //public void XpEndBattle(float xpFinal)
    public void XpEndBattle()
    {
        foreach (GameObject heroes in BSM.herosInBattle)
        {
            // multiplier = get all bonus for xp ibjectives
            //add the multiplier to xp final
            //heroes.GetComponent<HeroStateMachine>().hero.ReceiveXp(xpFinal);
            //xpGain = 0;
            heroes.GetComponent<HeroStateMachine>().hero.general_Setting.CharaHero.leveling.ReceiveXp(xpGain);
            //Debug.Log("xp from battle = " + xpGain);
        }
    }


}