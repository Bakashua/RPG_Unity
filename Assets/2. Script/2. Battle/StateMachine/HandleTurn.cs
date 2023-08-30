using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HandleTurn 
{

    public string attacker;
    public string type;

    public GameObject attackerGameObject;
    public GameObject attackerTarget;

    //which attack is performed
    //public SkillComponent choosenAttack;
    public BaseAttack choosenAttack;

    public void test()
    {
        Debug.Log("test handle turn");
    }

    public void Clear()
    {
        attacker = null;
        type = null;
        attackerGameObject = null;
        attackerTarget = null;
        choosenAttack = null;
    }

}

