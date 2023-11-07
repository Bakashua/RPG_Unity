using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Header("CLASS")]
    BattleStateMachine BSM;
    public Hero_Party hero_Party;
    public LevelDesignManager LDM;
    // private BattleCamManager battleCamManager;

    [Header("EVENT")]
    public float StartingDelay;
    public GameEvent onBattleStart;


    [Header("Instantiate Monster Data")]
    public GameObject Parent;
    public GameObject monsterGO;
    public SO_Encounter EncounterGroup;
    //public List<Chara_BaseStats> EnemyList;
    //public int initialMonsterSpawn_min;
    //public int initialMonsterSpawn_max;
    //public int maximumOfMonster = 3;


    //public GameObject[] hero;


    [Header("SPAWN POINT")]
    public List<Transform> monsterSpawnPoint;
    public List<Transform> heroSpawnPoint;
    //private List<GameObject> instanciatedMonster;
    //private List<GameObject> instanciatedHero;
    //private int numberOfMonster;
    private int numberOfHero;
    private GameObject newMonster;


    [Header("UI To Delete")]
    public GameObject startBattleButton;

    [Header("UI")]
    public GameObject TileBtn;
    public GameObject BtnTileParent;

    [Header("AUDIO")]
    public AudioSource StartSoundEffect;


    void Start()
    {
        BSM = BattleStateMachine.instance_BSM;

        if(EncounterGroup == null)
        {
        Launch_Battle node = FindObjectOfType<Launch_Battle>();
            EncounterGroup = node.Encounter;
        }
        //print(LB + "wwwwwwwwwwwwwww");
        //battleCamManager = GameObject.Find("BattleCamManager").GetComponent<BattleCamManager>();
        //ClickCreateMonster();
        Invoke("ClickCreateMonster", StartingDelay);
    }

    // set up battle start when click on btn
    public void ClickCreateMonster()
    {

        StartSoundEffect.Play();

        //instanciatedMonster = new List<GameObject>();

        // !!! ici on le fait a partir de encounter
        //int spawnnbr = Random.Range(initialMonsterSpawn_min, initialMonsterSpawn_max + 1);
        for (int i = 0; i < EncounterGroup.encounter.Count; i++)
        {
            //print(EncounterGroup.encounter[i] + " TTTTTTTTTTTTTTTTTTTTTTTTT " + i);
            // les monster prennent les stats de l encounter
            AddMonster(EncounterGroup.encounter[i]);
        }

        //set up cam for battle
        //battleCamManager.ActivateMainBattleCam();
        Invoke("Trigger_Battle", 0.1f);
        //Trigger_Battle();
    }

    void Trigger_Battle()
    {
        InstantiateHero();
        startBattleButton.SetActive(false);
        BSM.SetUpList();

        // CALL EVENT START
        onBattleStart.TriggerEvent();
    }

    private void AddMonster(Monster obj)
    {
        // create a new enemy and give him his SO data
        GameObject newChunkPrefab = monsterGO;
        newChunkPrefab.GetComponent<EnemyStateMachine>().enemy = ScriptableObject.Instantiate(obj.Enemy);
        //
        newChunkPrefab.GetComponent<Combat_Movement>().LDM = LDM;


        // Compute random number for monster spawnpoint OR monster have preference spawn point / random spawn
        int randomSpawnPoint = Random.Range(obj.PosA, obj.PosB);

        newMonster = Instantiate(newChunkPrefab, monsterSpawnPoint[randomSpawnPoint].position, Quaternion.identity, transform);
        newMonster.transform.parent = Parent.transform;
        //instanciatedMonster.Add(newMonster);
        //numberOfMonster++;

        //if (numberOfMonster == maximumOfMonster)
        //{
        //    RemoveFirstModule();
        //}
    }

    // Choose a monster from list of monster that would be unstantiated
    //private GameObject RandomMonster()
    //{
    //    //int randomIndex = Random.Range(0, monster.Length);
    //    //GameObject randomChunk = monster[randomIndex];
    //    //return randomChunk;
    //}

    //private Chara_BaseStats RandomMonsterStats()
    //{
    //    int randomIndex = Random.Range(0, EnemyList.Count);
    //    Chara_BaseStats randomStats = EnemyList[randomIndex];
    //    //Debug.Log("randomStats stats = " + randomStats);

    //    Chara_BaseStats InstanceStats = ScriptableObject.Instantiate(randomStats);
    //    //Debug.Log("monster stats = " + InstanceStats);
    //    return InstanceStats;
    //}

    void RemoveFirstModule()
    {
        //GameObject firstModule = instanciatedMonster[0];
        //Destroy(firstModule);
        //instanciatedMonster.RemoveAt(0);
    }

    public void InstantiateHero()
    {
        for (int i = 0; i < hero_Party.HeroInParty_Go.Count; i++)
        {            // !!! need changes !!!
            GameObject newHero = hero_Party.HeroInParty_Go[i];

            newHero.GetComponent<HeroStateMachine>().hero.general_Setting.CharaHero.SpellList.initialisation();
            newHero.GetComponent<HeroStateMachine>().hero = hero_Party.HeroInParty_Data[i].Stats;
            newHero.GetComponent<Combat_Movement>().LDM = LDM;
            newHero.GetComponent<Combat_Movement>().TileBtn = TileBtn;
            newHero.GetComponent<Combat_Movement>().BtnParent = BtnTileParent;

            Vector3 SP = heroSpawnPoint[hero_Party.HeroInParty_Go[i].GetComponent<HeroStateMachine>().hero.general_Setting.favoritePosition].position;

            GameObject instantiatedHero = Instantiate(newHero, SP, Quaternion.identity, transform);


            instantiatedHero.transform.parent = Parent.transform;
            // chaque nouveau hero = nouveau perso in party +1
            instantiatedHero.GetComponent<HeroStateMachine>().hero = hero_Party.HeroInParty_Data[i].Stats;
            instantiatedHero.SetActive(true);
            //instanciatedHero.Add(instantiatedHero);

        }
        //foreach (GameObject newhero in hero_Party.HeroInParty_Go)
        //{

        //}
    }
}


// raycast sur chaque spawn point
// 
