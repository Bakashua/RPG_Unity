using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    #region Variable Declaration

    public List<GameObject> CharaInv = new List<GameObject>();

    //bool addedItem = true;

    #endregion

    public void StoreItem()
    {

    }

    public void TryPickUp()
    {
        
    }

    bool AddItemToInv(bool finishedAdding)
    {
        return finishedAdding;
    }

    //int IncreaseID(int currentID)
    //{
    //    int newID = 1;

    //    for (int itemCount = 1; itemCount <= itemsInInventory.Count; itemCount++)
    //    {
    //        if (itemsInInventory.ContainsKey(newID))
    //        {
    //            newID += 1;
    //        }
    //        else return newID;
    //    }

    //    return newID;
    //}


    // tuto chara inventory

    //    #region Variable Declarations
    //    public static CharacterInventory instance;

    //    //public CharacterStats charStats;
    //    GameObject foundStats;

    //    public Image[] hotBarDisplayHolders = new Image[4];
    //    public GameObject InventoryDisplayHolder;
    //    public Image[] inventoryDisplaySlots = new Image[30];

    //    int inventoryItemCap = 20;
    //    int idCount = 1;
    //    bool addedItem = true;

    //    //public Dictionary<int, InventoryEntry> itemsInInventory = new Dictionary<int, InventoryEntry>();
    //    //public InventoryEntry itemEntry;
    //    #endregion

    //    #region Initializations
    //    void Start()
    //    {
    //        instance = this;
    //      //itemEntry = new InventoryEntry(0, null, null);
    //      //itemsInInventory.Clear();

    //        inventoryDisplaySlots = InventoryDisplayHolder.GetComponentsInChildren<Image>();

    //        foundStats = GameObject.FindGameObjectWithTag("Player");
    //       //charStats = foundStats.GetComponent<CharacterStats>();
    //    }
    //#endregion

    //    void Update()
    //    {
    //        #region Watch for Hotbar Keypresses - Called by Character Controller Later
    //        //Checking for a hotbar key to be pressed
    //        //if (Input.GetKeyDown(KeyCode.Alpha1))
    //        //{
    //        //    TriggerItemUse(101);
    //        //}
    //        //if (Input.GetKeyDown(KeyCode.Alpha2))
    //        //{
    //        //    TriggerItemUse(102);
    //        //}
    //        //if (Input.GetKeyDown(KeyCode.Alpha3))
    //        //{
    //        //    TriggerItemUse(103);
    //        //}
    //        //if (Input.GetKeyDown(KeyCode.Alpha4))
    //        //{
    //        //    TriggerItemUse(104);
    //        //}
    //        //if (Input.GetKeyDown(KeyCode.I))
    //        //{
    //        //    DisplayInventory();
    //        //}
    //        #endregion

    //        //Check to see if the item has already been added - Prevent duplicate adds for 1 item
    //        if (!addedItem)
    //        {
    //           // TryPickUp();
    //        }
    //    }

    //    //    public void StoreItem(ItemPickUp itemToStore)
    //    //{
    //    //    addedItem = false;

    //    //    if ((charStats.characterDefinition.currentEncumbrance + itemToStore.itemDefinition.itemWeight) <= charStats.characterDefinition.maxEncumbrance)
    //    //    {
    //    //        itemEntry.invEntry = itemToStore;
    //    //        itemEntry.stackSize = 1;
    //    //        itemEntry.hbSprite = itemToStore.itemDefinition.itemIcon;

    //    //        //addedItem = false;
    //    //        itemToStore.gameObject.SetActive(false);
    //    //    }
    //    //}



}
