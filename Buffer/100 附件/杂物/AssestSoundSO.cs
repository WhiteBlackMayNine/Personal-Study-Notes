using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assest
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Create/Assets/Sound", order = 0)]
    public class AssestSoundSO : ScriptableObject
    {
        [System.Serializable]
        private class Sounds
        {
            public SoundType SoundType;//����������
            public AudioClip[] AudioClip;//��Ƶ�ļ�
        }

        [SerializeField] private List<Sounds> _configSound = new List<Sounds>();

        public AudioClip GetAudioClip(SoundType type)
        {
            if (_configSound.Count == 0)
            {
                return null;
            }

            switch (type)
            {
                //_configSound[0] ��������б��е�һ��    case���������Ҫ��Unity�е�˳��һ��     
                // Random��������Ƭ�������һ������
                case SoundType.ATK:
                    return _configSound[0].AudioClip[Random.Range(0, _configSound[0].AudioClip.Length)];
                case SoundType.HIT:
                    return _configSound[1].AudioClip[Random.Range(0, _configSound[1].AudioClip.Length)];
                case SoundType.BLOCK:
                    return _configSound[2].AudioClip[Random.Range(0, _configSound[2].AudioClip.Length)];
                case SoundType.FOOT:
                    return _configSound[3].AudioClip[Random.Range(0, _configSound[3].AudioClip.Length)];
                default:
                    break;
            }
            return null;
        }
    }
}
