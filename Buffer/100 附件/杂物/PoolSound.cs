using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assest;

public enum SoundType//�������ͣ�Ϊ�˸��õĹ����������
{
    ATK,//����
    HIT,//�ܻ�
    BLOCK,//��
    FOOT//�Ų���
}

public class PoolSound : PoolItemBase
{
    #region �������

    private AudioSource _audioSource;
    [SerializeField] private SoundType _type;
    [SerializeField] private AssestSoundSO _soundAssest;

    #endregion

    #region �������ں���

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    #endregion

    #region �������

    public override void Spawn()
    {
        //����������ʱ����������
        //����������Ὺʼ����ʱ��0.3s ������ᱻ����
        PlaySound();
    }

    private void PlaySound()//�������� ���ݲ�ͬ���������������Ų�ͬ������
    {
        _audioSource.clip = _soundAssest.GetAudioClip(_type);
        _audioSource.Play();
        StartRecycl();
    }

    private void StartRecycl() // ���պ���
    {
        GameTimeManager.MainInstance.TryGetTimer(0.3f, DisableSelf);
    }

    private void DisableSelf()
    {
        _audioSource.Stop();
        this.gameObject.SetActive(false);//���ص�
    }

    #endregion

}
