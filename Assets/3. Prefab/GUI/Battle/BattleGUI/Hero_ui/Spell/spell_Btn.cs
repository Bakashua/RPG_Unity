using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spell_Btn : MonoBehaviour
{
    public BaseAttack spell;
    public Image Icon;
    public Image Panel;
    public GameObject ActiveAction;
    public TextMeshProUGUI cost;

    public GameObject Cooldown;
    public TextMeshProUGUI Cooldown_Text;



    //public void SetUp()
    //{
        
    //}


    public void IsActivated()
    {
        //StartCoroutine(playAnim());
    }

    IEnumerator playAnim()
    {
        ActiveAction.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        ActiveAction.SetActive(false);
        //yield return new WaitForSeconds(0.2f);
    }




        public void CantUse()
    {
        Icon.GetComponent<Image>().color = Color.red;
        Panel.GetComponent<Image>().color = new Color(0.65f, 0.654f, 0.65f, 1f);
    }

}
