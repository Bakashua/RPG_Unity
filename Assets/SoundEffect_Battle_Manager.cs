using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect_Battle_Manager : MonoBehaviour
{

    [Header("AUDIO")]
    [SerializeField] AudioSource source;


    [SerializeField] SO_AudioEvent SE_Click;

    public void SE_ClickPlay()
    {
        SE_Click.Play(source);

    }



}
