using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class BattleCamManager : MonoBehaviour
{
    public static BattleCamManager instance_BCam;

    //[Header("Event")]
    //public GameEvent Cam_Neutre;
    //public GameEvent Cam_Hero;
    //public GameEvent Cam_En;

    [Header("General")]
    public GameObject Cam;
    //public Cinemachine.CinemachineVirtualCamera Cam; 
    public Cinemachine.CinemachineDollyCart Cart;

    [Header("Intro")]
    public GameObject Tracker;
    public Cinemachine.CinemachineDollyCart DollyIntro;

    [Header("Player")]
    bool isTilted = false;


    [Header("Tactical")]
    public GameObject Cam_Tactic_go;
    public Cinemachine.CinemachineVirtualCamera Cam_Tactic;
    public GameObject Target;

    [Header("Neutral")]
    public Cinemachine.CinemachineSmoothPath startPath;
    public Cinemachine.CinemachineSmoothPath[] alternativePath;

    [Header("Enemy")]
    public Cinemachine.CinemachineSmoothPath En_startPath;
    public Cinemachine.CinemachineSmoothPath[] En_alternativePath;

    bool isOn;

    #region scene test01
    //public GameObject mainBattleCAM;
    //public GameObject winBattleCAM;
    //public GameObject levelUpBattleCAM;
    #endregion

    private void Awake()
    {
        if (instance_BCam != null && instance_BCam != this)
        {
            Destroy(this);
        }
        else
        {
            instance_BCam = this;
        }
    }

    // cam neutreal + path et alt path
    // cam PLayer / go to cam play + select enemy + switch cam tactic
    // cam enemy + path

    #region NeutralCam

    public void Listener_CombatIntroCam()
    {
        Tracker.SetActive(true);
        DollyIntro.enabled = true;
    }

    public void SetCamNeutral()
    {
        // Set up neutral Cam
        Cam.SetActive(true);

        StopAllCoroutines();
        Cart.m_Path = startPath;
        StartCoroutine(ChangeTrack());
    }

    IEnumerator ChangeTrack()
    {
        yield return new WaitForSeconds(Random.Range(4, 6));

        var path = alternativePath[Random.Range(0, alternativePath.Length)];
        Cart.m_Path = path;

        StartCoroutine(ChangeTrack());
    }

    #endregion

    #region Player Turn

    // get camera of active player and make it tilt when using spells
    // dans Player_Action_Manager
    public void PlayerChooseSpell(float f)
    {
        //// Reset setting of player Cam
        CinemachineComposer pCam = BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
        pCam.m_HorizontalDamping = 0.5f;
        pCam.m_DeadZoneWidth = 0f;
        pCam.m_DeadZoneHeight = 0f;
        pCam.m_SoftZoneWidth = 0f;
        pCam.m_SoftZoneHeight = 0f;


        Vector3 origin = BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.position;
        Vector3 Zoom = new Vector3(-3, 1, 0);
        float tiltForce = f / 3;
        //Debug.Log("qqqqqqq");
        //Debug.Log(BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3));
        //Cam.transform.DOMoveX(f, 0.5f);

        if (!isTilted)
        {
            //Debug.Log("00000");
            BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMoveZ(tiltForce, 0.5f); //.SetEase(Ease.InOutSine);
            BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOLocalMoveX(2f, 0.5f); //.SetEase(Ease.InOutSine);
            //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().m_Lens.FieldOfView = 30f;
            //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMoveY(1, 0.5f); //.SetEase(Ease.InOutSine);
            //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMove(new Vector3(origin.x - 2, 2, origin.z), 0.5f); //.SetEase(Ease.InOutSine);
            isTilted = true;
        }
        else
        {
            //Debug.Log("11111");
            //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().m_Lens.FieldOfView = 52.16f;
            //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMoveX(-4, 0.5f); //.SetEase(Ease.InOutSine);
            BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMove(new Vector3(0, 0, 0) + BattleStateMachine.instance_BSM.herosToManage[0].transform.position, 0.5f); //.SetEase(Ease.InOutSine);
            //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMoveY(0, 0.5f); //.SetEase(Ease.InOutSine);
            //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMove(-Zoom, 0.5f).SetEase(Ease.InOutSine);
            //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMove(new Vector3(origin.x + 2, -1, origin.z), 0.5f); //.SetEase(Ease.InOutSine);
            isTilted = false;
            //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.position = new Vector3(0,0,0) + BattleStateMachine.instance_BSM.herosToManage[0].transform.position;
        }
    }

    public void PlayerChooseTarget()
    {
        //// change setting of player Cam
        CinemachineComposer pCam = BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
        pCam.m_HorizontalDamping = 1f;
        pCam.m_DeadZoneWidth = 0.3f;
        pCam.m_DeadZoneHeight = 0.3f;
        pCam.m_SoftZoneWidth = 0.6f;
        pCam.m_SoftZoneHeight = 0.6f;


        //
        BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).GetComponentInChildren<CinemachineVirtualCamera>().LookAt = BattleStateMachine.instance_BSM.enemyInBattle[0].transform;
        //Vector3 targetPos = new Vector3(-1.5f, -0.5f, -1.95f);
        Vector3 targetPos = new Vector3(-1f, 1f, 0);
        BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOLocalMove(targetPos, 0.3f);
    }

    public void ResetPlayerCamera()
    {
        //// Reset setting of player Cam
        CinemachineComposer pCam = BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
        pCam.m_HorizontalDamping = 0.5f;
        pCam.m_DeadZoneWidth = 0f;
        pCam.m_DeadZoneHeight = 0f;
        pCam.m_SoftZoneWidth = 0f;
        pCam.m_SoftZoneHeight = 0f;

        //BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMove(new Vector3(0, 0, 0) + BattleStateMachine.instance_BSM.herosToManage[0].transform.position, 0.5f);
        BattleStateMachine.instance_BSM.herosToManage[0].transform.GetChild(3).transform.DOMove(BattleStateMachine.instance_BSM.herosToManage[0].transform.position, 0.25f);

    }

    #endregion

    #region Tactical_Cam

    public void Setup_TacticCam()
    {
        if (isOn)
        { Cam_Tactic_go.SetActive(false); isOn = false; return; }

        if (!isOn)
        {
            Cam_Tactic_go.SetActive(true); isOn = true;
            Battle_GUI_Manager.instance_BUIM.targetButton_Cancel.SetActive(true);
        }

    }

    public void Activate_TacticalCam()
    {
        Cam_Tactic_go.SetActive(true);
    }

    public void Desactivate_TacticalCam()
    {
        Cam_Tactic_go.SetActive(false);
    }


    // if we want the tactical cam to focus on selected enemy
    public void Target_Enemy(GameObject en)
    {
        //Cam_Tactic.m_LookAt = en.transform;
    }

    #endregion

    #region En_Cam
    public void SetCamEnemy()
    {
        // Set up neutral Cam
        Cam.SetActive(true);

        StopAllCoroutines();
        Cart.m_Path = En_startPath;
        StartCoroutine(ChangeTrack());
    }

    IEnumerator ChangeTrackEnemy()
    {
        yield return new WaitForSeconds(Random.Range(4, 6));

        var path = En_alternativePath[Random.Range(0, En_alternativePath.Length)];
        Cart.m_Path = path;

        StartCoroutine(ChangeTrack());
    }

    // cam qd en atk; zoomsur target he 


    #endregion



    #region scene test01
    //public void ActivateMainBattleCam()
    //{
    //    mainBattleCAM.SetActive(true);
    //    winBattleCAM.SetActive(false);
    //    levelUpBattleCAM.SetActive(false);
    //}
    //public void ActivateWinBattleCam()
    //{
    //    mainBattleCAM.SetActive(false);
    //    winBattleCAM.SetActive(true);
    //    levelUpBattleCAM.SetActive(false);
    //}
    //public void ActivateLooseCam()
    //{
    //    mainBattleCAM.SetActive(false);
    //    winBattleCAM.SetActive(false);
    //    levelUpBattleCAM.SetActive(false);
    //}
    //public void ActivateLevelUpCam()
    //{
    //    mainBattleCAM.SetActive(false);
    //    winBattleCAM.SetActive(false);
    //    levelUpBattleCAM.SetActive(true);
    //}
    #endregion
}
