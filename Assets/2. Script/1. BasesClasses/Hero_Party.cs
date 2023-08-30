using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Hero_Party", menuName = "My Game/Hero_Party")]
public class Hero_Party : ScriptableObject
{

    [Header("Instantiate Monster Data")]
    public List<GameObject> HeroInParty_Go;
    public List<Chara_Hero> HeroInParty_Data;
    //public int favoritePosition;

    public void ReceivePartyMember()
    {

    }

    public void RemovePartyMember()
    {

    }

}
