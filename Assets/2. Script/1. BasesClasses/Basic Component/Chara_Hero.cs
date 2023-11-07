using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Chara_Hero", menuName = "My Game/Character/ New Hero")]
public class Chara_Hero : ScriptableObject
{
    public Chara_BaseStats Stats;
    public Chara_SpellList SpellList;
    public Chara_Leveling leveling;
    public Bark_Manager BarkData;
    public SO_CharacterInventory Inventory;
    //public Chara_Visual_UI UI;
    //public Chara_Leveling Leveling;
}
