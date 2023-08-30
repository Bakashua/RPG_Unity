using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Spells", menuName = "My Game/Enemy/New SpellList")]
public class En_SpellList_SO : MonoBehaviour
{

    public List<Spells> Actions = new();
}
