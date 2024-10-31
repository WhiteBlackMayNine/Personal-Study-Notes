using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthData
{
    [CreateAssetMenu(fileName = "CharacterHealthInfo", menuName = "Create/Character/HealthData/CharacterHealthInfo", order = 0)]
    public class CharacterHealthInfo : ScriptableObject
    {
        #region 变量相关

        //最大生命值
        //最大体力值
        //当前生命值
        //当前体力值
        //是否死亡
        //当前体力是否充沛(用于格挡
        private float _currentHP;
        private float _currentStrength;
        private float _maxHP;
        private float _maxStrength;
        private bool _strengthFull;
        private bool _isDie => (_currentHP <= 0f);//当前生命值小于0 就设置为 true 即死亡

        public float CurrentHP => _currentHP;
        public float CurrentStrength => _currentStrength;
        public float MaxHP => _maxHP;
        public float MaxStrength => _maxStrength;
        public bool IsDie => _isDie;
        public bool StrengthFull => _strengthFull;

        //以上所有变量在初始化 InitCharacterHealthInfo() CharacterHealthBase脚本中 Start 的时候赋值

        //关联 CharacterHealthBaseData 获取具体的 最大生命值与体力值
        [SerializeField] private CharacterHealthBaseData _characterHealthBase;

        #endregion

        #region 函数相关

        #region 初始化变量

        public void InitCharacterHealthInfo()
        {
            _maxHP = _characterHealthBase.MaxHp;
            _maxStrength = _characterHealthBase.MaxStrength;
            _currentHP = _maxHP;
            _currentStrength = _maxStrength;
            _strengthFull = true;
        }

        #endregion

        #region 造成伤害

        public void Damage(float damage, bool hasParry = false)
        {
            //如果体力充沛，那么就需要扣除体力值
            //敌人现在正在攻击动作中，还没打到玩家，却被玩家打到了
            //在体力恢复期间 _strengthFull = flase 时，不去扣除
            if (_strengthFull && hasParry)
            {
                _currentStrength = Clamp(_currentStrength, damage, 0f, _maxStrength);
                //如果体力值被清空了，把_strengthFull设置为false 就是破防状态
                if (_currentStrength <= 0f)
                {
                    _strengthFull = false;
                }
            }
            else
            {
                //没有体力值的话 扣除血量
                _currentHP = Clamp(_currentHP, damage, 0f, _maxHP);
            }
        }

        #endregion

        #region 恢复生命 体力

        public void AddHP(float hp)
        {
            _currentHP = Clamp(_currentHP, hp, 0f, _maxHP, true);
        }

        public void AddStrength(float strength)
        {
            _currentStrength = Clamp(_currentHP, strength, 0f, _maxStrength, true);
            if (_currentStrength >= _maxStrength)
            {
                _strengthFull = true;
            }
        }

        #endregion

        #region 限制值的大小

        private float Clamp(float value, float offValue, float min, float max, bool Add = false)
        {
            return Mathf.Clamp((Add) ? value + offValue : value - offValue, min, max);
            //API的作用是将一个值限制在指定的最小值和最大值之间
        }

        #endregion

        #endregion

    }
}
