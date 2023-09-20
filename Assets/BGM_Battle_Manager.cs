using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Battle_Manager : MonoBehaviour
{
    AudioSource source;

    [SerializeField] SO_AudioEvent ClassicCombat;
    [SerializeField] SO_AudioEvent EpicEncounter;
    [SerializeField] SO_AudioEvent HeroicTriomph;
    [SerializeField] SO_AudioEvent InDanger;

    SO_AudioEvent previiousSO;

    [SerializeField] bool isBoss;
    [SerializeField] bool isDanger;
    public bool isHeroic;


    // besoin de dtat pour play music de boss ou quoi selon le combat

    // previous music cat = same as nw dont play music

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Listener_Initiate_BGM()
    {
        if (previiousSO != ClassicCombat && !isHeroic)
        {
           // Debug.Log("________________" + previiousSO);

            ClassicCombat.Play(source);
            previiousSO = ClassicCombat;
            //Debug.Log("_______222_________" + previiousSO);
        }
        //Debug.Log("wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
        // check si on joue deja le mem son ou mem categorie on ne rejoue pas le son !!!
        if (isHeroic && previiousSO != HeroicTriomph)
        {
            //Debug.Log("wwwwwwwwwwwwwwwww" + previiousSO);
            HeroicTriomph.Play(source);
            previiousSO = HeroicTriomph;
            
        }
        else if (isDanger && previiousSO != InDanger)
        {
            InDanger.Play(source);
            isDanger = false;
            previiousSO = InDanger;
        }
        else if (isBoss && previiousSO != EpicEncounter)
        {
            EpicEncounter.Play(source);
            isBoss = false;
            previiousSO = EpicEncounter;
        }


        //isHeroic = false;

    }


    public void Listener_Update_BGM()
    {
        // check les data la
        int i = Random.Range(0, 100);
        
        //if (i >= 95)
        //{
        //    isBoss = true;
        //}
        //else if (i >= 85)
        //{
        //    isDanger = true;
        //}

        Listener_Initiate_BGM();
    }

    public void Listener_IsHeroicON()
    {
        Debug.Log("sssssssssss") ;
        isHeroic = true;
    }


}
