using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerAudio : AuidoPlayBase
{
    public AudioClip clip;
    public override void Play(AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }

    
}
