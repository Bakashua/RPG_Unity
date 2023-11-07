using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu_HeroMenu : MonoBehaviour
{
    [SerializeField] Hero_Party hp;

    [SerializeField] List<Image> characterPreviews = new();

    [SerializeField] Image chara_Img;
    [SerializeField] TextMeshProUGUI chara_name;
    [SerializeField] TextMeshProUGUI chara_level;
    [SerializeField] Slider sliderLvl;
    [SerializeField] TextMeshProUGUI chara_levelNext;
    [SerializeField] TextMeshProUGUI CurrHpOnMax;
    [SerializeField] Slider sliderHp;
    [SerializeField] TextMeshProUGUI CurrMpOnMax;
    [SerializeField] Slider sliderMp;
    [SerializeField] TextMeshProUGUI CurrTpOnMax;
    [SerializeField] Slider sliderTp;

    [SerializeField] TextMeshProUGUI ATK;
    [SerializeField] TextMeshProUGUI MATK;
    [SerializeField] TextMeshProUGUI SPEED;
    [SerializeField] TextMeshProUGUI ACC;
    [SerializeField] TextMeshProUGUI CRITRATE;
    [SerializeField] TextMeshProUGUI DEF;
    [SerializeField] TextMeshProUGUI MDEF;
    [SerializeField] TextMeshProUGUI LUCK;
    [SerializeField] TextMeshProUGUI EVA;
    [SerializeField] TextMeshProUGUI CRTID;

    int currIndex = 0;

    private void Start()
    {
        SetUpPage(hp.HeroInParty_Data[currIndex].Stats);
        SetupPreview();
    }


    void SetUpPage(Chara_BaseStats chara)
    {
        chara.SetUP();

        chara_Img.sprite = chara.general_Setting.CharaBody;
        chara_name.text = chara.general_Setting.CharaName;

        sliderLvl.maxValue = chara.general_Setting.CharaHero.leveling.requiredXP;
        sliderLvl.value = chara.general_Setting.CharaHero.leveling.currentXP;

        chara_level.text = chara.general_Setting.CharaHero.leveling.currentLV.ToString();
        chara_levelNext.text = "Next : " + chara.general_Setting.CharaHero.leveling.requiredXP.ToString();

        sliderHp.maxValue = chara.life_Stats.baseHP;
        sliderHp.value = chara.life_Stats.currentHP;
        CurrHpOnMax.text = chara.life_Stats.currentHP + " / " + chara.life_Stats.baseHP;
        sliderMp.maxValue = chara.life_Stats.baseMP;
        sliderMp.value = chara.life_Stats.currentMP;
        CurrMpOnMax.text = chara.life_Stats.currentMP + " / " + chara.life_Stats.baseMP;
        sliderTp.maxValue = chara.life_Stats.baseTP;
        sliderTp.value = chara.life_Stats.currentTP;
        CurrTpOnMax.text = chara.life_Stats.currentTP + " / " + chara.life_Stats.baseTP;

        // ps on devrait faire un truc : si curr en dessous de base le text est rouge, au dessus il est jaune brillant
        ATK.text = chara.Battle_Stats.currentAtk.ToString();
        MATK.text = chara.Battle_Stats.currentMatk.ToString();
        SPEED.text = chara.Battle_Stats.currentSpeed.ToString();
        ACC.text = chara.Battle_Stats.currentAcc.ToString();
        CRITRATE.text = chara.Battle_Stats.currentCritRate.ToString();
        DEF.text = chara.Battle_Stats.currentDef.ToString();
        MDEF.text = chara.Battle_Stats.currentMdef.ToString();
        LUCK.text = chara.Battle_Stats.currentLuck.ToString();
        EVA.text = chara.Battle_Stats.currentEva.ToString();
        CRTID.text = chara.Battle_Stats.currentCritMult.ToString();
    }

    void SetupPreview()
    {
        for (int i = 0; i < characterPreviews.Count; i++)
        {
            // Check if the character index is within the valid range
            if (i < hp.HeroInParty_Data.Count)
            {
                // Set the sprite of the image based on the character's portrait
                characterPreviews[i].sprite = hp.HeroInParty_Data[i].Stats.general_Setting.Icon;

                // Highlight the selected character
                if (i == currIndex)
                {
                    characterPreviews[i].color = Color.white; // Highlighted color
                }
                else
                {
                    characterPreviews[i].color = Color.gray; // Non-highlighted color
                }

                // Make the image visible
                characterPreviews[i].gameObject.SetActive(true);
            }
            else
            {
                // Hide the image if there is no corresponding character
                characterPreviews[i].gameObject.SetActive(false);
            }
        }
    }

    void updataPreview()
    {
        for (int i = 0; i < characterPreviews.Count; i++)
        {
            if (i < hp.HeroInParty_Data.Count)
            {
                if (i == currIndex)
                {
                    characterPreviews[i].color = Color.white;
                }
                else
                {
                    characterPreviews[i].color = Color.gray;
                }
            }
        }
    }


    public void NextHero()
    {
        currIndex++;
        if (currIndex >= hp.HeroInParty_Data.Count)
        {
            currIndex = 0;
        }
        SetUpPage(hp.HeroInParty_Data[currIndex].Stats);
        updataPreview();
    }

    public void PreviousHero()
    {
        currIndex--;
        if (currIndex <= 0)
        {
            currIndex = hp.HeroInParty_Data.Count-1;
        }
        SetUpPage(hp.HeroInParty_Data[currIndex].Stats);
        updataPreview();
    }


}
