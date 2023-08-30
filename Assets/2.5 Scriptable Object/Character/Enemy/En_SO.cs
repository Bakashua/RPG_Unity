using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "My Game/Enemy/New DropReward")]
public class En_SO : ScriptableObject
{
    public Chara_BaseStats Stats;
    public En_SpellList_SO Spells;
    public DropReward Reward;
}
