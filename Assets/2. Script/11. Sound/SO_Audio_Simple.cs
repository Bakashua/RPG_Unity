using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Audio Events/Simple")]
public class SO_Audio_Simple : SO_AudioEvent
{
    public AudioClip[] Clips;

    [Range(0f, 2f)]
    public float Volume_min;
    [Range(0f, 2f)]
    public float Volume_max;
    [Range(0f, 2f)]
    public float Pitch_min;
    [Range(0f, 2f)]
    public float Pitch_max;

    public override void Play(AudioSource source)
    {
        if (Clips.Length == 0) return;

        source.clip = Clips[Random.Range(0, Clips.Length)];
        source.volume = Random.Range(Volume_min, Volume_max);
        source.pitch = Random.Range(Pitch_min, Pitch_max);
        //Debug.Log("___1___" +source.volume);
        source.Play();
    }
}
