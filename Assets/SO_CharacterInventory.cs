using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Character Inventory", menuName = "My Game/Character/New Inventory")]
public class SO_CharacterInventory : ScriptableObject
{
    public int Gold;
    public List<BaseAttack> ListSpell = new();

}
