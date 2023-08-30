using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UI_en_Info : MonoBehaviour
{

    [Header("CLASS")]
    public Chara_BaseStats hero;
    public Status_Effect_Manager status;
    private UI_Text_Lerp UI_Text_Lerp;
    public UI_Tweener isHit;


    [Header("TEXT")]
    public float Hp_Inter;
    public float Post_Inter;
    private float timeScale;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI SH;
    public TextMeshProUGUI BLD;
    public TextMeshProUGUI BLD_Broken;


    [Header("SLIDER")]
    public Slider HpSlider;
    public Slider HpSlider_transition;
    public Slider Shield_Slider;
    public Slider Shield_transition;
    public Slider Posture;
    public Slider Posture_transition;


    [Header("LIST")]
    public GameObject Status_Spacer;
    public List<Image> Icon_Status = new List<Image>();
    public GameObject Weak_Spacer;
    public List<Image> Icon_Weak = new List<Image>();
    List<string> processedObjects = new List<string>();


    [Header("FULL")]
    public GameObject Full;
    public GameObject Mini;
    public GameObject VulnImg;
    public GameObject Statusicon;


    [Header("SHIELD")]
    public GameObject AllShield;
    public GameObject Shield;
    public GameObject Shield_BG;
    public GameObject Blindage;
    public GameObject Blindage_Broken;

    bool IsStart = false;

    public void SetUp_Stat()
    {
        //heroPanel = Instantiate(heroPanel); //as GameObject;
        //HeroBust = hero.


        if (IsStart == false)
        {
            //status = hero.SEM;
            //hero.SetUP(); // when active double populate the vuln image list in chara base stats
            ImageWeaknessSetUp();
            //Debug.Log(" UI en SetUp ");

            //statsUI.heroName.text = hero.theName;
            HP.text = hero.life_Stats.currentHP.ToString();
            SH.text = hero.life_Stats.currentShield.ToString();
            BLD.text = hero.life_Stats.currentBlnd.ToString();
            BLD_Broken.text = hero.life_Stats.currentBlnd.ToString();

            if (hero.life_Stats.currentShield >= 1) { Shield.SetActive(true); Shield_BG.SetActive(true); }
            else { Shield.SetActive(false); Shield_BG.SetActive(false); }
            if (hero.life_Stats.currentBlnd >= 1) { Blindage.SetActive(true); }
            //AllShield.SetActive(true);
            else { Blindage.SetActive(false); }

            //create slider
            HpSlider.maxValue = hero.life_Stats.baseHP;
            HpSlider.value = hero.life_Stats.currentHP;
            HpSlider_transition.maxValue = hero.life_Stats.baseHP;
            HpSlider_transition.value = hero.life_Stats.currentHP;
            Hp_Inter = hero.life_Stats.currentHP;

            Shield_Slider.maxValue = hero.life_Stats.baseShield;
            Shield_Slider.value = hero.life_Stats.currentShield;
            Shield_transition.maxValue = hero.life_Stats.baseShield;
            Shield_transition.value = hero.life_Stats.currentShield;


            Posture.maxValue = hero.life_Stats.postureMax;
            Posture.value = hero.life_Stats.postureCurr;
            Posture_transition.maxValue = hero.life_Stats.postureMax;
            Posture_transition.value = hero.life_Stats.postureCurr;
            Post_Inter = hero.life_Stats.postureCurr;

            //transform.SetParent(heroPanelSpacer, false);
        }

        if (IsStart == true)
        {
            status = hero.Status.SEM;
            Update_Stats();
            //UpdateStatusEffect();
        }
        IsStart = true;
    }

    void ImageWeaknessSetUp()
    {

        //Debug.Log("UI_________________ " + hero.defense.imageVulnList.Count);
        foreach (Sprite sprite in hero.defense.imageVulnList)
        {
            //GameObject newButton2 = Instantiate(autoSelect, spacer, false);
            GameObject newImg = Instantiate(VulnImg, Weak_Spacer.transform, false);
            newImg.GetComponent<Image>().sprite = sprite;
            newImg.SetActive(true);
        }
    }

    private void UpdateStatusEffect()
    {
        // check si il est deja la ne pas instantier merci bg
        if (status.StatusEffectOnCharacter.Count > 0)
        {
            foreach (Status_Effect_SO effect in status.StatusEffectOnCharacter)
            {
                // tu dois aussi check si tu peux stack ou non les icons et ajouter une numero dessus 
                // ET un autre truc pour connaitre le nombre de tour restant pour un effet
                if (!processedObjects.Contains(effect.name))
                {
                    GameObject newImg = Instantiate(Statusicon, Status_Spacer.transform, false);
                    //GameObject vfx = Instantiate(effect.VFX_Text, gameObject.transform.position + new Vector3(-1, 1, 0), gameObject.transform.rotation);
                    //vfx.transform.localScale = new Vector3(1, 1, 1);
                    newImg.GetComponent<Image>().sprite = effect.icon;
                    processedObjects.Add(effect.name);
                    effect.isUIon = true;
                }
                else if (processedObjects.Contains(effect.name))
                {
                    // on decremente ui turn left text
                    if (effect.numberTurnLeft == 0)
                    {
                        //delete l ian=mge du status
                    }
                }
            }
        }
    }

    void Update_Stats()
    {
        //Debug.Log("IsStart ");  

        //ui animation when hit its bad
        //isHit.enabled = true;

        StartCoroutine(UpdateHP());
        StartCoroutine(UpdatePost());
        //StopCoroutine(UpdateHP());
        //----------------- need to do better for shield and all
        StartCoroutine(UpdateSh());
        StartCoroutine(UpdateBld());
        //
        UpdateStatusEffect();

        if (hero.life_Stats.currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator UpdateHP()
    {
        // without animation
        scoreText = HP;
        float t = (Hp_Inter - hero.life_Stats.currentHP) / 100; // trouver ratio de temps selon la difference entre pre et next value hp, plus elle est grand plus c'est rapide plus elle est petite plus c est lent.
        StartCoroutine(UpdateTextCoroutine(HP.text, Hp_Inter, hero.life_Stats.currentHP, 0.35f));
        HP.text = hero.life_Stats.currentHP.ToString();

        HpSlider.value = hero.life_Stats.currentHP;
        //StartCoroutine(LerpSlider(HpSlider, hero.currentHP));
        yield return new WaitForSeconds(0.5f);
        //StartCoroutine(LerpSlider(HpSlider_transition, hero.currentHP));
        HpSlider_transition.value = hero.life_Stats.currentHP;
        //Debug.Log(cpt + " 2 time scale = " + timeScale);
        Hp_Inter = hero.life_Stats.currentHP;

    }

    IEnumerator UpdateSh()
    {

        SH.text = hero.life_Stats.currentShield.ToString();
        // qd on gagne du shield on augmente aussi sa basevalue et quand on en perd on ne peut que sur sa current value
        Shield_Slider.maxValue = hero.life_Stats.baseShield;
        Shield_Slider.value = hero.life_Stats.currentShield;
        yield return new WaitForSeconds(1f);
        Shield_transition.maxValue = hero.life_Stats.baseShield;
        Shield_transition.value = hero.life_Stats.currentShield;

        if (0 >= hero.life_Stats.currentShield)
        {
            Shield.SetActive(false); Shield_BG.SetActive(false);

            Blindage_Broken.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            BLD.text = hero.life_Stats.currentBlnd.ToString();
            BLD_Broken.text = hero.life_Stats.currentBlnd.ToString();
            // au debut du tour il redevient normal
            yield return new WaitForSeconds(2f);
            Blindage_Broken.SetActive(false);
        }

    }
    IEnumerator UpdateBld()
    {
        Blindage.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        BLD.text = hero.life_Stats.currentBlnd.ToString();
        BLD_Broken.text = hero.life_Stats.currentBlnd.ToString();
        if (0 >= hero.life_Stats.currentBlnd)
        {
            AllShield.SetActive(false);
            //Shield_BG.SetActive(false);
            //Blindage.SetActive(false);
            //Blindage_Broken.SetActive(false);
        }
    }

    IEnumerator UpdatePost()
    {
        timeScale = 0;

        //while (timeScale < 1)
        //{
        //    timeScale += Time.deltaTime * 3;
        //    Posture.value = Mathf.Lerp(Post_Inter, hero.postureCurr, timeScale);
        //    yield return new WaitForSeconds(1f);
        //    Posture_transition.value = Mathf.Lerp(Post_Inter, hero.postureCurr, timeScale);
        //    Debug.Log(Posture.value);

        //    //Debug.Log(cpt + " 2 time scale = " + timeScale);
        //    Post_Inter = hero.postureCurr;
        //}
        yield return new WaitForSeconds(1f);
        Posture.value = hero.life_Stats.postureCurr;
        yield return new WaitForSeconds(1f);
        Posture_transition.value = hero.life_Stats.postureCurr;
        Post_Inter = hero.life_Stats.postureCurr;
        //Debug.Log(" UI value = " + Posture.value + "    " + hero.ID);
        //Debug.Log(" UI hero = " + hero.postureCurr + "    " + hero.ID);

        //Debug.Log("ici update UI en");
    }
    IEnumerator LerpSlider(Slider slider, float to)
    {
        while (true)
        {
            float end = slider.value + to;
            // Lerp the slider value towards the maximum value
            float lerpValue = Mathf.Lerp(slider.value, end, Time.deltaTime * 0.7f);

            // Set the value of the slider to the lerp value
            slider.value = lerpValue;

            // Wait for the next frame
            yield return null;
        }
    }
    private IEnumerator UpdateTextCoroutine(string text, float from, float to, float time)
    {
        float score = 0;
        float currentTime = Time.timeSinceLevelLoad;
        float elapsedTime = 0.0f;
        float lastTime = currentTime;

        while (time > 0 && elapsedTime < time)
        {
            //Update Time
            currentTime = Time.timeSinceLevelLoad;
            elapsedTime += currentTime - lastTime;
            lastTime = currentTime;

            //Update current value
            score = Mathf.Lerp(from, to, elapsedTime / time);

            //Update the UI text component
            scoreText.text = score.ToString(".");
            yield return null;
        }
        scoreText.text = to.ToString();

    }

    public void ShowFull()
    {
        Full.SetActive(true);
    }

    public void HideFull()
    {
        Full.SetActive(false);
    }

}