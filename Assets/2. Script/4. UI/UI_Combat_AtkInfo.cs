using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Combat_AtkInfo : MonoBehaviour
{
    BaseAttack atk;
    HandleTurn turn;
    float damage;
    public TextMeshProUGUI SH;
    // need enemy iverview

    public void GetInformation()
    {
        turn = BattleStateMachine.instance_BSM.performList[0];
        //turn.
        atk = turn.choosenAttack;
        Chara_BaseStats hero = turn.attackerGameObject.GetComponent<HeroStateMachine>().hero;
        Chara_BaseStats en = turn.attackerTarget.GetComponent<EnemyStateMachine>().enemy;

        turn.choosenAttack.DoDamage(hero, en);
        damage = turn.choosenAttack.BaseAttackdata.attackDamage;
    }

    void PrintInfo()
    {

    }
}
