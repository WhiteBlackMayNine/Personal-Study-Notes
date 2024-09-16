using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HealthData
{
    [CreateAssetMenu(fileName = "BaseHealthData", menuName = "Create/Character/HealthData/BaseData", order = 0)]
    public class CharacterHealthBaseData : ScriptableObject
    {
        //����ÿһ�ֵ��˵ĳ�ʼ����ֵ��Ϣ
        //�����ж��ֵ��ˣ����³�ʼ����ֵ��һ��
        [SerializeField] private float _maxHP;//�������ֵ
        [SerializeField] private float _maxStrength;//�������ֵ

        /// <summary>
        /// ��ʼ�������ֵ
        /// </summary>
        public float MaxHp => _maxHP;

        /// <summary>
        /// ��ʼ�������ֵ
        /// </summary>
        public float MaxStrength => _maxStrength;
    }
}
