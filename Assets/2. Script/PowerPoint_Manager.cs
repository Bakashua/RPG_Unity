using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPoint_Manager : MonoBehaviour
{
    [SerializeField] HeatSystem_Manager HM;
    Chara_BaseStats chara;

    public float PowerPointMax;
    public float PowerPointCurr;

    public GameEvent Event_TriggeredPP;

    [SerializeField] SO_AudioEvent GainPP;
    [SerializeField] SO_AudioEvent ActivatePP;
    [SerializeField] SO_AudioEvent DesactivatePP;
    [SerializeField] SO_AudioEvent ApplyPP;
    AudioSource source;

    [Header("UI")]
    [SerializeField] GameObject UI;
    [SerializeField] Color colorOn;
    [SerializeField] Color colorSelected;
    [SerializeField] Color colorOff;
    [SerializeField] List<Image> ppIcon = new();
    [SerializeField] List<Image> ActivatedIcon = new();
    [SerializeField] List<Image> Selectedpp = new();
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private int countPreviousSelection = 0;

    bool hasSelected = false;


    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Listener_SetUP()
    {
        UI.SetActive(true);
    }

    public void GainPowerPoints()
    {
        PowerPointCurr += 1;
        if (PowerPointCurr >= PowerPointMax) { PowerPointCurr = PowerPointMax; }
        UpdateUI();
        HM.UpdateHeatBonus(PowerPointCurr);
    }

    private void UpdateUI()
    {
        if (!hasSelected && PowerPointCurr <= PowerPointMax)
        {
            for (int i = currentIndex; i < PowerPointCurr; i++)
            {
                //if (!ActivatedIcon.Contains(ppIcon[i]))
                //{
                ppIcon[i].color = colorOn;
                ActivatedIcon.Add(ppIcon[i]);
                currentIndex++;
                GainPP.Play(source);
                //}
            }
        }
    }


    public void AddToSelection()
    {
        //currentIndex = ActivatedIcon.Count;
        if (currentIndex > 0)
        {
            Image objToAdd = ActivatedIcon[currentIndex - 1];
            objToAdd.color = colorSelected;
            Selectedpp.Add(objToAdd);
            currentIndex--; PowerPointCurr--;
            ActivatePP.Play(source);
            if (currentIndex < 0) { currentIndex = 0; }
        }
    }

    public void RemoveFromSelection()
    {
        if (Selectedpp.Count > 0)
        {
            int lastIndex = Selectedpp.Count - 1;
            Selectedpp[lastIndex].color = colorOn;
            Selectedpp.RemoveAt(lastIndex);
            //currentIndex = Mathf.Max(0, currentIndex - 1);
            currentIndex++; PowerPointCurr++;
            DesactivatePP.Play(source);
        }
        //if (Selectedpp.Count > 0)
        //{
        //    int firstIndex = 0;
        //    Selectedpp[firstIndex].color = colorOn;
        //    Selectedpp.RemoveAt(firstIndex);
        //    //currentIndex = Mathf.Max(firstIndex + 1, Selectedpp.Count);
        //    currentIndex ++;
        //}
    }

    // used by unity event
    public void UsePP()
    {
        if (Selectedpp.Count >= 1)
        {
            ValidateSelection();
            Event_TriggeredPP.TriggerEvent();
        }
    }

    public void Listener_ENDTURN()
    {
        print("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
    }

    void ValidateSelection()
    {
        //Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb" + "           " + countPreviousSelection);
        chara = BattleStateMachine.instance_BSM.herosToManage[0].GetComponent<HeroStateMachine>().hero;



        if (Selectedpp.Count >= 1)
        {
            hasSelected = true;
        }
        if (Selectedpp.Count >= 5)
        {
            print(chara.Battle_Stats.currentAtk);
            PPStatEffect(chara, 2);
            print(chara.Battle_Stats.currentAtk);
        }
        else if (Selectedpp.Count >= 4)
        {
            print(chara.Battle_Stats.currentAtk);
            PPStatEffect(chara, 1.8f);
            print(chara.Battle_Stats.currentAtk);
        }
        else if (Selectedpp.Count >= 3)
        {
            print(chara.Battle_Stats.currentAtk);
            PPStatEffect(chara, 1.6f);
            print(chara.Battle_Stats.currentAtk);
        }
        else if (Selectedpp.Count >= 2)
        {
            //print("validated 2");
            print(chara.Battle_Stats.currentAtk);
            PPStatEffect(chara, 1.4f);
            print(chara.Battle_Stats.currentAtk);
        }
        else if (Selectedpp.Count >= 1)
        {
            print(chara.Battle_Stats.currentAtk);
            PPStatEffect(chara, 1.2f);
            print(chara.Battle_Stats.currentAtk);
        }


        if (Selectedpp.Count > 0)
        {
            foreach (var item in Selectedpp)
            {
                item.color = colorOff;

            }
            countPreviousSelection = Selectedpp.Count;
            RemoveObjectsFromLists(Selectedpp.Count);

            ApplyPP.Play(source);
        }


    }

    private void RemoveObjectsFromLists(int selectedCount)
    {
        ////int startIndex = Mathf.Max(0, Selectedpp.Count - selectedCount);
        //int startIndex = 0;
        //Selectedpp.RemoveRange(startIndex, selectedCount);
        //ActivatedIcon.RemoveRange(startIndex, selectedCount);

        int endIndex = selectedCount - 1;
        int endIndexSelection = ActivatedIcon.Count - 1;

        for (int i = endIndex; i >= 0; i--)
        {
            Selectedpp.RemoveAt(i);
            ActivatedIcon.RemoveAt(endIndexSelection);
            endIndexSelection--;
        }
        // reset bool
        hasSelected = false;
    }

    public void Reset_Bonus()
    {
        //Debug.Log("AAAAAAAAAAAAAAAAAAA___" + "           " + countPreviousSelection);

        if (countPreviousSelection > 0)
        {
            if (Selectedpp.Count >= 5)
            {
                RemovePPStatsEffect(chara, 2);
            }
            else if (Selectedpp.Count >= 4)
            {
                RemovePPStatsEffect(chara, 1.8f);
            }
            else if (Selectedpp.Count >= 3)
            {
                RemovePPStatsEffect(chara, 1.6f);
            }
            else if (Selectedpp.Count >= 2)
            {
                RemovePPStatsEffect(chara, 1.4f);
            }
            else if (Selectedpp.Count >= 1)
            {
                RemovePPStatsEffect(chara, 1.2f);
            }
            countPreviousSelection = 0;
        }

    }

    void ResetSelection(Image img)
    {
        img.color = colorOff;
    }

    void PPStatEffect(Chara_BaseStats target, float bonus)
    {
        target.Battle_Stats.currentAtk = target.Battle_Stats.currentAtk * bonus;
        target.Battle_Stats.currentDef = target.Battle_Stats.currentDef * bonus;
        target.Battle_Stats.currentMatk = target.Battle_Stats.currentMatk * bonus;
        target.Battle_Stats.currentMdef = target.Battle_Stats.currentMdef * bonus;
        target.Battle_Stats.currentAcc = target.Battle_Stats.currentAcc * bonus;
        target.Battle_Stats.currentLuck = target.Battle_Stats.currentLuck * bonus;
        target.Battle_Stats.currentSpeed = target.Battle_Stats.currentSpeed * bonus;
        target.Battle_Stats.currentEva = target.Battle_Stats.currentEva * bonus;
        target.Battle_Stats.currentCritEva = target.Battle_Stats.currentCritEva * bonus;
        //target.Battle_Stats.currentCritMult = target.Battle_Stats.currentCritMult * bonus;
        target.Battle_Stats.currentCritRate = target.Battle_Stats.currentCritRate * bonus;
    }

    void RemovePPStatsEffect(Chara_BaseStats target, float bonus)
    {
        target.Battle_Stats.currentAtk = target.Battle_Stats.currentAtk * bonus;
        target.Battle_Stats.currentDef = target.Battle_Stats.currentDef * bonus;
        target.Battle_Stats.currentMatk = target.Battle_Stats.currentMatk * bonus;
        target.Battle_Stats.currentMdef = target.Battle_Stats.currentMdef * bonus;
        target.Battle_Stats.currentAcc = target.Battle_Stats.currentAcc * bonus;
        target.Battle_Stats.currentLuck = target.Battle_Stats.currentLuck * bonus;
        target.Battle_Stats.currentSpeed = target.Battle_Stats.currentSpeed * bonus;
        target.Battle_Stats.currentEva = target.Battle_Stats.currentEva * bonus;
        target.Battle_Stats.currentCritEva = target.Battle_Stats.currentCritEva * bonus;
        target.Battle_Stats.currentCritMult = target.Battle_Stats.currentCritMult * bonus;
        target.Battle_Stats.currentCritRate = target.Battle_Stats.currentCritRate * bonus;
    }


}



