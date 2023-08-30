using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Win_Hero_Panel : MonoBehaviour
{
    float previeousXp;

    public GameObject HeroBust;

    public TextMeshProUGUI Text_XpCurrent;
    public TextMeshProUGUI Text_Xp;
    public TextMeshProUGUI Text_XpGain;
    public TextMeshProUGUI Text_Level;

    public Slider Xp_Slider;
    public Slider Xp_transition;

    //faire avec xp du core
    public Slider Core_Slider;
    public Slider Core_transition;

    //private void Start()
    //{
    //    Xp_Slider = 
    //}

    public void SetUp(Chara_Leveling xp)
    {
        //previeousXp = xp.currentXP - xp.xpReceived;
    }

    public void SetUpPortrait(Sprite img)
    {
        HeroBust.GetComponent<Image>().sprite = img;
    }

    public void SetUpText(Chara_Leveling xp)
    {
        Text_Level.text = xp.currentLV.ToString();

        Text_XpGain.text = "+ " + xp.xpReceived.ToString() + " Exp !";
        // ici on update le text qui servira a mettre current/required 
        // a finir plus tard, bisous
        //StartCoroutine(UpdateTextCoroutine(Text_XpCurrent.text, xp.currentXP, xp.currentXP, 1f));

        Text_Xp.text = xp.currentXP.ToString()            
            + " / " + xp.requiredXP.ToString();
    }


    public void MoveSlider(Chara_Leveling xp)
    {
        //Xp_transition.maxValue = xp.requiredXP;
        //Xp_Slider.maxValue = xp.requiredXP;

        // faire avec xp du core
        //Core_transition.maxValue = xp.requiredXP;

        StartCoroutine(SliderXpMove(xp));
    }




    IEnumerator SliderXpMove(Chara_Leveling xp)
    {
        ////Xp_Slider.maxValue = xp.requiredXP;

        ////Xp_Slider.value = xp.currentXP;
        ////StartCoroutine(LerpSlider(Xp_transition, xp.currentXP));
        yield return new WaitForSeconds(0.1f);
        Xp_transition.value = xp.currentXP / xp.requiredXP;
        yield return new WaitForSeconds(0.5f);
        Xp_Slider.value = xp.currentXP / xp.requiredXP;
        //float end = Xp_Slider.value + xp.currentXP;
        //StartCoroutine(LerpSlider(Xp_Slider, previeousXp, end));

        //Xp_Slider.value = xp.currentXP;

        ////faire avec xp du core
        ////Core_Slider.value = xp.currentXP;
        ////StartCoroutine(LerpSlider(Core_transition, xp.currentXP));
        yield return new WaitForSeconds(0.1f);
        Core_transition.value = xp.currentXP / xp.requiredXP;
        yield return new WaitForSeconds(0.5f);
        Core_Slider.value = xp.currentXP / xp.requiredXP;
        //Core_Slider.maxValue = xp.requiredXP;
        //float end2 = Core_Slider.value + xp.currentXP;
        //StartCoroutine(LerpSlider(Core_Slider, previeousXp, end2));
        ////Core_Slider.value = xp.currentXP;
    }


    IEnumerator LerpSlider(Slider slider, float from, float to)
    {
        while (true)
        {
            // Lerp the slider value towards the maximum value
            float lerpValue = Mathf.Lerp(from, to, Time.deltaTime * 2f);

            // Set the value of the slider to the lerp value
            slider.value = lerpValue;

            // Wait for the next frame
        }
            yield break;
    }

    // fonction level up

    // fonction jesaispluslol


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
            text = score.ToString(".");
            yield return null;
        }
        text = to.ToString();

    }

}
