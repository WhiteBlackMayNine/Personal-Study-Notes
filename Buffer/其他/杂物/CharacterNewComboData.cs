using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Combo Data", menuName = "Character/Combo/New ComboData")]
public class CharacterNewComboData : ScriptableObject
{
    //老的动作代码，伤害是不变的
    //比如说有两个动作 在 Inspector 上的 Damage 设置为 25
    //那么这两个动作的伤害就都是 25

    //这个新的动作代码
    //写成每一段攻击都对应一段伤害，而不是多个动作一个伤害
    //每一个动作的伤害值都不一样

    [System.Serializable]
    public class ComboDamageInfo
    {
        //伤害，受伤动画名，格挡名，还可以加一个伤害类型
        public DamageType _damageType;
        public float Damage;
        public string HitName;
        public string ParryName;
    }

    public enum DamageType
    {
        //伤害类型
        WEAPON,//武器
        PUNCH//拳头
    }

    [SerializeField] private string _actionName;//要播放的动作名字
    [SerializeField] private List<ComboDamageInfo> _comboDamageInfo;
    [SerializeField] private float _actionColdTime;//动作的冷却时间，衔接下一段动作前需要的等待时间
    [SerializeField] private CharacterComboData _nextCombo;//下一个动作 ，执行完当前动作后的下一个要执行的动作
    [SerializeField] private CharacterComboData _childCombo;//子动作，就是变招动作

    //方便外部作为判断条件来判断是否有子动作，或者说是派生动作
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
