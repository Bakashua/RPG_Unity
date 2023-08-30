using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "BaseStats_SO", menuName = "My Game/Base_Stats/New_Character")]
public class BaseClass// : ScriptableObject

{
    public string theName;

    [Header("LIFE STATS")]
    [Tooltip("Maximum of health")]
    public float baseHP;
    //[HideInInspector] 
    public float currentHP;
    public float baseMP;
    [HideInInspector] public float currentMP;
    public float baseTP;
    [HideInInspector] public float currentTP;


    [Header("BATTLE STATS")]
    public float baseAtk;
    [HideInInspector] public float currentAtk;
    public float baseDef;
    [HideInInspector] public float currentDef;
    
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
    //public float baseMove;
    //public float currentMove;


    [Header("STATUS EFFECT RATE")]

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


    //[Header("ATTACK TYPE")]

    public enum DefenseType
    { Slash, Bash, Pierce, } // Strike,  }
    public DefenseType defenseType;

    public enum Elements
    { physical, earth, wind, fire, water, light, darkness }
    public Elements elements;

    //[Header("DEFENSE TYPE")]


    [Header("LIST OF ATTACKS")]
    public List<BaseAttack> attacks = new List<BaseAttack>(); //liste des skill ici là !!!
    public List<BaseAttack> magics = new List<BaseAttack>();
    public List<BaseAttack> techs = new List<BaseAttack>();  
    
    [Header("LINK ATTACKS")]
    public List<BaseAttack> linkAttack = new List<BaseAttack>(); //liste des skill ici là !!!
    public List<BaseAttack> linkRush = new List<BaseAttack>();
    public List<BaseAttack> linkAllOut = new List<BaseAttack>();


    public void SetStat()
    {
        //Debug.Log("Stats are set");
        //currentHP = baseHP;
        //currentMP = baseMP;
        //currentTP = baseTP;

        currentAtk = baseAtk;
        currentDef = baseDef;
    }


    //public List<SkillComponent> Atk = new List<SkillComponent>();
       
    // hp/mp/tp (current + max), atk, def, matk, mdef, speed, dex (accuracy), agl (evasion),
    // movement, range, luck (crit chance/inflicting status ?)
    // status effect %

    // TRAIT
    // rate :
    // element rate (type elementaire, the high the value the greater the weakness) -> (type * %)
    // status resist -> (type * %)
    // debuff/status rate  using skill or item might add debuff -> (stat debuff * % )


    // AUTOMATISER ON BATTLE START CURRENTSTAT = BASE STAT !!!!!!!
}
