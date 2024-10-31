using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RandomPlayAudio : AuidoPlayBase
{
    //ϣ��������ŵ���Ч�ļ�
    public List<AudioClip> clips;
    public override void Play(AudioSource source)
    {
        if (clips.Count == 0)
            return;

        //������Ч��Ƭ�ļ�
        source.clip = clips[Random.Range(0, clips.Count)];
        source.Play();
    }
}
