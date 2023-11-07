using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Emblem_", menuName = "My Game/Skills/New Emblem")]
public class SpellEmblemEffect : ScriptableObject
{
    public List<SpellEmblem> EmblemEffect = new();
}


[System.Serializable]
public class SpellEmblem
{

    [SerializeField] string Name;
    [SerializeField] Sprite Icon;
    public int Level;
    [SerializeField] List<Status_Effect_SO> emblemstatus = new();
}
