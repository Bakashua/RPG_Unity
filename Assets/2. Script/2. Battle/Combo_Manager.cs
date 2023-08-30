using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo_Manager: MonoBehaviour
{
    public bool isComboOn = false;

    public float currentCombo = 0;
    public float comboBonusRate = 1;
    public float comboIncrementation = 0.2f;
    public float comboExplosionValue = 1.5f;



    public void OnAttackCombo(BaseAttack attack)
    {
       if(attack.Enumsetting.comboType == ComboType.Starter && isComboOn == false)
        {
            isComboOn = true;
            currentCombo += 1; Debug.Log(currentCombo);
            comboBonusRate += comboBonusRate + comboIncrementation; Debug.Log(comboBonusRate);
        }
       else if(attack.Enumsetting.comboType == ComboType.Follow && isComboOn == true)
        {

        }
       else if(attack.Enumsetting.comboType == ComboType.Finisher && isComboOn == true)
        {

        }
    }
 
    
    // on attack
    // check if combo is on
    // if true increment +0.2 degat and combo +1

    // if attack = burst then make combo explode 

}
