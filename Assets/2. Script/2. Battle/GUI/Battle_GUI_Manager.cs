using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

/*
 //trace une ligne de prefab là ou on va faire le spell
https://www.youtube.com/watch?v=d_zviIfcCHk&ab_channel=Unity
Vector3 position = Utility.MouseToTerrainPosition();
Graphics.DrawMesh(gameobjectMesh,   position,   Quaternion.identity,    gameobjectMat,  0);
 */

public class Battle_GUI_Manager : MonoBehaviour
{
    #region Data
    [HideInInspector] public static Battle_GUI_Manager instance_BUIM;

    [Header("CLASS")]
    BattleStateMachine BSM;
    public Hero_Party hero_Party;
    public Launch_Battle Launch_Battle;

    [Header("EVENT")]
    public GameEvent Display_Hero;
    public GameEvent Update_En_UI;
    public GameEvent Update_He_UI;
    //public GameEvent UpdateBSM;

    [Header("Start Battle")]
    public GameObject StartBattle;

    [Header("DAMAGE POP UP")]
    public UI_SO_DamagePopUp UI_SO_DamagePopUp;
    public GameObject PopUp_Parent;
    public GameObject PopUp_GO;
    public float PopUp_Damage;
    public float PopUp_LifeTime;
    public Transform PopUp_Spawn;

    [Header("COLORS")]
    // { CRITICAL, EFFECTIVE, UNEFFECTIVE, NORMAL, MISS, BREAK
    Color critical = new Color(1f, 0.654f, 0f, 1f);
    Color effective = new Color(0.8f, 0.11f, 0.11f, 1f);
    Color uneffective = new Color(0.0f, 0.5f, 1f, 1f);
    Color normal = new Color(1f, 1f, 1f, 1f);
    Color miss = new Color(1f, 1f, 1f, 1f);
    Color breaK = new Color(0.87f, 0.75f, 0.3f, 1f);

    [Header("HERO DISPLAY")]
    public Transform spacerHero;
    public GameObject HeroDiplay;
    public List<GameObject> heroDisplay = new List<GameObject>();

    [Header("ENEMY DISPLAY")]
    public GameObject EnemyDiplay;

    [Header("GUI TARGET BTN")]
    public Transform spacer;
    public Transform spacer2;
    //enemybutton
    public GameObject atk_info_Box;
    public TextMeshProUGUI atk_info_Text;
    public GameObject targetButton;
    public GameObject targetButton_Cancel;
    public GameObject autoSelect;
    private GameObject clear_autoSelect;
    public List<Button> enemyList = new List<Button>();
    private List<GameObject> clear_heroList = new List<GameObject>();
    //alliesbutton
    public List<Button> alliesList = new List<Button>();
    public List<GameObject> clear_enemyList = new List<GameObject>();

    [Header("GUI END FIGHT")]
    public GameObject ButtonNext;
    public GameObject ItemDropGO;
    int iteration = 0;
    public GameObject BackPanel;
    public GameObject Win_Screen;
    public GameObject Win_Bnt;
    public GameObject Win_HeroParty;
    public GameObject HeroGui;
    public GameObject Win_TacticalBonus;
    public GameObject Win_Item;
    public GameObject Loose_Screen;

    bool win01;
    bool win02;
    bool isLevelingUP;
    #region EndBattleCanvas
    //[Header("End Battle")]
    //public GameObject battleGUI;
    //public GameObject winningUI;
    //public GameObject loosingUI;
    //public GameObject levelUpUI;
    //public GameObject monsterSpawnButton;

    ////public GameObject damagePoppup;
    ////public GameObject damagePoppupSpawner;

    //[Header("End Battle Hud")]
    //public GameObject panelForInstantiation;
    //public GameObject goInstancebonusxp;
    //public GameObject goInstanceherolvPLUSxpbonus;
    //public GameObject goInstanceItem;
    //public TextMeshProUGUI panelTitle;
    #endregion

    //[Header("Level Up")]
    //public GameObject goInstancehrostats;
    #endregion

    private void Awake()
    {
        if (instance_BUIM != null && instance_BUIM != this)
        { Destroy(this); }
        else
        { instance_BUIM = this; }
        //Debug.Log(instance_BUIM);        
    }

    void Start()
    {
        BSM = BattleStateMachine.instance_BSM;
        ScriptableObject.CreateInstance("UI_SO_DamagePopUp");

        //Battle_GUI_Manager BM = FindObjectOfType<Battle_GUI_Manager>();
        //BM.Launch_Battle = this;
        Launch_Battle = FindObjectOfType<Launch_Battle>();
    }

    public void CreateTargetButtons(TargetType targetType)
    {
        // CHANGE // SET CAMERA TO SELECT A TARGET
        BattleCamManager.instance_BCam.PlayerChooseTarget();

        targetButton_Cancel.SetActive(true);
        // AUTO SELECT BTN
        GameObject newButton2 = Instantiate(autoSelect, spacer, false);
        clear_autoSelect = newButton2;


        //create button for allies
        if (targetType == TargetType.ally)
        {
            foreach (GameObject hero in BSM.herosInBattle)
            {
                //// instantiate button for every ennemy in the scene
                GameObject newButton1 = Instantiate(targetButton, spacer2, false);
                //// link button to the enemy data
                EnemySelectButton button = newButton1.GetComponent<EnemySelectButton>();
                //// here bouton = gameobject enemyX, tres cool
                button.enemyPrefab = hero;
                button.Atk_info = atk_info_Text;
                atk_info_Box.SetActive(true);

                alliesList.Add(newButton1.GetComponent<Button>());
                clear_heroList.Add(newButton1);
            }
        }

        //create button for enemy
        if (targetType == TargetType.enemy)
        {
            foreach (GameObject enemy in BSM.enemyInBattle)
            {
                //// instantiate button for every ennemy in the scene
                GameObject newButton = Instantiate(targetButton, spacer, false);
                //// link button to the enemy data
                EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();
                //// here bouton = gameobject enemyX, tres cool
                button.enemyPrefab = enemy;
                button.enemy_ui = enemy.GetComponent<EnemyStateMachine>().enemy_ui;
                button.Atk_info = atk_info_Text;
                atk_info_Box.SetActive(true);

                enemyList.Add(newButton.GetComponent<Button>());
                clear_enemyList.Add(newButton);

                //set up Navigation
                //SetUpNavigation_Enemy();
                foreach (Button aaa in enemyList)
                {
                    int i = enemyList.IndexOf(aaa);
                    //Debug.Log("aaaa   " + enemyList[i]);
                    //Debug.Log(i);
                }
                // select un des btn des ennemis mais le probleme c et que ca le selectionne pour faire l atk MY TRAGEDY !!!!!!!!!!!!!!!!!!!
                //EventSystem.current.SetSelectedGameObject(newButton);
            }
        }
        //for(i = 0, i < enemyList.Count, i++)
    }
     
    public void ClearSelectList()
    {
        atk_info_Box.SetActive(false);
        // Call on Start and Death
        targetButton_Cancel.SetActive(false);
        Destroy(clear_autoSelect);
        //Debug.Log("1");
        //cleanup
        foreach (GameObject target in clear_enemyList)
        { Destroy(target); }
        enemyList.Clear();
        clear_enemyList.Clear();
        foreach (GameObject target in clear_heroList)
        { Destroy(target); }
        alliesList.Clear();
        clear_heroList.Clear();
    }

    #region UI In Combat
    // listener on bui_manager
    public void Instantiate_HeroDisplay()
    {
        foreach (GameObject hero in BSM.herosInBattle)
        {
            //// instantiate button for every ennemy in the scene
            GameObject newHeroDiplay = Instantiate(HeroDiplay, spacerHero, false);
            newHeroDiplay.GetComponentInChildren<HeroPanelStat>().hero = hero.GetComponent<HeroStateMachine>().hero;
            newHeroDiplay.SetActive(true);
            //Debug.Log("aaaa");
            //hero.GetComponent<Chara_BaseStats>();

            //newHeroDiplay.GetComponent<HeroPanelStat>().SetUpStat();
            Display_Hero.TriggerEvent();
        }
    }

    // listener on bui_manager
    public void Instantiate_EnemyDisplay()
    {
        foreach (GameObject enemy in BSM.enemyInBattle)
        {
            //Debug.Log(EnemyDiplay);
            //Debug.Log("bbbb");

            // vector 3 to set up instance 
            Transform spawnPos = enemy.transform;
            Vector3 distance = new Vector3(1, 1, -2);
            //// instantiate button for every ennemy in the scene
            GameObject newEnDiplay = Instantiate(EnemyDiplay, (spawnPos.position + distance), spawnPos.rotation, spawnPos);
            newEnDiplay.GetComponent<UI_en_Info>().hero = enemy.GetComponent<EnemyStateMachine>().enemy;
            newEnDiplay.transform.parent = PopUp_Parent.transform;

            enemy.GetComponent<EnemyStateMachine>().enemy_ui = newEnDiplay.GetComponent<UI_en_Info>();
            //Debug.Log("gui manager trigger update en ui");
            // 
            Update_En_UI.TriggerEvent();
        }

    }

    public void Instantiate_PopUp_Event()
    {
        StartCoroutine(instantiateDamagePopUp());
    }

    public void Instantiate_Text_Event()
    {
        StartCoroutine(instantiateTextPopUp());
    }

    public IEnumerator instantiateDamagePopUp()
    {
        PopUp_Damage = UI_SO_DamagePopUp.Damage;
        PopUp_Spawn = UI_SO_DamagePopUp.TargetPos;
        Vector3 tranformrandom = new Vector3(Random.Range(1, 3), Random.Range(1, 3), 1);
        //GameObject newPopUP = Instantiate(PopUp_GO, PopUp_Spawn, false);
        GameObject newPopUP = Instantiate(PopUp_GO, (PopUp_Spawn.position + tranformrandom), PopUp_Spawn.rotation, PopUp_Spawn);
        newPopUP.transform.parent = PopUp_Parent.transform;
        newPopUP.GetComponent<TextMeshProUGUI>().text = PopUp_Damage.ToString();
        ////Navigation navigation = newButton1.GetComponent<Button
        ///
                //// Get UI_SO_DamagePopUp.AtkEffect;
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.BREAK)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().color = Color.yellow;
            //yield return new WaitForSeconds(PopUp_LifeTime/4);
            //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.white;

        }
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.CRITICAL)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().color = Color.yellow;
            //yield return new WaitForSeconds(PopUp_LifeTime / 4);
            //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.EFFECTIVE)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().color = Color.red;

            //yield return new WaitForSeconds(0.5f);
            //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.UNEFFECTIVE)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().color = Color.blue;
            //yield return new WaitForSeconds(0.5f);
            //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        yield return new WaitForSeconds(0f);

        float animDur = PopUp_LifeTime;
        //// PLAY ANIMATION MOVE
        newPopUP.transform.
        DOMove(new Vector3(newPopUP.transform.position.x + Random.Range(-1, 2),
        newPopUP.transform.position.y + Random.Range(1, 4),
        newPopUP.transform.position.z + Random.Range(-2, -2)),
        animDur).
        SetEase(Ease.InOutSine);
        // SCALE
        newPopUP.transform.
        DOScale(new Vector3(
        Random.Range(0.02f, 0.03f),
        Random.Range(0.02f, 0.03f),
        Random.Range(1, 1)),
        animDur).
        SetEase(Ease.OutElastic);
        //SetDelay(animDur / 3);

        // TEXT EFFECT  FADE IN 
        newPopUP.GetComponent<TextEffect>().ShowDuration = animDur;
        newPopUP.GetComponent<TextEffect>().enabled = true;

        Destroy(newPopUP, animDur);

    }

    public IEnumerator instantiateTextPopUp()
    {
        PopUp_Spawn = UI_SO_DamagePopUp.TargetPos;
        //PopUp_Type = 
        //UI_SO_DamagePopUp.AtkEffect;

        Vector3 tranformrandom = new Vector3(Random.Range(1, 3), Random.Range(1, 4), 1);
        GameObject newPopUP = Instantiate(PopUp_GO, (PopUp_Spawn.position + tranformrandom), PopUp_Spawn.rotation, PopUp_Spawn);
        newPopUP.transform.parent = PopUp_Parent.transform;

        //// Get UI_SO_DamagePopUp.AtkEffect;
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.BREAK)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().color = Color.yellow;
            newPopUP.GetComponent<TextMeshProUGUI>().text = "BREAK";
            //yield return new WaitForSeconds(0.5f);
            //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.CRITICAL)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().color = Color.yellow;
            newPopUP.GetComponent<TextMeshProUGUI>().text = "CRIT";
            //yield return new WaitForSeconds(0.5f);
            //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.EFFECTIVE)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().color = Color.red;
            newPopUP.GetComponent<TextMeshProUGUI>().text = "WEAK";
            //yield return new WaitForSeconds(0.5f);
            //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.MISS)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().color = Color.grey;
            newPopUP.GetComponent<TextMeshProUGUI>().text = "MISS";
            //yield return new WaitForSeconds(0.5f);
            //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.NORMAL)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().text = " ";
        }
        if (UI_SO_DamagePopUp.currentState == UI_SO_DamagePopUp.AtkEffect.UNEFFECTIVE)
        {
            newPopUP.GetComponent<TextMeshProUGUI>().color = Color.blue;
            newPopUP.GetComponent<TextMeshProUGUI>().text = "RESSIT";
            //yield return new WaitForSeconds(0.5f);
            //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        yield return new WaitForSeconds(0f);

        ////Navigation navigation = newButton1.GetComponent<Button
        float animDur = PopUp_LifeTime;
        //// PLAY ANIMATION MOVE
        newPopUP.transform.
        DOMove(new Vector3(newPopUP.transform.position.x + Random.Range(-1, 2),
        newPopUP.transform.position.y + Random.Range(1, 4),
        newPopUP.transform.position.z + Random.Range(-2, -2)),
        animDur).
        SetEase(Ease.InOutSine);
        // SCALE
        newPopUP.transform.
        DOScale(new Vector3(
        Random.Range(0.04f, 0.06f),
        Random.Range(0.04f, 0.06f),
        Random.Range(1, 1)),
        animDur);
        //SetEase(Ease.OutElastic).
        //SetDelay(animDur / 3);

        // TEXT EFFECT  FADE IN 
        newPopUP.GetComponent<TextEffect>().ShowDuration = animDur;
        newPopUP.GetComponent<TextEffect>().enabled = true;

        Destroy(newPopUP, animDur);

        //newPopUP.GetComponent<TextMeshProUGUI>().color = Color.Lerp(newPopUP.GetComponent<TextMeshProUGUI>().color, Color.clear, PopUp_LifeTime);
    }

    #endregion

    #region EndBattleCanvas
    public void OnClick_BattleIsWon()
    {

        foreach (GameObject hero in heroDisplay) { hero.SetActive(false); }
        HeroDiplay.SetActive(false);

        BackPanel.SetActive(true);
        HeroDiplay.SetActive(false);
        //Win_Screen.SetActive(true);
        //ButtonNext.GetComponent<Button>().onClick.RemoveAllListeners();
        //ButtonNext.GetComponent<Button>().onClick.AddListener(delegate { _ShowXpGain(); });

        if (iteration == 0)
        {
            Win_Screen.SetActive(true);
            StartCoroutine(BattleWon());
        }
        if (iteration == 1)
        {
            _ShowXpGain();
        }
        if (iteration == 2)
        {
            _ShowItem();
        }
        if (iteration == 3)
        {
            //yield break;
            _SkipEndBattleMenu();
        }

        //if (!isLevelingUP)
        //{
        //    _SkipEndBattleMenu();
        //}

        iteration++;

        // old version it was over time in a coroutine and it was b
        //StartCoroutine(BattleWon());    
    }

    public IEnumerator BattleWon()
    {
        Win_Bnt.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ButtonNext.SetActive(true);
    }

    public void BattleIsLost()
    {
        Loose_Screen.SetActive(true);
        Debug.Log("lose");
    }

    public void _ShowXpGain()
    {
        Win_Screen.SetActive(false);
        Win_HeroParty.SetActive(true);
        //Debug.Log("exppoint showed");
        XpManager.instance_XPM.XpEndBattle();

        foreach (Chara_Hero chara in hero_Party.HeroInParty_Data)
        {
            GameObject obj = Instantiate(HeroGui, Win_HeroParty.transform);
            GUI_Win_Hero_Panel data = obj.GetComponent<GUI_Win_Hero_Panel>();
            Chara_BaseStats hero = chara.Stats;

            data.SetUp(chara.leveling);
            data.SetUpPortrait(hero.general_Setting.HeroBust);
            data.SetUpText(chara.leveling);
            data.MoveSlider(chara.leveling);
        }
    }

    public void _ShowItem()
    {
        Win_TacticalBonus.SetActive(false);
        Win_HeroParty.SetActive(false);
        Win_Item.SetActive(true);

        for (int i = 0; i < XpManager.instance_XPM.spellDropped.Count; i++)
        {
            GameObject newImage = Instantiate(ItemDropGO, Win_Item.transform);
            newImage.GetComponentInChildren<spell_Btn>().Icon.sprite = XpManager.instance_XPM.spellDropped[i].GeneralSetting.icon;

            // Set the position of the UI image
            float yPositionOffset = i * 150;
            RectTransform rectTransform = newImage.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, yPositionOffset);

            newImage.transform.parent = Win_Item.transform.parent;
        }
        
        //panelTitle.text = "Item";
        //Debug.Log("item showed");
    }

    public void _SkipEndBattleMenu()
    {
        Loose_Screen.SetActive(false);
        BackPanel.SetActive(false);
        Win_Screen.SetActive(false);
        Win_Bnt.SetActive(false);
        Win_HeroParty.SetActive(false);
        Win_TacticalBonus.SetActive(false);
        Win_Item.SetActive(false);
        //Debug.Log("Skipped");
        //StartBattle.SetActive(true);
        Invoke("EndCombat", 0.01f);
    }
    #endregion 

    void EndCombat()
    {
        Launch_Battle.End_Combat();
    }


}







//set up Navigation new input system
// // get the Navigation data
// Navigation navigation = newButton1.GetComponent<Button>().navigation;
// // switch mode to Explicit to allow for custom assigned behavior
// navigation.mode = Navigation.Mode.Explicit;
// // highlight the Save button if the left arrow key is pressed
////navigation.selectOnLeft = alliesList[0];
//// Debug.Log(alliesList.Count);
// //navigation.selectOnRight = alliesList[alliesList.Count];
// // reassign the struct data to the button
// newButton1.GetComponent<Button>().navigation = navigation;