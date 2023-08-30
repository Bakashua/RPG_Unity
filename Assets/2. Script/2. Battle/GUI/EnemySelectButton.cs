using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemySelectButton : MonoBehaviour
{
    //[HideInInspector] 
    public GameObject enemyPrefab;
    public UI_en_Info enemy_ui;

    //public bool can;
    BaseAttack atk;
    HandleTurn turn;
    float damage;
    public TextMeshProUGUI Atk_info;

    [Header("AUDIO")]
    public GameEvent ClickAttack;
    public AudioSource PlayAudio;
    public AudioClip Click;
    public AudioClip Hover;

    public void SelectEnemy()
    {

        UI_PlayerTurn.instance_PTM.Hero_Input_ChooseTarget(enemyPrefab);
        Battle_GUI_Manager.instance_BUIM.targetButton_Cancel.SetActive(false);
        //PlayAudio.PlayOneShot(Click);
        //soundEffect_Manager.SE_ClickPlay();
        ClickAttack.TriggerEvent();
        if (enemyPrefab.tag == "Enemy")
        {
            HideSelector();
        }

        //Debug.Log("click target btn");        
    }

    public void HideSelector()
    {

        enemyPrefab.transform.GetChild(0).gameObject.SetActive(false);
        if (enemyPrefab.tag == "Enemy")
        {
            enemy_ui.HideFull();
        }
        //Debug.Log("click HideSelector");

        ClearInformation();
    }

    public void ShowSelector()
    {
        enemyPrefab.transform.GetChild(0).gameObject.SetActive(true);
        PlayAudio.PlayOneShot(Hover);
        if (enemyPrefab.tag == "Enemy")
        {
            enemy_ui.ShowFull();
        }
        BattleStateMachine.instance_BSM.herosToManage[0].GetComponent<Player_Camera_Combat>().Target_Enemy(enemyPrefab);
        //Player_Camera_Combat.instance.Target_Enemy(enemyPrefab);
        BattleCamManager.instance_BCam.Target_Enemy(enemyPrefab);
        //Debug.Log("click ShowSelector");

        GetInformation();
    }

    public void GetInformation()
    {
        //Debug.Log("Get Information");
        turn = UI_PlayerTurn.instance_PTM.heroChoice;
        atk = turn.choosenAttack;

        if (enemyPrefab.tag == "Enemy")
        { 
            damage = atk.ComputePreview
            (
            UI_PlayerTurn.instance_PTM.herosToManage[0].GetComponent<HeroStateMachine>().hero,
            enemyPrefab.GetComponent<EnemyStateMachine>().enemy,
            enemyPrefab.GetComponent<Damage_Matrice>()
            );
        }
        if (enemyPrefab.tag == "Hero")
        {
            damage = atk.ComputePreview
            (
            UI_PlayerTurn.instance_PTM.herosToManage[0].GetComponent<HeroStateMachine>().hero,
            enemyPrefab.GetComponent<HeroStateMachine>().hero,
            enemyPrefab.GetComponent<Damage_Matrice>()
            );
        }
        // preview ?
        //StartCoroutine(atk.ResetTargetTiles());
        atk.FindPreviewTile(enemyPrefab);


        TextMeshProUGUI text = new TextMeshProUGUI();
        text.text = atk.Enumsetting.elements.ToString();

        #region set text color
        if (atk.Enumsetting.elements == Elements.Bash)
        {
            Atk_info.color = new Color(0.85f, 0.85f, 0.85f, 1);
        }
        if (atk.Enumsetting.elements == Elements.Dark)
        {
            Atk_info.color = new Color(0.4f, 0.16f, 0.54f, 1);
        }
        if (atk.Enumsetting.elements == Elements.Earth)
        {
            Atk_info.color = new Color(0.74f, 0.4f, 0.17f, 1);
        }
        if (atk.Enumsetting.elements == Elements.Fire)
        {
            Atk_info.color = new Color(0.76f, 0.12f, 0.08f, 1);
        }
        if (atk.Enumsetting.elements == Elements.Light)
        {
            Atk_info.color = new Color(1f, 0.95f, 0.56f, 1);
        }
        if (atk.Enumsetting.elements == Elements.Pierce)
        {
            Atk_info.color = new Color(0.85f, 0.85f, 0.85f, 1);
        }
        if (atk.Enumsetting.elements == Elements.Slash)
        {
            Atk_info.color = new Color(0.85f, 0.85f, 0.85f, 1);
        }
        if (atk.Enumsetting.elements == Elements.Water)
        {
            Atk_info.color = new Color(0.42f, 0.61f, 0.9f, 1);
        }
        if (atk.Enumsetting.elements == Elements.Wind)
        {
            Atk_info.color = new Color(0.31f, 0.86f, 0.55f, 1);
        }
        #endregion

        //Debug.Log(text.color);
        Atk_info.text = damage.ToString() + " " + text.text;
    }

    public void ClearInformation()
    {
        turn = UI_PlayerTurn.instance_PTM.heroChoice;
        atk = turn.choosenAttack;

        atk.ClearTilePreview();
    }


}
