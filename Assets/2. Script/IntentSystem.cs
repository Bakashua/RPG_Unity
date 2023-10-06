using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntentSystem : MonoBehaviour
{
    public Image image;
    public Image image_Target;

    public  List<Sprite>  Atk = new();    
    public  Sprite  Atk_Buff;
    public  Sprite  Atk_Debuff;
    public  Sprite  Atk_Defend;
    
    public  Sprite  Buff;
    public  Sprite  BuffL;

    public Sprite Debuff;
    public Sprite DebuffL;

    public Sprite Defend;
    public Sprite DefendL;
    public Sprite DefendBuff;
    public Sprite DefendBuffL;

    public Sprite Magic;
    public Sprite MagicL;

    public Sprite Stun;
    public Sprite StunL;

    public Sprite Unknown;
    public Sprite Placeholder;

    HandleTurn handleTurn;

    public void ShowIntent(HandleTurn Action) 
    {
        handleTurn = Action;
        string attackType = Action.choosenAttack.Enumsetting.attackType.ToString();

        switch (attackType)
        {
            case "Atk":
                ShowImage(Atk[2]);
            break;

            case "Atk_Buff":
                ShowImage(Atk_Buff);
                break;

            case "Atk_Debuff":
                ShowImage(Atk_Debuff);

                break;

            case "Atk_Defend":
                ShowImage(Atk_Defend);

                break;

            case "Buff":
                ShowImage(Buff);

                break;

            case "Debuff":
                ShowImage(Debuff);

                break;

            case "Defend":
                ShowImage(Defend);

                break;


            case "DefendBuff":
                ShowImage(DefendBuff);

                break;

            case "Stun":
                ShowImage(Stun);

                break;

            case "Unknown":
                ShowImage(Unknown);

                break;
        }    
    }

    void ShowImage(Sprite sp)
    {        
        image.sprite = sp;
        Color newColor = image.color;
        newColor.a = 100f;
        image.color = newColor;

        if(handleTurn.choosenAttack.Enumsetting.targetType != TargetType.self || handleTurn.choosenAttack.Enumsetting.targetType != TargetType.ally)
        {
        image_Target.sprite = handleTurn.attackerTarget.GetComponent<HeroStateMachine>().hero.general_Setting.HeroBust;
        image_Target.color = newColor;
        }
    }

    public void ClearIntent()
    {
        Color newColor = image.color;
        newColor.a = 0f;
        image.color = newColor;
        image.sprite = null;
        image_Target.color = newColor;
    }


}
