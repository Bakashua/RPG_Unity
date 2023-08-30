using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Spells
{
    public BaseAttack Spell;
    public int Weight;
    public bool Always; 
    public enum Condition  { No, Turn, HP, MP, PartyLevel,};
    public Condition condition;
    [Header("______________________________")]
    public int Turn_Range1;
    public int Turn_Range2;
    [Header("______________________________")]
    public float HP_Range1;
    public float HP_Range2;
    [Header("______________________________")]
    public float MP_Range1;
    public float MP_Range2;
    [Header("______________________________")]
    public int PartyLevel;
    // state
    // switch
}

public class IA_Brain : MonoBehaviour
{
    #region Data

    public List<IA_BrainSO> Brains_SO = new();
    public IA_BrainSO CurrBrain;
    public List<Spells> Actions = new();
    public HandleTurn myAttack = new HandleTurn();
    BattleStateMachine BSM;

    #endregion
    private void Start()
    {
        BSM = BattleStateMachine.instance_BSM;

        CurrBrain = GetComponent<EnemyStateMachine>().enemy.IA.brain;
        //CurrBrain = Brains_SO[0];
    }

    public HandleTurn Do_Action(Chara_BaseStats enemy)
    {
        myAttack.attacker = enemy.general_Setting.CharaName;
        myAttack.type = "Enemy";
        myAttack.attackerGameObject = gameObject;

        // here we choose the attack the ennemy will use 
        myAttack.choosenAttack = PickRandomSpell();

        // here we choose the target of attack
        myAttack.attackerTarget = CurrBrain.PickTarget(BSM.herosInBattle, gameObject);

        return myAttack;
    }

    // choose spell
    BaseAttack PickRandomSpell()
    {
        int totalWeight = 0;

        foreach (var item in Actions)
        {
            totalWeight += item.Weight;
        }

        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        foreach (var item in Actions)
        {
            cumulativeWeight += item.Weight;
            if (randomWeight < cumulativeWeight)
            {
                return item.Spell;
            }
        }

        return null;
    }

    // choose target

    //// we use handle turn to store all date we need on this character action
    //HandleTurn myAttack = new HandleTurn();
    //myAttack.attacker = enemy.CharaName;
    //    myAttack.type = "Enemy";
    //    myAttack.attackerGameObject = gameObject;
    //    myAttack.attackerTarget = BSM.herosInBattle[Random.Range(0, BSM.herosInBattle.Count)]; // ICI coef d'aggro des joueurs


    //    // here we choose the attack the ennemy will use //its here we should imput IA logic?
    //    int num = Random.Range(0, enemy.attacks.Count);
    //myAttack.choosenAttack = enemy.attacks[num];
    //    BSM.CollectAction(myAttack);

}
