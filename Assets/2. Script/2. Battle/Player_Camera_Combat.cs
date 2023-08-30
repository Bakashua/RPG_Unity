using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player_Camera_Combat : MonoBehaviour
{
    //[HideInInspector] public static Player_Camera_Combat instance;

    [Header("Hero")]
    public GameObject CamPlayer_go;
    public Cinemachine.CinemachineVirtualCamera CamPlayer;
    public Cinemachine.CinemachineVirtualCamera CamPlayerFront;
    public GameObject GO_Player;
    [SerializeField] GameObject GO_Target;
    //bool isTactical;
    // zoom on attack


    private void Awake()
    {
        //if (instance != null && instance != this)
        //{ Destroy(this); }
        //else
        //{ instance = this; }
        //Debug.Log(instance_BUIM);
    }


    #region He_Cam
    // set up cam + focus target +activate player cam + acti tactical
    public void Setup_PlayerCam()
    {
        BattleCamManager.instance_BCam.ResetPlayerCamera();
        //CamPlayer.transform.DOMove(new Vector3(0f, 0f, 0f), 0f);
        //CamPlayer_go.transform.DOMove(new Vector3(0f, 0f, 0f), 0f);
        CamPlayer_go.transform.DOMove(transform.position, 0f);
        CamPlayer_go.SetActive(true);
        //CamPlayer_go.transform.position = new Vector3(0,0,0);
        CamPlayer.transform.DOLocalMove(new Vector3(-4f, 2f, -0.5f), 0f);
        //CamTactic.SetActive(false);
    }

    public void Setup_TacticCam()
    {
        CamPlayer_go.SetActive(false);
        //CamTactic.SetActive(true);
    }

    public void Desactivate_cam()
    {
        CamPlayer_go.SetActive(false);
        CamPlayer.m_LookAt = GO_Player.transform;
        //CamTactic.SetActive(false);
    }

    public void Target_Enemy(GameObject en)
    {
        GO_Target = en;
        CamPlayer.m_LookAt = en.transform;
    }


    #region Hero Action

    public void Do_Action()
    {
        StartCoroutine(PlayerAct());
    }

    IEnumerator PlayerAct()
    {
        float r = Random.Range(1, 100);

        // show chara doing atk before playing move animation
        if (BattleStateMachine.instance_BSM.performList[0].choosenAttack.Enumsetting.attackCategory != AttackCategory.basic)
        {
            // r is how likely we will see the chara
            if (r >= 75)
            {
                LookAt_He();
                yield return new WaitForSeconds(0.5f);
            }
        }

        yield return new WaitForSeconds(0.2f);
        Follow_He();

    }

    void LookAt_He()
    {
        //CamPlayer.m_LookAt = GO_Player.transform;
        CamPlayerFront.gameObject.SetActive(true);
        CamPlayer.transform.DOLocalMove(new Vector3(-5.85f, 2f, -0.5f), 0f);
        //CamPlayer.transform.DOLocalMoveX(1, 0f);
    }

    void Follow_He()
    {
        CamPlayerFront.gameObject.SetActive(false);
        //CamPlayer.transform.DOLocalMoveX(-1, 0f);

        CamPlayer.m_LookAt = GO_Target.transform;

    }

    void Behind_He()
    {

    }

    void Behind_Target()
    {

    }


    #endregion

    #endregion


}
