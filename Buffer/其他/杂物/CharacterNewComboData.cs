using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Combo Data", menuName = "Character/Combo/New ComboData")]
public class CharacterNewComboData : ScriptableObject
{
    //�ϵĶ������룬�˺��ǲ����
    //����˵���������� �� Inspector �ϵ� Damage ����Ϊ 25
    //��ô�������������˺��Ͷ��� 25

    //����µĶ�������
    //д��ÿһ�ι�������Ӧһ���˺��������Ƕ������һ���˺�
    //ÿһ���������˺�ֵ����һ��

    [System.Serializable]
    public class ComboDamageInfo
    {
        //�˺������˶������������������Լ�һ���˺�����
        public DamageType _damageType;
        public float Damage;
        public string HitName;
        public string ParryName;
    }

    public enum DamageType
    {
        //�˺�����
        WEAPON,//����
        PUNCH//ȭͷ
    }

    [SerializeField] private string _actionName;//Ҫ���ŵĶ�������
    [SerializeField] private List<ComboDamageInfo> _comboDamageInfo;
    [SerializeField] private float _actionColdTime;//��������ȴʱ�䣬�ν���һ�ζ���ǰ��Ҫ�ĵȴ�ʱ��
    [SerializeField] private CharacterComboData _nextCombo;//��һ������ ��ִ���굱ǰ���������һ��Ҫִ�еĶ���
    [SerializeField] private CharacterComboData _childCombo;//�Ӷ��������Ǳ��ж���

    //�����ⲿ��Ϊ�ж��������ж��Ƿ����Ӷ���������˵����������
    public bool HasChildCombo
    {
        get
        {
            return _childCombo != null;
        }
    }

    public string ActionName
    {
        get
        {
            return _actionName;
        }
    }

    public List<ComboDamageInfo> ComboDamageInfos
    {
        get
        {
            return _comboDamageInfo;
        }
    }

    public float ActionColdTime
    {
        get
        {
            return _actionColdTime;
        }
    }

    public CharacterComboData NextCombo
    {
        get
        {
            return _nextCombo;
        }
    }
    public CharacterComboData ChildCombo
    {
        get
        {
            return _childCombo;
        }
    }
}
