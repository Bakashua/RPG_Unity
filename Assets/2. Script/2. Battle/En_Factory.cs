using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "En_Factory", menuName = "My Game/Enemy/New En_Factory")]
[System.Serializable]
public class En_Factory : ScriptableObject
{

    [Header("Instantiate Monster Data")]
    public Chara_Hero enemy;
    public List<Chara_Hero> EnemyList;
    public List<Chara_Hero> Enemy_Instance;
    //public int favoritePosition;

    // create en, clear en
    
    public void CreateInstance()
    {
        foreach(Chara_Hero obj in EnemyList)
        {
        Chara_Hero newEn = ScriptableObject.Instantiate(obj);
            Enemy_Instance.Add(newEn);
        }
        //Chara_Hero newEn = ScriptableObject.Instantiate(enemy);
    }


    public void ReceivePartyMember()
    {

    }

    public void RemovePartyMember()
    {

    }
}

