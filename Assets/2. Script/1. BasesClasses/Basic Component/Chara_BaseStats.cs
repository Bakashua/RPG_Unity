using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Chara_BaseStats", menuName = "My Game/Character/ New BaseStats")]
[System.Serializable]
public class General_Setting
{
    public Chara_Hero CharaHero;
    public int favoritePosition = 0;
    [SerializeField] public string id;
    public string ID { get { return id; } }
    public string CharaName;
    public Sprite HeroBust;
    public Sprite CharaBody;
    public Sprite Icon;
}

[System.Serializable]
public class Life_Stats
{
    [Tooltip("Maximum of health")]
    public float baseHP;
    //[HideInInspector] 
    public float currentHP;
    public float baseMP;
    [HideInInspector] public float currentMP;
    [HideInInspector] public float baseTP = 200;
    public float currentTP;
    public float baseShield;
    public float KeepShield;
    public float currentShield;
    public float baseBlnd;
    public float currentBlnd;
    //
    public float postureCurr;
    public float postureMax;
}

[System.Serializable]
public class Battle_Stats
{
    public float baseAtk;
    //[HideInInspector] 
    public float currentAtk;
    public float baseDef;
    //[HideInInspector] 
    public float currentDef;

    public float baseMatk;
    [HideInInspector] public float currentMatk;
    public float baseMdef;
    [HideInInspector] public float currentMdef;

    public float baseAcc;
    [HideInInspector] public float currentAcc;
    public float baseEva;
    [HideInInspector] public float currentEva;

    public float baseSpeed;
    [HideInInspector] public float currentSpeed;
    public float baseLuck;
    [HideInInspector] public float currentLuck;

    public float baseCritRate;
    [HideInInspector] public float currentCritRate;

    public float baseCritEva;
    [HideInInspector] public float currentCritEva;

    public float baseCritMult;
    [HideInInspector] public float currentCritMult = 2f;

    public float baseAggro;
    [HideInInspector] public float currentAggro = 0f;
    //public float baseMove;
    //public float currentMove;

}

[System.Serializable]
public class EnemyIA
{
    [Header("BATTLE REWARD")]
    public DropReward reward;
    public IA_BrainSO brain;
}

[System.Serializable]
public class Defense_Type
{
    public float vuln_Slash = 1;
    public float vuln_Bash = 1;
    public float vuln_Pierce = 1;
    public float vuln_earth = 1;
    public float vuln_wind = 1;
    public float vuln_fire = 1;
    public float vuln_water = 1;
    public float vuln_light = 1;
    public float vuln_darkness = 1;
    public List<Sprite> imageVulnList = new List<Sprite>();
    public Sprite im_slash;
    public Sprite im_bash;
    public Sprite im_pierce;
    public Sprite im_earth;
    public Sprite im_wind;
    public Sprite im_fire;
    public Sprite im_water;
    public Sprite im_light;
    public Sprite im_darkness;

}

[System.Serializable]
public class Status_Effect_Rate
{
    public Status_Effect_Manager SEM;

    public float poisonRate;
    public float burnRate;

    public float sealRate;
    public float muteRate;

    public float blindRate;

    public float sleepRate;
    public float freezeRate;
    public float petrifyRate;

    public float confuseRate;
    public float charmRate;
    public float rageRate;

    //public float statDownRate;
    //public float StatUPrate;
    //public float regenHPRate;
    //public float regenMPRate;
    //public float regenTPRate;
    //public float focusRate;
    //public float insightRate;

}


public class Chara_BaseStats : ScriptableObject
{
    #region Base stats

    [Header("GENERAL SETTING")]
    public General_Setting general_Setting;

    [Header("______________________")]
    [Header("LIFE STATS")]
    public Life_Stats life_Stats;

    [Header("______________________")]
    [Header("BATTLE STATS")]
    public Battle_Stats Battle_Stats;

    #endregion

    // is IA
    [Header("______________________")]
    [Header("IA")]
    public EnemyIA IA;


    #region DEFENSE TYPE STATS

    [Header("______________________")]
    [Header("DEFENSE TYPE")]
    //    { physical, earth, wind, fire, water, light, darkness }
    public Defense_Type defense;

    #endregion

    #region STATUS EFFECT RATE

    [Header("______________________")]
    [Header("STATUS EFFECT RATE")]
    public Status_Effect_Rate Status;

    #endregion



    public enum DefenseType
    { Slash, Bash, Pierce, } // Strike,  }
    public DefenseType PhysicVuln;

    public enum Elements
    { physical, earth, wind, fire, water, light, darkness }
    public Elements elements;


    [Header("______________________")]
    [Space(10)]
    public List<BaseAttack> attacks = new List<BaseAttack>(); //liste des skill ici là !!! apres on les mettra sur un script spécifique,
                                                              //là c'est pour debut ennemy statemachine qui a besoin d'une atk
                                                              // !!! A CHANGER QUAND REWORK IA !!!

    protected virtual void OnValidate()
    {
        //string path = AssetDatabase.GetAssetPath(this);
        //general_Setting.id = AssetDatabase.AssetPathToGUID(path);
    }

    public void SetUP()
    {
            //create a random favorite position for the hero
            if (general_Setting.favoritePosition == 0)
            {
                int i = Random.Range(1, 10);
                general_Setting.favoritePosition = i;
            }
            SetVulnImg();        

        SetStat();
    }

    public void SetStat()
    {
            //Debug.Log("SetStat");
            life_Stats.currentHP = life_Stats.baseHP;
            life_Stats.currentMP = life_Stats.baseMP;
            life_Stats.currentTP = Random.Range(1, 10);
            if (0 <= life_Stats.baseBlnd)
            {
                life_Stats.baseShield = life_Stats.baseBlnd;
                life_Stats.currentShield = life_Stats.baseBlnd;
            }
            else
            {
                life_Stats.currentShield = life_Stats.baseShield;
            }
            life_Stats.currentBlnd = life_Stats.baseBlnd;
            //Debug.Log(currentShield);

        Battle_Stats.currentAtk = Battle_Stats.baseAtk;
        Battle_Stats.currentDef = Battle_Stats.baseDef;
        Battle_Stats.currentMatk = Battle_Stats.baseMatk;
        Battle_Stats.currentMdef = Battle_Stats.baseMdef;
        Battle_Stats.currentAcc = Battle_Stats.baseAcc;
        Battle_Stats.currentLuck = Battle_Stats.baseLuck;
        Battle_Stats.currentSpeed = Battle_Stats.baseSpeed;
        Battle_Stats.currentEva = Battle_Stats.baseEva;
        Battle_Stats.currentCritEva = Battle_Stats.baseCritEva;
        Battle_Stats.currentCritMult = Battle_Stats.baseCritMult;
        Battle_Stats.currentCritRate = Battle_Stats.baseCritRate;
    }

    public void SetUpShield()
    {
        if (life_Stats.currentShield <= life_Stats.KeepShield)
        {
            life_Stats.KeepShield = life_Stats.currentShield;
        }
    }

    public void SetVulnImg()
    {
        //Debug.Log("DATA_________  " + name );
        //Debug.Log("DATA_________________ " + name + "  ________ " + defense.vuln_Bash);
        //Debug.Log("DATA_________________ " + name + "  ________ " + defense.vuln_Pierce);
        //Debug.Log("DATA_________________ " + name + "  ________ " + defense.vuln_earth);
        //Debug.Log("DATA_________________ " + name + "  ________ " + defense.vuln_wind);
        //Debug.Log("DATA_________________ " + name + "  ________ " + defense.vuln_fire);
        //Debug.Log("DATA_________________ " + name + "  ________ " + defense.vuln_water);
        //Debug.Log("DATA_________________ " + name + "  ________ " + defense.vuln_light);
        //Debug.Log("DATA_________________ " + name + "  ________ " + defense.vuln_darkness);

        if (defense.vuln_Slash > 1) { defense.imageVulnList.Add(defense.im_slash); }
        if (defense.vuln_Bash > 1) { defense.imageVulnList.Add(defense.im_bash); }
        if (defense.vuln_Pierce > 1) { defense.imageVulnList.Add(defense.im_pierce); }
        if (defense.vuln_earth > 1) { defense.imageVulnList.Add(defense.im_earth); }
        if (defense.vuln_wind > 1) { defense.imageVulnList.Add(defense.im_wind); }
        if (defense.vuln_fire > 1) { defense.imageVulnList.Add(defense.im_fire); }
        if (defense.vuln_water > 1) { defense.imageVulnList.Add(defense.im_water); }
        if (defense.vuln_light > 1) { defense.imageVulnList.Add(defense.im_light); }
        if (defense.vuln_darkness > 1) { defense.imageVulnList.Add(defense.im_darkness); }

    }
}