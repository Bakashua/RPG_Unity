using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// maybe some extra edge case on set up spell
public class Player_UI_ActionBTN : MonoBehaviour
{
    // use to populate all action btn image w character spells icon
    // show spe1 n spe 2
    // Start is called before the first frame update
    public Chara_SpellList SpellList;

    public GameObject spellBtn;
    public GameObject normal;
    public GameObject spe1;
    public GameObject spe2;

    // set up spell in order north west est south for spells in 01 then in 02
    public List<RectTransform> btnLocation = new List<RectTransform>();

    public List<spell_Btn> ListNormal = new List<spell_Btn>();
    public List<spell_Btn> ListSpe01 = new List<spell_Btn>();
    public List<spell_Btn> ListSpe02 = new List<spell_Btn>();


    [Header("AUDIO")]
    public AudioSource PlayAudio;
    public AudioClip Open;
    public AudioClip Close;


    private void OnEnable()
    {
        //Debug.Log("_____ aaaa  _____  " + btnLocation[0].position);
    }

    public void ShowSpe1()
    {
        //Debug.Log("_____ aaaa  _____  " + btnLocation[0].position);
        if (spe1 != null)
        {
            PlayAudio.PlayOneShot(Open);
            spe1.SetActive(true);
        }
    }

    public void ShowSpe2()
    {
        if (spe2 != null)
        {
            PlayAudio.PlayOneShot(Open);
            spe2.SetActive(true);
        }
        //normal.SetActive(false);
        //spe1.SetActive(false);
    }

    public void ShowSpeCancel()
    {
        PlayAudio.PlayOneShot(Close);
        normal.SetActive(true);
        spe1.SetActive(false);
        spe2.SetActive(false);
    }


    public void SetUpSpellIcon(Chara_BaseStats chara)
    {
        SetUpSpell(chara);
    }


    void SetUpSpell(Chara_BaseStats chara)
    {
        int i = 0;
        foreach (BaseAttack obj in SpellList.Set01)
        {
            if (obj != null)
            {
                GameObject spell = Instantiate(spellBtn, btnLocation[i], false);
                //spell.transform.parent = spe1.transform;
                spell.transform.SetParent(spe1.transform);
                spell.GetComponent<spell_Btn>().Icon.sprite = obj.GeneralSetting.icon;

                float cost = 0;
                #region setup panel
                if (obj.Enumsetting.attackCategory == AttackCategory.magic)
                {
                    spell.GetComponent<spell_Btn>().Panel.GetComponent<Image>().color = new Color(0.12f, 0.38f, 0.87f, 1f);
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().text = obj.BaseAttackdata.attackMPCost.ToString();
                    cost = obj.BaseAttackdata.attackMPCost;
                }
                else if (obj.Enumsetting.attackCategory == AttackCategory.tech)
                {
                    spell.GetComponent<spell_Btn>().Panel.GetComponent<Image>().color = new Color(0.09f, 0.754f, 0.3f, 1f);
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().text = obj.BaseAttackdata.attackTPCost.ToString();
                    cost = obj.BaseAttackdata.attackTPCost;
                }
                else
                {
                    Debug.Log("spawned spell type not set");
                    spell.GetComponent<spell_Btn>().Panel.GetComponent<Image>().color = Color.black;
                }
                #endregion

                #region when you cannot use it
                // si no cost on le met en red
                if (chara.life_Stats.currentMP < cost)
                {
                    spell.GetComponent<spell_Btn>().Icon.color = Color.grey;
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
                }

                //// si cooldown on active le cooldown
                //Debug.Log(obj.name + "____" + obj.CurrentCooldown);
                if (obj.BaseAttackdata.CurrentCooldown > 0)
                {
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
                    spell.GetComponent<spell_Btn>().Cooldown.SetActive(true);
                    float f = obj.BaseAttackdata.CurrentCooldown / obj.BaseAttackdata.MaxCooldown;
                    spell.GetComponent<spell_Btn>().Cooldown.GetComponent<Image>().fillAmount = f; 
                    spell.GetComponent<spell_Btn>().Cooldown_Text.text = obj.BaseAttackdata.CurrentCooldown.ToString();
                }
                else { spell.GetComponent<spell_Btn>().Cooldown.SetActive(false); }
                #endregion

                i++;
                ListSpe01.Add(spell.GetComponent<spell_Btn>());
            }
            else
            {
                //Debug.Log("there is no speel");
            }
        }
        i = 0;



        foreach (BaseAttack obj in SpellList.Set02)
        {
            if (obj != null)
            {
                GameObject spell = Instantiate(spellBtn, btnLocation[i], false);
                //spell.transform.parent = spe2.transform;
                spell.transform.SetParent(spe2.transform);

                spell.GetComponent<spell_Btn>().Icon.sprite = obj.GeneralSetting.icon;
                spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().text = obj.BaseAttackdata.attackMPCost.ToString();

                float cost = 0;
                #region setup panel
                if (obj.Enumsetting.attackCategory == AttackCategory.magic)
                {
                    spell.GetComponent<spell_Btn>().Panel.GetComponent<Image>().color = new Color(0.12f, 0.38f, 0.87f, 1f);
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().text = obj.BaseAttackdata.attackMPCost.ToString();
                    cost = obj.BaseAttackdata.attackMPCost;
                }
                else if (obj.Enumsetting.attackCategory == AttackCategory.tech)
                {
                    spell.GetComponent<spell_Btn>().Panel.GetComponent<Image>().color = new Color(0.09f, 0.754f, 0.3f, 1f);
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().text = obj.BaseAttackdata.attackTPCost.ToString();
                    cost = obj.BaseAttackdata.attackTPCost;
                }
                else
                {
                    Debug.Log("DEBUG : spawned spell type not set");
                    spell.GetComponent<spell_Btn>().Panel.GetComponent<Image>().color = Color.black;
                }
                #endregion

                #region when you cannot use it
                // si no cost on le met en red
                if (chara.life_Stats.currentMP < cost)
                {
                    spell.GetComponent<spell_Btn>().Icon.color = Color.grey;
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
                }

                if (chara.life_Stats.currentTP < cost)
                {
                    spell.GetComponent<spell_Btn>().Icon.color = Color.grey;
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
                }

                //// si cooldown on active le cooldown
                //Debug.Log(obj.name + "____" + obj.CurrentCooldown);
                if (obj.BaseAttackdata.CurrentCooldown > 0)
                {
                    spell.GetComponent<spell_Btn>().cost.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
                    spell.GetComponent<spell_Btn>().Cooldown.SetActive(true);
                    float f = obj.BaseAttackdata.CurrentCooldown / obj.BaseAttackdata.MaxCooldown;
                    spell.GetComponent<spell_Btn>().Cooldown.GetComponent<Image>().fillAmount = f;
                    spell.GetComponent<spell_Btn>().Cooldown_Text.text = obj.BaseAttackdata.CurrentCooldown.ToString();
                }
                else { spell.GetComponent<spell_Btn>().Cooldown.SetActive(false); }
                #endregion

                i++;
                ListSpe02.Add(spell.GetComponent<spell_Btn>());
            }
            else
            {
                //Debug.Log("there is no speel");
            }
        }
        i = 0;

    }
}


