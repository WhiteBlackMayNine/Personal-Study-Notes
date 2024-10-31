using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RandomPlayAudio : AuidoPlayBase
{
    //希望随机播放的音效文件
    public List<AudioClip> clips;
    public override void Play(AudioSource source)
    {
        if (clips.Count == 0)
            return;

        //设置音效切片文件
        source.clip = clips[Random.Range(0, clips.Count)];
        source.Play();
    }
}
