using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IA_Brain", menuName = "My Game/IA/New Brain")]
public class IA_BrainSO : ScriptableObject
{
    // check list of enemy
    // pick the lowest highest
    // of stat a
    // of stat b
    //-----------------------------------
    // focus on healing itself
    // focus on defense
    // focus on buff debuff

    public enum Stats
    { RANDOM, RANGE, CurrHP, HP, ATK, MATK, DEF, MDEF, SH, BLD, }
    public Stats Stat_Compared;
    public bool Highest;
    GameObject target;
    GameObject caster;


    // check les vulnaribilite !!!!!!!!!!!
    // prend une stats et il fait lowest ou highest c est tout

    //public GameObject PickTarget(List<Chara_BaseStats> chara)
    public GameObject PickTarget(List<GameObject> Charas, GameObject En)
    {
        caster = En;
        switch (Stat_Compared)
        {

            case (Stats.RANDOM):
                ReturnRandom(Charas);
                break;

            case (Stats.RANGE):
                ReturnRange(Charas);
                break;

            case (Stats.HP):
                ReturnHP(Charas);
                break;

            case (Stats.CurrHP):
                ReturnCHp(Charas);
                break;

            case (Stats.ATK):
                ReturnAtk(Charas);
                break;

            case (Stats.MATK):
                ReturnMAtk(Charas);
                break;

            case (Stats.DEF):
                ReturnDef(Charas);
                break;

            case (Stats.MDEF):
                ReturnMDef(Charas);
                break;

            case (Stats.SH):
                ReturnSH(Charas);
                break;

            case (Stats.BLD):
                ReturnBld(Charas);
                break;

        }
        return target;
    }

    #region Brain Method
    void ReturnRandom(List<GameObject> Charas)
    {
        target = BattleStateMachine.instance_BSM.herosInBattle[Random.Range(0, BattleStateMachine.instance_BSM.herosInBattle.Count)];
    }

    void ReturnRange(List<GameObject> Charas)
    {
        GameObject EnReturn = null;
        if (Highest)
        {
            float minimumValue = 0;
            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                float dist = Vector3.Distance(caster.transform.position, item.transform.position);
                if (dist >= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = dist;
                }
            }
        }
        if (!Highest)
        {
            float minimumValue = float.MaxValue;
            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                float dist = Vector3.Distance(caster.transform.position, item.transform.position);
                if (dist <= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = dist;
                }
            }
        }
        target = EnReturn;
    }

    void ReturnHP(List<GameObject> Charas)
    {
        GameObject EnReturn = null;
        if (Highest)
        {
            float minimumValue = 0;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.life_Stats.baseHP >= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        if (!Highest)
        {
            float minimumValue = float.MaxValue;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.life_Stats.baseHP <= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }

        target = EnReturn;
    }

    GameObject ReturnCHp(List<GameObject> Charas)
    {
        GameObject EnReturn = null;
        if (Highest)
        {
            float minimumValue = 0;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.life_Stats.currentHP >= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        if (!Highest)
        {
            float minimumValue = float.MaxValue;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.life_Stats.currentHP <= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }

        target = EnReturn;
        return EnReturn;
    }

    void ReturnAtk(List<GameObject> Charas)
    {
        GameObject EnReturn = null;
        if (Highest)
        {
            float minimumValue = 0;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.Battle_Stats.currentAtk >= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        if (!Highest)
        {
            float minimumValue = float.MaxValue;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.Battle_Stats.currentAtk <= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        target = EnReturn;
    }

    void ReturnMAtk(List<GameObject> Charas)
    {
        GameObject EnReturn = null;
        if (Highest)
        {
            float minimumValue = 0;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.Battle_Stats.currentMatk >= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        if (!Highest)
        {
            float minimumValue = float.MaxValue;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.Battle_Stats.currentMatk <= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        target = EnReturn;
    }

    void ReturnDef(List<GameObject> Charas)
    {
        GameObject EnReturn = null;
        if (Highest)
        {
            float minimumValue = 0;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.Battle_Stats.currentDef >= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        if (!Highest)
        {
            float minimumValue = float.MaxValue;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.Battle_Stats.currentDef <= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }

        target = EnReturn;
    }

    void ReturnMDef(List<GameObject> Charas)
    {
        GameObject EnReturn = null;
        if (Highest)
        {
            float minimumValue = 0;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.Battle_Stats.currentMdef >= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        if (!Highest)
        {
            float minimumValue = float.MaxValue;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.Battle_Stats.currentMdef <= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        target = EnReturn;
    }

    void ReturnSH(List<GameObject> Charas)
    {
        GameObject EnReturn = null;
        if (Highest)
        {
            float minimumValue = 0;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.life_Stats.currentShield >= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        if (!Highest)
        {
            float minimumValue = float.MaxValue;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.life_Stats.currentShield <= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        target = EnReturn;
    }

    void ReturnBld(List<GameObject> Charas)
    {
        GameObject EnReturn = null;
        if (Highest)
        {
            float minimumValue = 0;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.life_Stats.currentBlnd >= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        if (!Highest)
        {
            float minimumValue = float.MaxValue;

            foreach (var item in Charas)
            {
                var target = item.GetComponent<HeroStateMachine>();
                if (target.hero.life_Stats.currentBlnd <= minimumValue)
                {
                    EnReturn = item.gameObject;
                    minimumValue = target.hero.life_Stats.currentHP;
                }
            }
        }
        target = EnReturn;

    }
    #endregion


    // on prend list, on prend une stat a comparer, on ajoute les item selon un stat d aggro dans une liste, on tire ensuite un element au hasard de cette liste selon leur weight d aggro

}
