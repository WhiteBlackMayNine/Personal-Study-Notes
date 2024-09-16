using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthData
{
    [CreateAssetMenu(fileName = "CharacterHealthInfo", menuName = "Create/Character/HealthData/CharacterHealthInfo", order = 0)]
    public class CharacterHealthInfo : ScriptableObject
    {
        #region �������

        //�������ֵ
        //�������ֵ
        //��ǰ����ֵ
        //��ǰ����ֵ
        //�Ƿ�����
        //��ǰ�����Ƿ����(���ڸ�
        private float _currentHP;
        private float _currentStrength;
        private float _maxHP;
        private float _maxStrength;
        private bool _strengthFull;
        private bool _isDie => (_currentHP <= 0f);//��ǰ����ֵС��0 ������Ϊ true ������

        public float CurrentHP => _currentHP;
        public float CurrentStrength => _currentStrength;
        public float MaxHP => _maxHP;
        public float MaxStrength => _maxStrength;
        public bool IsDie => _isDie;
        public bool StrengthFull => _strengthFull;

        //�������б����ڳ�ʼ�� InitCharacterHealthInfo() CharacterHealthBase�ű��� Start ��ʱ��ֵ

        //���� CharacterHealthBaseData ��ȡ����� �������ֵ������ֵ
        [SerializeField] private CharacterHealthBaseData _characterHealthBase;

        #endregion

        #region �������

        #region ��ʼ������

        public void InitCharacterHealthInfo()
        {
            _maxHP = _characterHealthBase.MaxHp;
            _maxStrength = _characterHealthBase.MaxStrength;
            _currentHP = _maxHP;
            _currentStrength = _maxStrength;
            _strengthFull = true;
        }

        #endregion

        #region ����˺�

        public void Damage(float damage, bool hasParry = false)
        {
            //����������棬��ô����Ҫ�۳�����ֵ
            //�����������ڹ��������У���û����ң�ȴ����Ҵ���
            //�������ָ��ڼ� _strengthFull = flase ʱ����ȥ�۳�
            if (_strengthFull && hasParry)
            {
                _currentStrength = Clamp(_currentStrength, damage, 0f, _maxStrength);
                //�������ֵ������ˣ���_strengthFull����Ϊfalse �����Ʒ�״̬
                if (_currentStrength <= 0f)
                {
                    _strengthFull = false;
                }
            }
            else
            {
                //û������ֵ�Ļ� �۳�Ѫ��
                _currentHP = Clamp(_currentHP, damage, 0f, _maxHP);
            }
        }

        #endregion

        #region �ָ����� ����

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

        #region ����ֵ�Ĵ�С

        private float Clamp(float value, float offValue, float min, float max, bool Add = false)
        {
            return Mathf.Clamp((Add) ? value + offValue : value - offValue, min, max);
            //API�������ǽ�һ��ֵ������ָ������Сֵ�����ֵ֮��
        }

        #endregion

        #endregion

    }
}
