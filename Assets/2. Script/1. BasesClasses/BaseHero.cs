using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class BaseHero : BaseClass
{
    [Header("GENERAL SETTING")]    
    public float requiredXP;
    public float currentXP;
    public int currentLV;    
    public float statsPoints;
    public float skillsPoints;

    //[Header("Leveling Data")]
    //public float baseRequiredValue;
    //public float extraStaticValue;
    //public float accelerationA;
    //public float accelerationB;

    //public string Nickname;
    public string profile;

    [Header("GENERAL SETTING")]
    public int favoritePosition;

    [Header("GRAPHIC")]
    // graphics
    // model,
    // menu image,
    // battler
    private string deleteme;

    void awake()
    {
        //create a random favorite position for the hero
       int i = Random.Range(1, 10);
        favoritePosition = i;
    }

    public void ReceiveXp(float xpGain)
    {
        currentXP = currentXP + xpGain;

        Debug.Log("click to see xp script hero");
        //Debug.Log("receive xp");

        if (currentXP >= requiredXP)
        {
            LevelUp();
            currentXP = 0;

           // Debug.Log("current level is " + currentLV);
        }
    }
    public void LevelUp ()
    {
        //if (currentXP >= requiredXP)
        currentLV += 1;
        //Debug.Log("current level is " + currentLV);
        
        requiredXP = (requiredXP + requiredXP) * (1 + currentLV); 


        // Add formula for each stats level up
        baseHP += 1;
        baseMP += +1;
        baseAtk += +1;
        baseDef += +1;
        baseMatk += +1;
        baseMdef += +1;
        baseAcc += +1;
        baseEva += +1;
        baseSpeed += +1;
        baseLuck += +1;
        baseCritRate += +1;
        baseCritMult += +1;

        statsPoints += +10;
        skillsPoints += +1;
    }
    

}

