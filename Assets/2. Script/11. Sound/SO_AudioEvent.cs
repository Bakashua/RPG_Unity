using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SO_AudioEvent : ScriptableObject
{
    public abstract void Play(AudioSource source);
}
