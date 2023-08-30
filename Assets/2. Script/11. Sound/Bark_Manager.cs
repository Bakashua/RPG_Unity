using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bark_Data", menuName = "My Game/Audio/New Bark_Manager")]
[System.Serializable]
public class Bark_Manager : ScriptableObject

{
    public AudioSource source;
    public SO_AudioEvent Bark_Start;
    public SO_AudioEvent Bark_Attack;
    public SO_AudioEvent Bark_FinishEn;
    public SO_AudioEvent Bark_GetHits;
    public SO_AudioEvent Bark_EndCombat;

    public void Play_Bark_Start()
    {
        //Debug.Log(source);
        Bark_Start.Play(source);
    }

    public void Play_Bark_Attack()
    {
        Bark_Attack.Play(source);
    }

    public void Play_Bark_FinishEn()
    {
        Bark_FinishEn.Play(source);
    }

    public void Play_Bark_GetHits()
    {
        Bark_GetHits.Play(source);
    }

    public void Play_Bark_EndCombat()
    {
        Bark_EndCombat.Play(source);
    }


}
