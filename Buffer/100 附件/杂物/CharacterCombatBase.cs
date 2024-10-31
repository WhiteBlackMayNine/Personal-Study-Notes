using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CharacterCombatBase : MonoBehaviour
    {
        #region 注释

        //玩家和AI他们的攻击事件触发、伤害触发也是相同的
        //基础组合技也是差不多的
        //组合技信息也是相似的
        //把差不多的都提取到基类里面

        #endregion

        #region 变量相关

        protected Animator _animator;

        #region 攻击/受击相关

        [SerializeField, Header("角色连招表")] protected CharacterComboSO _baseCombo;
        [SerializeField, Header("角色变招表")] protected CharacterComboSO _changeCombo;
        protected CharacterComboSO _currentCombo;//当前使用的连招

        protected int _currentComboIndex;//当前连招的索引值
        protected float _maxColdTime;//连招的最大冷却时间
        protected bool _canAttackInput;//是否允许进行攻击输入
        protected int _hitIndex;//受击索引值

        protected int _currentComboCount;//当前连招数量

        #endregion

        #region 敌人检测
        //一些函数需要这个变量

        [SerializeField, Header("当前敌人")] protected Transform _currentEnemy;//当前敌人位置 用于敌人检测中

        #endregion

        #region 处决 暗杀

        [SerializeField, Header("处决")] protected CharacterComboSO _finishCombo;
        [SerializeField, Header("暗杀")] protected CharacterComboSO _assassinationCombo;

        //处决/暗杀  防止在普通攻击为结算前进行处决时抛出的 超出索引值 问题
        protected int _FinishComboIndex;//处决单独用这个处理，各干个
        protected bool _canFinish;//是否可以进行处决 比如当生命值/体力值低于一定值时

        #endregion

        #endregion

        #region 生命周期函数

        protected virtual void Awake()
        {
            _animator = this.GetComponent<Animator>();
        }

        protected void Start()
        {
            _canAttackInput = true;
            _currentCombo = _baseCombo;
        }

        protected virtual void Update()
        {
            LookTargetOnAttack();
            MatchPosition();
            OnEndAttack();
        }


        #endregion

        #region 函数相关

        #region 位置同步

        protected virtual void MatchPosition()
        {
            if (_currentEnemy == null)
            {
                return;
            }

            if (!_animator)
            {
                return;
            }

            //如果敌人在播放死亡动画时 突然往前移动了
            //就是因为在播放攻击动画时，被玩家给打死了，触发了攻击事件，进行了位置匹配
            //可以把 _animator.MatchTarget 给注释了

            if (_animator.AnimationAtTag("Attack"))
            {
                //对动画时间归一化 0 — 1 的一个值
                var timer = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                //播放进度大约 35% 返回
                if (timer > 0.35f)
                {
                    return;
                }

                //距离过大 返回
                if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 2f)
                {
                    return;
                }

                //不在匹配状态  同时不在过渡中
                if (!_animator.isMatchingTarget && !_animator.IsInTransition(0))
                {
                    _animator.MatchTarget(_currentEnemy.position + (-transform.forward * _currentCombo.TryGetComboPositionOffset(_currentComboIndex)),
                    Quaternion.identity, AvatarTarget.Body, new MatchTargetWeightMask(Vector3.one, 0f), 0, 0.35f);
                }
            }
        }

        protected void RunningMatch(CharacterComboSO comboSO, int index, float startTime = 0f,
            float endTime = 0.01f)
        {
            //不在匹配状态  同时不在过渡中
            if (!_animator.isMatchingTarget && !_animator.IsInTransition(0))
            {
                _animator.MatchTarget(_currentEnemy.position +
                    (-transform.forward * comboSO.TryGetComboPositionOffset(index)),
                Quaternion.identity, AvatarTarget.Body, new MatchTargetWeightMask(Vector3.one, 0f), startTime, endTime);
            }
        }

        #endregion

        #region 动画事件

        //这个函数用于触发动画事件
        //包括且不限于 动画声音播放、伤害等
        protected void ATK()
        {
            TriggerDamager();
            UpdateHitIndex();
            GamePoolManager.MainInstance.TryGetOnePoolItem("ATKSound", this.transform.position, Quaternion.identity);
        }

        #endregion

        #region 伤害触发

        protected void TriggerDamager()
        {
            //要确保有目标
            if (_currentEnemy == null)
            {
                return;
            }
            //要确保敌人处于我们可触发伤害的距离和角度
            //这个是角度
            if (Vector3.Dot(transform.forward, DevelopmentToos.DirectionForTarget(transform, _currentEnemy)) < 0.9f)
            {
                return;
            }
            //这个是距离
            if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 1.3f)
            {
                return;
            }
            //去呼叫事件中，调用触发伤害这个函数
            if (_animator.AnimationAtTag("Attack"))
            {
                GameEventManager.MainInstance.CallEvent("触发伤害", _currentCombo.TryGetComboDamage(_currentComboIndex),
                    _currentCombo.TryGetOneHitAction(_currentComboIndex, _hitIndex),
                    _currentCombo.TryGetOneParryAction(_currentComboIndex, _hitIndex),
                    transform, _currentEnemy);//伤害值 受伤动画名 格挡动画名 攻击者 当前被攻击者
                                              //这个写完基本就不用动了
                                              //这里传入的受伤动画是单个片段
            }
            else
            {
                //这里就是处决、暗杀等其他动作  需要调用另外的函数
                //处决是一个完整的被处决动作，导致在动作期间可能会有多次触发伤害

                GameEventManager.MainInstance.CallEvent("生成伤害", _finishCombo.TryGetComboDamage(_FinishComboIndex)
                    , _currentEnemy);
            }
        }

        #endregion

        #region 攻击看着目标

        protected void LookTargetOnAttack()
        {
            //要确保有一个敌人目标
            if (_currentEnemy == null)
            {
                return;
            }
            //确保玩家与目标之间的距离要小于一定距离
            if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 5f)
            {
                return;
            }
            //获取当前动画状态的详细信息
            if (_animator.AnimationAtTag("Attack") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            {
                transform.Look(_currentEnemy.position, 1000f);
            }
        }

        #endregion

        #region 基础攻击

        protected virtual void CharacterBaseAttackInput()
        {

        }

        #endregion

        #region 更新连招动画信息

        protected virtual void UpdataComboInfo()
        {
            _currentComboIndex++;

            if (_currentComboIndex == _currentCombo.TryGetComboMaxCount())
            {
                //如果执行到了最后一个动作
                _currentComboIndex = 0;
            }
            _maxColdTime = 0f;
            _canAttackInput = true;
        }

        protected void UpdateHitIndex()
        {
            _hitIndex++;
            if (_hitIndex == _currentCombo.TryGetHitAndParryMaxCount(_currentComboIndex))
            {
                _hitIndex = 0;
            }
        }

        #endregion

        #region 重置连招动画信息

        protected void ResetComboInfo()
        {
            _currentComboIndex = 0;
            _maxColdTime = 0f;
            _hitIndex = 0;
        }

        protected void OnEndAttack()
        {
            //移动后从头开始进行攻击
            if (_animator.AnimationAtTag("Motion") && _canAttackInput)
            {
                ResetComboInfo();
            }
        }

        #endregion

        #region 改变组合技

        protected void ChangeComboData(CharacterComboSO characterComboSO)
        {
            if (_currentCombo != characterComboSO)
            {
                _currentCombo = characterComboSO;
                ResetComboInfo();
            }
        }

        #endregion

        #region 执行动作
        protected virtual void ExecuteComboAction()
        {
            _currentComboCount += (_currentCombo == _baseCombo) ? 1 : 0;
            _hitIndex = 0;
            if (_currentComboIndex == _currentCombo.TryGetComboMaxCount())
            {
                //如果执行到了最后一个动作
                _currentComboIndex = 0;
            }

            _maxColdTime = _currentCombo.TryGetComboColdTime(_currentComboIndex);
            _animator.CrossFadeInFixedTime(_currentCombo.TryGetOneComboAction(_currentComboIndex), 0.15f, 0, 0);
            GameTimeManager.MainInstance.TryGetTimer(_maxColdTime, UpdataComboInfo);
            _canAttackInput = false;
        }

        #endregion

        #endregion


    }
}
