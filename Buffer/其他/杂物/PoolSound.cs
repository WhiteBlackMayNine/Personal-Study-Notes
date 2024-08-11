using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assest;

public enum SoundType//声音类型，为了更好的管理多种声音
{
    ATK,//攻击
    HIT,//受击
    BLOCK,//格挡
    FOOT//脚步声
}

public class PoolSound : PoolItemBase
{
    private AudioSource _audioSource;
    [SerializeField] private SoundType _type;
    [SerializeField] private AssestSoundSO _soundAssest;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Spawn()
    {
        //当自身被激活时，播放声音
        //播放声音后会开始倒计时，0.3s 后自身会被隐藏
        PlaySound();
    }

    private void PlaySound()//播放声音 根据不同的声音类型来播放不同的声音
    {
        _audioSource.clip = _soundAssest.GetAudioClip(_type);
        _audioSource.Play();
        StartRecycl();
    }

    private void StartRecycl() // 回收函数
    {
        GameTimeManager.MainInstance.TryGetTimer(0.3f, DisableSelf);
    }

    private void DisableSelf()
    {
        _audioSource.Stop();
        this.gameObject.SetActive(false);//隐藏掉
    }
}
