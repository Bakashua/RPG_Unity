using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VFX", menuName = "My Game/Visual/New VisualEffect")]
[System.Serializable]

public class VFX_Spawner : ScriptableObject
{
    public GameObject Fx;
    public Transform Target;

    public SO_AudioEvent Vfx_Audio;


    public void SpawnFX()
    {
        Instantiate(Fx, Target);

        if (Vfx_Audio != null)
        {
            AudioSource audioSource = Fx.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = Fx.AddComponent<AudioSource>();
            }

            Vfx_Audio.Play(audioSource);
        }
    }

}
