using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UI_Combat_Timeline : MonoBehaviour
{
    public GameObject Icon;
    public Transform TimelineParent;
    public BattleStateMachine BSM;
    private List<UI_Combat_TimelineIcon> icons = new List<UI_Combat_TimelineIcon>();


    public void Listener_StartTimeLine()
    {
        foreach (var item in BSM.herosInBattle)
        {
            GameObject icon = Instantiate(Icon, TimelineParent);
            icons.Add(icon.GetComponent<UI_Combat_TimelineIcon>());

            UI_Combat_TimelineIcon iconScript = icon.GetComponent<UI_Combat_TimelineIcon>();
            iconScript.HSM = item.GetComponent<HeroStateMachine>();
            iconScript.IsHero = true;
            iconScript.Icon_Ch.sprite = item.GetComponent<HeroStateMachine>().hero.general_Setting.Icon;

            icon.SetActive(true);
        }

        foreach (var item in BSM.enemyInBattle)
        {
            GameObject icon = Instantiate(Icon, TimelineParent);
            icons.Add(icon.GetComponent<UI_Combat_TimelineIcon>());

            UI_Combat_TimelineIcon iconScript = icon.GetComponent<UI_Combat_TimelineIcon>();
            iconScript.ESM = item.GetComponent<EnemyStateMachine>();
            iconScript.IsHero = false;
            iconScript.Icon_Ch.sprite = item.GetComponent<EnemyStateMachine>().enemy.general_Setting.Icon;

            icon.SetActive(true);
        }

        StartCoroutine(UpdateIconPos());
    }


    public IEnumerator UpdateIconPos()
    {
        while (true)
        {

            for (int i = 0; i < icons.Count; i++)
            {
                float currentCooldown = 0, maxCooldown = 0;

                if (icons[i].IsHero)
                {
                    currentCooldown = icons[i].HSM.curentCooldown;
                    maxCooldown = icons[i].HSM.maxCooldown;
                }
                if (!icons[i].IsHero)
                {
                    currentCooldown = icons[i].ESM.curentCooldown;
                    maxCooldown = icons[i].ESM.maxCooldown;
                }


                float normalizedCooldown = Mathf.InverseLerp(0, maxCooldown, currentCooldown);

                Slider slider = GetComponentInChildren<Slider>();
                //float iconPosition = Mathf.Lerp(slider.minValue, slider.maxValue, normalizedCooldown);
                float iconPosition = Mathf.Lerp(-300f, 300f, normalizedCooldown);

                //// Smooth the transition using an easing function (e.g., using SmoothStep)
                //float smoothedNormalizedCooldown = Mathf.SmoothStep(-300f, 300f, normalizedCooldown);
                //float iconPosition = Mathf.Lerp(slider.minValue, slider.maxValue, smoothedNormalizedCooldown);

                //icons[i].transform.localPosition.x = new Vector3(iconPosition, 0f, 0f);
                //icons[i].transform.localPosition.x = Mathf.Lerp(icon[i].transform.localPosition.x, iconPosition, 1f);
                icons[i].transform.DOLocalMoveX(iconPosition, normalizedCooldown, false);

            }

            yield return new WaitForSeconds(0.1f);
            //StartCoroutine(UpdateIconPos());
        }

    }
}
