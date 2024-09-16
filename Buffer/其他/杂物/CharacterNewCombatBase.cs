using GGG.Tool;
using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterComboData;

//新的攻击输入采用的是 接口 
//如果希望使用事件的话 就按照老的去修改
public abstract class CharacterNewCombatBase : MonoBehaviour
{
    #region 变量相关

    protected Animator _animator;

    [SerializeField, Header("ComboData")] protected CharacterComboData _comboData;

    protected Transform _me;//挂载脚本的对象


    [SerializeField, Header("攻击检测")] protected float _detectionRang;//范围

    [SerializeField] private LayerMask _enemyLayer;//敌人的层级

    protected Transform _enemy;//当前的敌人

    protected float _attackColdTime;
    protected bool _applyAttackInput;

    #endregion

    #region 生命周期函数

    private void Awake()
    {
        _me = this.transform;
        _animator = this.GetComponent<Animator>();
        ResetAttackInput();
    }

    #endregion

    #region 函数相关

    #region 动画事件

    //不同于 老的动作输入
    //这个 受伤索引 根据动画来进行更新（通过动画来取）
    //比如说 一个动画 有八个攻击动作 那么老攻击就需要整八个受击与格挡（假设八个全不同）而且伤害都相同
    //（假设这八个攻击中 第一个 第二到七个 第八个 分别需要一个不同的受击与格挡当红）
    //那么，新的攻击输入，只需要写三个就行 第一个的index为0（动画添加时就有 int）
    //第二到第七个的index 为1 第八个为3
    //对应的就行新动作组件 在 ComboDamageInfo 这个 List 中添加三个，分别对应这 0 1 2
    //在 ComboDamageInfo 编写信息
    //通过动画事件 ATK 的 index 去 ComboDamageInfo 中拿相应的受伤动画、格挡、伤害 

    public void NewATK(int index)
    {
        TriggerDamage(index);
    }

    #endregion

    #region 伤害触发

    private void TriggerDamage(int index)
    {
        //获取当前一个锁定的敌人
        if (GetUnits().Length != 0)
        {
            if (_enemy == null || _enemy != GetUnits()[0].transform)
            {
                if (_enemy != GetUnits()[0].transform != _me)
                {
                    _enemy = GetUnits()[0].transform;
                }
            }
        }
        if (GetUnits().Length != 0)
        {
            foreach (var e in GetUnits())
            {
                //判断敌人的角度是否小于0.85
                if (Vector3.Dot(DevelopmentToos.DirectionForTarget(e.transform, _me), _me.forward) > 0.85f)
                {
                    continue;
                }
                //判断敌人的距离是否小于3
                if (DevelopmentToos.DistanceForTarget(e.transform, _me) > 3f)
                {
                    continue;
                }
                if (e.transform.TryGetComponent(out IDamaged damage))
                {
                    //Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)
                    //防止动画的index输入错误 ComboDamageInfos 只有三个，但输入 5
                    //那么就去最小值 3
                    damage.CharacterNormalDamaged(_comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)].HitName,
                        _comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)].ParryName,
                        _comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)].Damage,
                        _me, _comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)]._damageType);

                }
            }
        }
    }

    #endregion

    #region 得到敌人

    private Collider[] GetUnits()
    {
        return Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRang,
       _enemyLayer, QueryTriggerInteraction.Ignore);
    }

    #endregion

    #region 重写接口

    //public void CharacterNormalDamaged(string hitName, string parryName, float damage, Transform attacker, DamageType damageType)
    //{
    //    throw new System.NotImplementedException();
    //}

    #endregion

    #region 攻击动作的执行

    protected void ComboActionExecute()
    {
        _attackColdTime = _comboData.ActionColdTime;

        _animator.CrossFade(_comboData.ActionName, 0.025f, 0, 0f);

        GameTimeManager.MainInstance.TryGetTimer(_attackColdTime, ResetAttackInput);

        _applyAttackInput = false;
    }

    #endregion

    #region 更新下一个动作

    protected void SetComboData(CharacterComboData comboData)
    {
        //可能有的动作没派生动作
        //在执行这些动作时有失误点击了右键
        //那么就可能造成空引用
        //这里判断一下
        if (comboData != null)
        {
            _comboData = comboData;
        }
    }

    #endregion

    #region 更新攻击输入

    protected void ResetAttackInput()
    {
        _applyAttackInput = true;
    }

    #endregion

    #endregion
}
