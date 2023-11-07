using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "SpellTrait", menuName = "My Game/Skills/New SpellTrait")]
public class SO_SpellTrait : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public bool isOrigin;
    public bool isClass;

    //[System.Serializable]
    //public class SpellTraits
    //{
    //    //public enum SpellTraits
    //    //{ traitorigin1, traitorigin2, traitorigin3, traitorigin4, traitorigin5, traitorigin6, traitCategory1, traitCategory2, traitCategory3, traitCategory4, traitCategory5 }
    //}
}
