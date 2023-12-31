using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "Encounter Group", menuName = "My Game/Enemy/New Encounter")]


public class SO_Encounter : ScriptableObject
{
    public List<Monster> encounter = new();
}



[System.Serializable]
public class Monster
{
    public Chara_BaseStats Enemy;
    public int PosA;
    public int PosB;
}

