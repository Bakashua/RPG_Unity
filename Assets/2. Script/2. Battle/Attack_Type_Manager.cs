using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Attack_Type_Manager : MonoBehaviour
{
    /*
     flot type d'atp pwr + float type de faiblesse def
     quand atk random float D100 si inf type atk + type def = unbalance

    =i atk start link attack 
    coroutine avec choose atk / rush / all out ////

     */
    #region Data

    [Header("SCRIPTS")]

    private EnemyStateMachine ESM;


    [Header("LINK ATTACK")]

    public float bravourePoint;

    [Header("BASIC STATS")]

    public float atk_Slash;
    public float atk_Pierce;
    public float atk_Bash;
    public float vuln_Slash;
    public float vuln_Pierce;
    public float vuln_Bash;  
    
    [Header("ATTACKS")]

    public BaseAttack Link_ATK;
    public BaseAttack Link_RUSH;
    public BaseAttack Link_ALLOUT;

    [Header("BOOL")]

    public bool isLinkAttack;
    public bool isSlash;
    public bool isPierce;
    public bool isBash;
    

    [Header("INTERFACE")]

    public GameObject ui_all;
    public GameObject ui_background_top;
    public GameObject ui_background_down;
    public GameObject ui_background_text;
    public GameObject ui_atkButn;
    public GameObject ui_rushButn;
    public GameObject ui_allButn;

    // EXTRA ///
    public GameObject ui_triggerSlash;
    public GameObject ui_triggerPierce;
    public GameObject ui_triggerBash;
    public GameObject ui_linkAtk;
    public GameObject ui_linkRush;
    public GameObject ui_linkAll;



    #endregion

    public IEnumerator RoolLinkAttack(List<GameObject> target) //List<GameObject>  IEnumerator
    {
        float r = Random.Range(1, 100);
       //Debug.Log("zkjjdhkajh     " + r);
        
       //         Debug.Log("roollinkatk SLASH");
        foreach (GameObject enemy in target)
        {
            
        if (r <= atk_Slash + enemy.GetComponent<Attack_Type_Manager>().vuln_Slash)
        {
            ui_triggerSlash.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            ui_triggerSlash.SetActive(false);

            isSlash = true;  StartCoroutine(CreateUIForLink());
            yield break;
        }

        else if (r <= atk_Pierce + enemy.GetComponent<Attack_Type_Manager>().vuln_Pierce)
        {
            ui_triggerPierce.SetActive(true);         
            yield return new WaitForSecondsRealtime(0.5f);
            ui_triggerPierce.SetActive(false);  
                
            isPierce = true;  StartCoroutine(CreateUIForLink());
            yield break;
        }

        else if (r <= atk_Bash + enemy.GetComponent<Attack_Type_Manager>().vuln_Bash)
        {
            ui_triggerBash.SetActive(true);           
            yield return new WaitForSecondsRealtime(0.5f);
            ui_triggerBash.SetActive(false);

            isBash = true;  StartCoroutine(CreateUIForLink());
            yield break;
        }
            //yield return null;
    }
       // Debug.Log("RoolLinkAttack works");
    }

    public IEnumerator CreateUIForLink()
    {
        Time.timeScale = 0;
        //Debug.Log("CreateUIForLink works");

        ui_atkButn.GetComponentInChildren<Button>().onClick.AddListener(() => { LinkAttack(); });
        ui_rushButn.GetComponentInChildren<Button>().onClick.AddListener(() => { LinkRush(); });
        ui_allButn.GetComponentInChildren<Button>().onClick.AddListener(() => { LinkAllOut(); });

        ui_all.SetActive(true);
        ui_background_top.SetActive(true);
        ui_background_down.SetActive(true);
        ui_background_text.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        // ALL ANIME UI IN
        //ui_background_top.transform.DOMoveX(moveDistLR, moveDurationLR).SetLoops(numberOfLoopLR, LoopType.Yoyo).SetEase(Ease.OutSine);
        //ui_background_down.transform.DOMoveX(moveDistLR, moveDurationLR).SetLoops(numberOfLoopLR, LoopType.Yoyo).SetEase(Ease.OutSine);
        //ui_atkButn.transform.DOMoveX(moveDistLR, moveDurationLR).SetLoops(numberOfLoopLR, LoopType.Yoyo).SetEase(Ease.OutSine);
        //ui_rushButn.transform.DOMoveX(moveDistLR, moveDurationLR).SetLoops(numberOfLoopLR, LoopType.Yoyo).SetEase(Ease.OutSine);
        //ui_allButn.transform.DOMoveX(moveDistLR, moveDurationLR).SetLoops(numberOfLoopLR, LoopType.Yoyo).SetEase(Ease.OutSine);

        ui_atkButn.SetActive(true);

        if (2 <= bravourePoint)
        {
        ui_rushButn.SetActive(true);
        }
        if (5 <= bravourePoint)
        {
        ui_rushButn.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(3f);
        DesactivateLinkUI();
    }

    public void LinkAttack()
    {
        //Link_ATK = gameObject.GetComponent<BaseClass>().linkAttack[0];
        Debug.Log("LINK ATTACK");
        DesactivateLinkUI();

    }

    public void LinkRush()
    {
       //Link_RUSH = gameObject.GetComponent<BaseClass>().linkRush[0];
        Debug.Log("LINK RUSH");
        DesactivateLinkUI();

    }

    public void LinkAllOut()
    {
        //Link_ALLOUT = gameObject.GetComponent<BaseClass>().linkAllOut[0];
        Debug.Log("LINK ALL OUT");
        DesactivateLinkUI();

    }

    public void DesactivateLinkUI() 
    {
        ui_all.SetActive(false);

        //anim out

        //fix time
        Time.timeScale = 1;
    }



}
