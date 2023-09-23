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
    public GameObject[] monster;
    public List<Chara_BaseStats> EnemyList;
    public int initialMonsterSpawn_min;
    public int initialMonsterSpawn_max;
    public int maximumOfMonster = 3;


    //public GameObject[] hero;


    [Header("SPAWN POINT")]
    public List<Transform> monsterSpawnPoint;
    public List<Transform> heroSpawnPoint;
    private List<GameObject> instanciatedMonster;
    //private List<GameObject> instanciatedHero;
    private int numberOfMonster;
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
        //battleCamManager = GameObject.Find("BattleCamManager").GetComponent<BattleCamManager>();
        //ClickCreateMonster();
        Invoke("ClickCreateMonster", StartingDelay);
    }

    // set up battle start when click on btn
    public void ClickCreateMonster()
    {

        StartSoundEffect.Play();

        instanciatedMonster = new List<GameObject>();
        int spawnnbr = Random.Range(initialMonsterSpawn_min, initialMonsterSpawn_max + 1);
        for (int i = 0; i < spawnnbr; i++)
        {
            AddMonster();
        }
        InstantiateHero();
        startBattleButton.SetActive(false);
        BSM.SetUpList();

        //set up cam for battle
        //battleCamManager.ActivateMainBattleCam();
        Trigger_Battle();
    }

    void Trigger_Battle()
    {
        // CALL EVENT START
        onBattleStart.TriggerEvent();
    }

    private void AddMonster()
    {
        // create a new enemy and give him his SO data
        GameObject newChunkPrefab = RandomMonster();
        newChunkPrefab.GetComponent<EnemyStateMachine>().enemy = RandomMonsterStats();
        newChunkPrefab.GetComponent<Combat_Movement>().LDM = LDM;


        // Compute random number for monster spawnpoint OR monster have preference spawn point / random spawn
        int randomSpawnPoint = Random.Range(0, monsterSpawnPoint.Count);

        if (monsterSpawnPoint[randomSpawnPoint].gameObject.GetComponent<Combat_Tiles>().currentHolder != null)
        {
            randomSpawnPoint = randomSpawnPoint + 1;
        }

        newMonster = Instantiate(newChunkPrefab, monsterSpawnPoint[randomSpawnPoint].position, Quaternion.identity, transform);
        newMonster.transform.parent = Parent.transform;
        instanciatedMonster.Add(newMonster);
        numberOfMonster++;

        if (numberOfMonster == maximumOfMonster)
        {
            RemoveFirstModule();
        }
    }

    // Choose a monster from list of monster that would be unstantiated
    private GameObject RandomMonster()
    {
        int randomIndex = Random.Range(0, monster.Length);
        GameObject randomChunk = monster[randomIndex];
        return randomChunk;
    }

    private Chara_BaseStats RandomMonsterStats()
    {
        int randomIndex = Random.Range(0, EnemyList.Count);
        Chara_BaseStats randomStats = EnemyList[randomIndex];
        //Debug.Log("randomStats stats = " + randomStats);

        Chara_BaseStats InstanceStats = ScriptableObject.Instantiate(randomStats);
        //Debug.Log("monster stats = " + InstanceStats);
        return InstanceStats;
    }

    void RemoveFirstModule()
    {
        GameObject firstModule = instanciatedMonster[0];
        Destroy(firstModule);

        instanciatedMonster.RemoveAt(0);
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
