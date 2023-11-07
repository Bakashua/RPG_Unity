using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "DropReward", menuName = "My Game/Enemy/New DropReward")]
public class DropReward : ScriptableObject
{
    public float xpReceived;
    public float goldReceived;
    public List<SpellDrop> SpellDroped = new List<SpellDrop>();

}

[System.Serializable] 
public class SpellDrop
{
    public BaseAttack spell;
    [Range(0.0f, 100.0f)]
    public int dropRate;
}