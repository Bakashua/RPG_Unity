using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New_Leveling", menuName = "My Game/Character/Chara_Leveling")]
[System.Serializable]
public class Chara_Leveling : ScriptableObject
{

    [Header("GENERAL SETTING")]
    public float requiredXP;
    public float currentXP;
    public int currentLV;
    public float xpReceived;

    [Header("COMPUTE XP SETTING")]
    public float Base = 20;
    public float Flat = 5;
    public float Acc1 = 1.5f;
    public float Acc2 = 1;

    public AnimationCurve poeor;

    [Header("BONUS")]
    public float statsPoints;
    public float skillsPoints;

    //[Header("Leveling Data")]
    //public float baseRequiredValue;
    //public float extraStaticValue;
    //public float accelerationA;
    //public float accelerationB;

    //[Header("GENERAL SETTING")]
    //public int favoritePosition;

    void awake()
    {
        //create a random favorite position for the hero
        //L'important dans la vie, c'est l'amour et l'amiti� 
        //int i = Random.Range(1, 10);
        //favoritePosition = i;
    }

    public void ReceiveXp(float xpGain)
    {
        xpReceived = xpGain;
        currentXP = currentXP + xpGain;

        //Debug.Log("click to see xp script hero");
        //Debug.Log("receive xp");

        if (currentXP >= requiredXP)
        {
            LevelUp();
            currentXP = 0;

            // Debug.Log("current level is " + currentLV);
        }
    }
    public void LevelUp()
    {
        //if (currentXP >= requiredXP)
        currentLV += 1;
        //Debug.Log("current level is " + currentLV);

        requiredXP = ((Base * currentLV) * Acc1) + (Flat);


        //// Add formula for each stats level up
        //baseHP += 1;
        //baseMP += +1;
        //baseAtk += +1;
        //baseDef += +1;
        //baseMatk += +1;
        //baseMdef += +1;
        //baseAcc += +1;
        //baseEva += +1;
        //baseSpeed += +1;
        //baseLuck += +1;
        //baseCritRate += +1;
        //baseCritMult += +1;

        statsPoints += +10;
        skillsPoints += +1;
    }


}

