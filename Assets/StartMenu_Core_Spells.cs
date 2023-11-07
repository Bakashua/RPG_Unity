using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StartMenu_Core_Spells : MonoBehaviour
{
    [SerializeField] Hero_Party hp;

    [Header("SPELL TRAIT")]

    [SerializeField] GameObject TraitPanel;
    [SerializeField] GameObject TraitIcon;


    [Header("SPELL INVENTORY")]

    [SerializeField] GameObject SpellItem;
    [SerializeField] GameObject Parent;
    [SerializeField] GameObject SpellInventory;
    [SerializeField] Tween AnimationSelected;


    [Header("SPELL HERO")]

    [SerializeField] TextMeshProUGUI TextDescription;

    [SerializeField] Sprite noSpell;
    [SerializeField] spell_Btn SpellSelected;
    [SerializeField] List<spell_Btn> ListNormal = new List<spell_Btn>();
    [SerializeField] List<spell_Btn> ListSpe01 = new List<spell_Btn>();
    //[SerializeField] List<Image> ListSpe01Panel = new List<Image>();
    [SerializeField] List<spell_Btn> ListSpe02 = new List<spell_Btn>();
    //[SerializeField] List<Image> ListSpe02Panel = new List<Image>();

    private void Start()
    {
        SetUpSpell(hp.HeroInParty_Data[0].SpellList);
        SpellTrait(hp.HeroInParty_Data[0].SpellList);
        PopulateSpellInventory(hp.HeroInParty_Data[0].Inventory);
    }

    void SetUpSpell(Chara_SpellList SpellList)
    {
        ListNormal[0].spell = SpellList.attack;
        //ListNormal[0].Icon.sprite = SpellList.attack.GeneralSetting.icon;
        ListNormal[1].spell = SpellList.defense;
        //ListNormal[1].Icon.sprite = SpellList.defense.GeneralSetting.icon;


        for (int i = 0; i < ListSpe02.Count; i++)
        {
            //print(i);
            if (i < SpellList.O_Set01.Count && SpellList.O_Set01[i] != null)
            {
                ListSpe02[i].Icon.sprite = SpellList.O_Set01[i].GeneralSetting.icon;
                ListSpe02[i].spell = SpellList.O_Set01[i];

                #region set up panel
                if (SpellList.O_Set02[i].Enumsetting.attackCategory == AttackCategory.magic)
                {
                    ListSpe02[i].Panel.GetComponent<Image>().color = new Color(0.12f, 0.38f, 0.87f, 1f);
                    ListSpe02[i].cost.GetComponent<TMPro.TextMeshProUGUI>().text = SpellList.O_Set02[i].BaseAttackdata.attackMPCost.ToString();
                }

                if (SpellList.O_Set02[i].Enumsetting.attackCategory == AttackCategory.tech)
                {
                    ListSpe02[i].Panel.GetComponent<Image>().color = new Color(0.09f, 0.754f, 0.3f, 1f);
                    ListSpe02[i].cost.GetComponent<TMPro.TextMeshProUGUI>().text = SpellList.O_Set02[i].BaseAttackdata.attackMPCost.ToString();
                }
                #endregion

            }
            else
            {
                ListSpe02[i].Icon.sprite = noSpell;
            }
        }


        for (int i = 0; i < ListSpe01.Count; i++)
        {
            //print(i);
            if (i < SpellList.O_Set02.Count && SpellList.O_Set02[i] != null)
            {
                //BaseAttack spell = SpellList.O_Set02[i];
                ListSpe01[i].Icon.sprite = SpellList.O_Set02[i].GeneralSetting.icon;
                ListSpe01[i].spell = SpellList.O_Set02[i];

                #region set up panel
                if (SpellList.O_Set02[i].Enumsetting.attackCategory == AttackCategory.magic)
                {
                    ListSpe01[i].Panel.GetComponent<Image>().color = new Color(0.12f, 0.38f, 0.87f, 1f);
                    ListSpe01[i].cost.GetComponent<TMPro.TextMeshProUGUI>().text = SpellList.O_Set02[i].BaseAttackdata.attackMPCost.ToString();
                }

                if (SpellList.O_Set02[i].Enumsetting.attackCategory == AttackCategory.tech)
                {
                    ListSpe01[i].Panel.GetComponent<Image>().color = new Color(0.09f, 0.754f, 0.3f, 1f);
                    ListSpe01[i].cost.GetComponent<TMPro.TextMeshProUGUI>().text = SpellList.O_Set02[i].BaseAttackdata.attackMPCost.ToString();
                }
                #endregion
            }
            else
            {
                ListSpe01[i].Icon.sprite = noSpell;
            }
        }


    }

    void SpellTrait(Chara_SpellList SpellList)
    {
        // to keep track of spawned object
        List<string> spawnedTrait = new();
        int iteration = 0;

        // get all spell
        List<string> allTraits = new List<string>();
        List<BaseAttack> allspells = new();
        allspells.Add(SpellList.O_attack);
        allspells.Add(SpellList.O_defense);
        foreach (BaseAttack spells in SpellList.O_Set01)
        { if (spells != null) { allspells.Add(spells); } }
        foreach (BaseAttack spells in SpellList.O_Set02)
        { if (spells != null) { allspells.Add(spells); } }

        // instantiate for each spells
        foreach (BaseAttack spells in allspells)
        {
            //Debug.Log(spells);
            foreach (var item in spells.GeneralSetting.spellTrait)
            {
                allTraits.Add(item.Name);
                //Debug.Log(item.Name);
            }
        }

        foreach (BaseAttack spells in allspells)
        {

            foreach (var item in spells.GeneralSetting.spellTrait)
            {
                iteration += 100;

                if (spawnedTrait.Contains(item.Name))
                {
                    // ici on devrait pouvoir chopper l objet qui a le texte puis donner son nombre
                }
                else
                {
                    Vector3 newPos = new Vector3(TraitPanel.transform.position.x + iteration - TraitPanel.transform.position.x / 2, TraitPanel.transform.position.y, TraitPanel.transform.position.z);

                    GameObject obj = Instantiate(TraitIcon, newPos, TraitPanel.transform.rotation);
                    //obj.transform.parent = TraitPanel.transform;
                    obj.transform.SetParent(TraitPanel.transform);

                    //Debug.Log("_______111111111_____   " + item.Name);

                    obj.GetComponent<Image>().sprite = item.Icon;
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = CountOccurrences(allTraits, item.Name).ToString() + " / 8 ";

                }

                spawnedTrait.Add(item.Name);
                //Debug.Log(SO_SpellTrait.Name);

            }
        }
    }

    int CountOccurrences(List<string> list, string target)
    {
        // Debug.Log("444444444444444444   " + list.Count);
        int count = 0;
        foreach (string item in list)
        {
            //Debug.Log("_______111111111_____   " + item);
            //Debug.Log("_____2222222222_______   " + target);
            if (item == target)
            {
                count++;
                //Debug.Log("_____3333333333_______   " + count);
            }
        }
        return count;
    }


    void PopulateSpellInventory(SO_CharacterInventory SpellInventory)
    {
        for (int i = 0; i < SpellInventory.ListSpell.Count; i++)
        {
            Vector3 pos = new Vector3(Parent.transform.position.x, Parent.transform.position.y - 125 * i, Parent.transform.position.z);
            GameObject newSpell = Instantiate(SpellItem, pos, Parent.transform.rotation);
            newSpell.transform.parent = Parent.transform;

            newSpell.GetComponentInChildren<spell_Btn>().Icon.sprite = SpellInventory.ListSpell[i].GeneralSetting.icon;
            // newSpell.GetComponentInChildren<Button>().onClick.AddListener(() => OnButtonSelected(newSpell.GetComponentInChildren<spell_Btn>()));

            // Create a local variable to capture the current button
            Button currentButton = newSpell.GetComponentInChildren<Button>();

            // Add an onClick listener using the local variable
            currentButton.onClick.AddListener(() => OnButtonSelected(newSpell.GetComponentInChildren<spell_Btn>()));

            ////PointerEventData.InputButton.
            //newSpell.GetComponent<EventTrigger>().OnPointerEnter(PointerEventData.);
            //// Create a new entry for PointerEnter event



        }
    }


    public void OnButtonSelected(spell_Btn button)
    {
        //Debug.Log("wwwwwwwwwwwwwwwwwwwwwwwwww");
        if (TextDescription != null && button.spell != null)
        {
            SpellSelected = button;
            TextDescription.text = button.spell.GeneralSetting.attackDescription;
        }
    }

    public void OpenPanelSpells()
    {
        SpellInventory.SetActive(true);

        //GameObject obj = SpellSelected.GetComponent<GameObject>();

        if(AnimationSelected != null)
        {
            AnimationSelected.Kill();
        }

        AnimationSelected = SpellSelected.transform.DOScale(0.95f,1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    public void ClosePanelSpell()
    {
        SpellInventory.SetActive(false);

        AnimationSelected.Kill();
    }

}


////create the event
//EventTrigger.Entry entry = new EventTrigger.Entry();
//entry.eventID = EventTriggerType.PointerEnter;

//// Add a callback to the event
////entry.callback.AddListener((data) => { OnButtonSelected(newSpell.GetComponent<spell_Btn>()); });
//entry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });

//// Add the entry to the EventTrigger
//print(newSpell.GetComponent<EventTrigger>());
//newSpell.GetComponent<EventTrigger>().triggers.Add(entry);

//////PointerEventData.InputButton.
////newSpell.GetComponent<EventTrigger>().OnPointerEnter(PointerEventData.);
////// Create a new entry for PointerEnter event

//// Add a listener to the pointer enter event
//EventTrigger trigger = newSpell.GetComponent<EventTrigger>();
//EventTrigger.Entry entry = new EventTrigger.Entry();
//entry.eventID = EventTriggerType.PointerEnter;
//entry.callback.AddListener((eventData) => OnButtonSelected(newSpell.GetComponentInChildren<spell_Btn>()));
//trigger.triggers.Add(entry);