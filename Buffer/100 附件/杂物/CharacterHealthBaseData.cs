using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HealthData
{
    [CreateAssetMenu(fileName = "BaseHealthData", menuName = "Create/Character/HealthData/BaseData", order = 0)]
    public class CharacterHealthBaseData : ScriptableObject
    {
        //处理每一种敌人的初始生命值信息
        //可能有多种敌人，导致初始生命值不一样
        [SerializeField] private float _maxHP;//最大生命值
        [SerializeField] private float _maxStrength;//最大体力值

        /// <summary>
        /// 初始最大生命值
        /// </summary>
        public float MaxHp => _maxHP;

        /// <summary>
        /// 初始最大体力值
        /// </summary>
        public float MaxStrength => _maxStrength;
    }
}
