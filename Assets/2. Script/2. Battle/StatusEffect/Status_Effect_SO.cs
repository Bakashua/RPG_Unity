using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public enum StatusState
{ Onhit, OnAttack, TickEffect, Onaction, Onendturn, OnLeaveEffect, OneTime }

[CreateAssetMenu(fileName = "StatusEffect", menuName = "My Game/Status_Weakness/New StatusEffect")]
[System.Serializable]
public class Status_Effect_SO : ScriptableObject
{
    [Header("CLASS")]
    [HideInInspector] public Chara_BaseStats caster;
    [HideInInspector] public Chara_BaseStats target;

    [Header("General setting")]
    public string statusName;
    public enum ComboElem
    { no, chill, wet, warm, oil, poison, frozen, vapo, shock, burn, explosion, cryst, life, melt }
    public ComboElem comboElem;
    public string Combo;
    public Sprite icon;
    public int statusID;
    public int priority;

    [Space(20)]
    [Header("Combat Data")]
    public UnityEvent effect;
    public bool HasTrait;
    public List<Trait> trait = new();
    public StatusState statusState;

    public float damage;
    public float numberOfStacks;
    public int numberOfTurn = 3;
    public int numberTurnLeft = 3;
    public bool isUIon = false;
    public bool isBeneficial;
    public bool canStack;
    public bool canIncrementTurn;
    public bool isRemovable;


    [Header("Effect")]
    public GameObject VFX_Text;

    public void ApplyEffect()
    {
        {
            //bool condition = true;
            numberTurnLeft--;
            if (numberTurnLeft <= 0)
            {
                isUIon = false;

            }

            switch (statusState)

            {
                #region State Of The Effect

                //
                case (StatusState.Onhit):
                    //if (condition == true)
                    //{
                    EntryEffect();
                    //condition = false;
                    //}
                    //else { condition = true; Debug.Log("condition = " + condition); }
                    break;

                //
                case (StatusState.TickEffect):
                    //if (condition == true)
                    //{
                    TickEffect();
                    //condition = false;
                    //}
                    //else { condition = true; Debug.Log("condition = " + condition); }
                    break;
                //
                case (StatusState.Onaction):
                    //if (condition == true)
                    //{
                    TickEffect();
                    // condition = false;
                    //}
                    //else { condition = true; Debug.Log("condition = " + condition); }
                    break;
                //
                case (StatusState.Onendturn):
                    //if (condition == true)
                    //{
                    PersistantEffect();
                    OnEndTurn();
                    //condition = false;
                    //}
                    //else { condition = true; Debug.Log("condition = " + condition); }
                    break;
                //
                case (StatusState.OnLeaveEffect):
                    //if (condition == true)
                    //{
                    PersistantEffect();
                    OnEndTurn();
                    //condition = false;
                    //}
                    //else { condition = true; Debug.Log("condition = " + condition); }
                    break;

                    #endregion
            }
        }
    }

    public void EntryEffect()
    {
        effect.Invoke();
        //Debug.Log("effect is of type : " + statusState);
        //Debug.Log("EntryEffect");
    }
    public void TickEffect()
    {
        effect.Invoke();
        //Debug.Log("effect is of type : " + statusState);
        //Debug.Log("TickEffect");
    }
    public void PersistantEffect()
    {
        effect.Invoke();
        //Debug.Log("effect is of type : " + statusState);
        //Debug.Log("PersistantEffect");
    }
    public void OnEndTurn()
    {
        effect.Invoke();
        //Debug.Log("effect is of type : " + statusState);
        //Debug.Log("OnEndTurn");
    }

    // bool conparaison surcharge d operateur
    //public static bool operator ==(Status_Effect_SO self, Status_Effect_SO other) => self.statusID == other.statusID;
    //public static bool operator !=(Status_Effect_SO self, Status_Effect_SO other) => self.statusID 
    //    != other.statusID;



}