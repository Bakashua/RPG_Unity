using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HeroPanelStat : MonoBehaviour
{

    public Chara_BaseStats hero;
    public UI_Tweener isHit;

    public Image HeroBust;
    public Image HeroBustBack;
    //public GameObject blackpannel;
    //buttonText.GetComponent<TMP_Text>().text
    //public TextMeshProUGUI heroName;
    //public Text heroName;
    public TextMeshProUGUI heroHP;
    public TextMeshProUGUI heroShield;
    public TextMeshProUGUI heroBlnd;
    public TextMeshProUGUI heroMP;
    public TextMeshProUGUI heroTP;
    public Slider hpSlider;
    public Slider hpSlider_transition;
    public Slider shield_Slider;
    public Slider shield_transition;
    public Slider mpSlider;
    public Slider mpSlider_transition;
    public Slider tpSlider;
    public Slider tpSlider_transition;
    public Image progressBar;
    public GameObject Shield;
    public GameObject Shield_BG;
    public GameObject Blindage;
    public GameObject Blindage_Broken;
    public GameObject TP_100;
    public GameObject TP_200;

    bool IsStart = false;

    Sequence t;
    Sequence t1;

    public void SetUpStat()
    {
        //heroPanel = Instantiate(heroPanel); //as GameObject;
        //HeroBust = hero.

        if (IsStart == false)
        {
            //hero.SetUP();


            HeroBust.sprite = hero.general_Setting.HeroBust;
            HeroBustBack.sprite = hero.general_Setting.HeroBust;
            //statsUI.heroName.text = hero.theName;
            heroHP.text = hero.life_Stats.currentHP.ToString();
            heroMP.text = hero.life_Stats.currentMP.ToString();
            heroTP.text = hero.life_Stats.currentTP.ToString();
            heroShield.text = hero.life_Stats.currentShield.ToString();
            heroBlnd.text = hero.life_Stats.currentBlnd.ToString();

            if (hero.life_Stats.currentShield >= 1) { Shield.SetActive(true); Shield_BG.SetActive(true); }
            else { Shield.SetActive(false); Shield_BG.SetActive(false); }
            if (hero.life_Stats.currentBlnd >= 1) Blindage.SetActive(true);
            else { Blindage.SetActive(false); }

            //create slider
            hpSlider.maxValue = hero.life_Stats.baseHP;
            hpSlider.value = hero.life_Stats.currentHP;
            hpSlider_transition.maxValue = hero.life_Stats.baseHP;
            hpSlider_transition.value = hero.life_Stats.currentHP;

            shield_Slider.maxValue = hero.life_Stats.baseShield;
            shield_Slider.value = hero.life_Stats.currentShield;
            shield_transition.maxValue = hero.life_Stats.baseShield;
            shield_transition.value = hero.life_Stats.currentShield;

            mpSlider.maxValue = hero.life_Stats.baseMP;
            mpSlider.value = hero.life_Stats.currentMP;
            mpSlider_transition.maxValue = hero.life_Stats.baseMP;
            mpSlider_transition.value = hero.life_Stats.currentMP;

            tpSlider.maxValue = 400;
            tpSlider.value = hero.life_Stats.currentTP;
            tpSlider_transition.maxValue = 400;
            tpSlider_transition.value = hero.life_Stats.currentTP;
        }

        if (IsStart == true)
        {
            UpdateBars();
        }
        IsStart = true;

        //transform.SetParent(heroPanelSpacer, false);
    }


    void UpdateBars()
    {
        //ui animation when hit its bad
        //isHit.enabled = true;

        StartCoroutine(UpdateHP());
        StartCoroutine(UpdateMP());
        StartCoroutine(UpdateTP());
        //-----------------
        StartCoroutine(UpdateSh());
        StartCoroutine(UpdateBld());

        //if (hero.currentShield >= 0)
        //{
        //    StartCoroutine(UpdateSh());
        //}
        //else { Shield.SetActive(false); Shield_BG.SetActive(false); }

        //if (hero.currentBlnd >= 0)
        //{
        //    StartCoroutine(UpdateBld());
        //}
        //else { Blindage.SetActive(false); }        
    }

    IEnumerator UpdateHP()
    {
        heroHP.text = hero.life_Stats.currentHP.ToString();
        hpSlider.value = hero.life_Stats.currentHP;
        yield return new WaitForSeconds(1f);
        hpSlider_transition.value = hero.life_Stats.currentHP;
    }
    IEnumerator UpdateMP()
    {
        heroMP.text = hero.life_Stats.currentMP.ToString();
        mpSlider.value = hero.life_Stats.currentMP;
        yield return new WaitForSeconds(1f);
        mpSlider_transition.value = hero.life_Stats.currentMP;
    }
    IEnumerator UpdateTP()
    {
        heroTP.text = hero.life_Stats.currentTP.ToString();
        tpSlider.value = hero.life_Stats.currentTP;
        yield return new WaitForSeconds(1f);
        tpSlider_transition.value = hero.life_Stats.currentTP;

        if(hero.life_Stats.currentTP >= 100) { TP_100.SetActive(true);  }
        if (hero.life_Stats.currentTP >= 200) { TP_200.SetActive(true); TP_100.SetActive(false); }
        if (hero.life_Stats.currentTP <= 100) { TP_100.SetActive(false); }
    }
    IEnumerator UpdateSh()
    {
        //Debug.Log("hero ui");
        //Debug.Log(hero.life_Stats.currentShield);

        // if shield then show otherwise dont show
        if (hero.life_Stats.currentShield > 0)
        { Shield.SetActive(true); Shield_BG.SetActive(true);  }
        else { Shield.SetActive(false); Shield_BG.SetActive(false); }


        heroShield.text = hero.life_Stats.currentShield.ToString();

        // qd on gagne du shield on augmente aussi sa basevalue et quand on en perd on ne peut que sur sa current value
        shield_transition.maxValue = hero.life_Stats.baseHP;
        shield_transition.value = hero.life_Stats.currentShield;
        yield return new WaitForSeconds(1f);
        shield_Slider.maxValue = hero.life_Stats.baseHP;
        shield_Slider.value = hero.life_Stats.currentShield;

        if (0 >= hero.life_Stats.currentShield)
        {
            Shield.SetActive(false); Shield_BG.SetActive(false);

            Blindage_Broken.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            heroBlnd.text = hero.life_Stats.currentBlnd.ToString();
            // au debut du tour il redevient normal
            yield return new WaitForSeconds(2f);
            Blindage_Broken.SetActive(false);
        }

    }
    IEnumerator UpdateBld()
    {

        // if shield then show otherwise dont show
        if (hero.life_Stats.currentBlnd > 0)
        { Blindage.SetActive(true); Blindage.SetActive(true); }
        else { Blindage.SetActive(false); Blindage.SetActive(false); }

        yield return new WaitForSeconds(0.5f);
        heroBlnd.text = hero.life_Stats.currentBlnd.ToString();


        //if (0 >= hero.life_Stats.currentBlnd)
        //{
        //    Shield.SetActive(false); Shield_BG.SetActive(false);
        //}

    }

    public void Listener_PlayerTurn()
    { 
        // whem is turn move hero bust + back up and down
        if (BattleStateMachine.instance_BSM.herosToManage[0].GetComponent<HeroStateMachine>().hero.name == hero.name)
        {
            Vector3 to = new Vector3(gameObject.transform.position.x, 10, 0);
            t = DOTween.Sequence();
            t.Append(
            HeroBust.rectTransform.DOMoveY((HeroBust.rectTransform.position.y + 2), 1.50f).SetEase(Ease.InOutSine).SetDelay(0.2f).SetLoops(-1, LoopType.Yoyo)
                    );
            t.SetLoops(-1, LoopType.Yoyo);

            t1 = DOTween.Sequence();
            t1.Append(
            HeroBustBack.rectTransform.DOMoveY((HeroBust.rectTransform.position.y + 2), 1.50f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo)
                    );
            t1.SetLoops(-1, LoopType.Yoyo);
        }
    }

    public void Listener_EndTurn()
    {
        Debug.Log("here the script to stop player animation when it finishes his turn");
        //Debug.Log(t + "  ____  " + t1);
        //t.Pause();
        //t1.Pause();
        if (BattleStateMachine.instance_BSM.herosToManage[0].GetComponent<HeroStateMachine>().hero.name == hero.name)
        {
            //Debug.Log(BattleStateMachine.instance_BSM.herosToManage[0].GetComponent<HeroStateMachine>().hero.name + "  __________  " + hero.name);
            t.Kill();
            t1.Kill();
            //t.SetAutoKill(true);
            //t1.SetAutoKill(true);
        }
    }

}
