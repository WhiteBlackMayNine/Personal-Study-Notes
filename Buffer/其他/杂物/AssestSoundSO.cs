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
            public SoundType SoundType;//声音的类型
            public AudioClip[] AudioClip;//音频文件
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
                //_configSound[0] 代表的是列表中第一个    case后面的类型要和Unity中的顺序一致     
                // Random从声音切片中随机拿一个出来
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
