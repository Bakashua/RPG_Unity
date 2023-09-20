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
        if (BattleStateMachine.instance_BSM.enemyInBattle.Count > 0 || BattleStateMachine.instance_BSM.herosInBattle.Count > 0)
        {

            for (int i = 0; i < icons.Count; i++)
            {
                float currentCooldown = 0, maxCooldown = 0;

                if (icons[i].IsHero)
                {
                    if (icons[i].HSM.currentState == HeroStateMachine.TurnState.DEAD)
                    {
                        icons[i].gameObject.SetActive(false);
                        icons.Remove(icons[i]);
                        break;
                    }
                    else
                    {
                        currentCooldown = icons[i].HSM.curentCooldown;
                        maxCooldown = icons[i].HSM.maxCooldown;
                    }
                }
                if (!icons[i].IsHero)
                {
                    if (icons[i].ESM.currentState == EnemyStateMachine.TurnState.DEAD)
                    {
                        icons[i].gameObject.SetActive(false);
                        icons.Remove(icons[i]);
                        break;
                    }
                    else
                    {
                        currentCooldown = icons[i].ESM.curentCooldown;
                        maxCooldown = icons[i].ESM.maxCooldown;
                    }
                }


                float normalizedCooldown = Mathf.InverseLerp(0, maxCooldown, currentCooldown);

                Slider slider = GetComponentInChildren<Slider>();

                float iconPosition = Mathf.Lerp(-300f, 300f, normalizedCooldown);


                icons[i].transform.DOLocalMoveX(iconPosition, normalizedCooldown, false);

            }

            yield return new WaitForSeconds(0.1f);

            StartCoroutine(UpdateIconPos());
        }

        

    }
}
