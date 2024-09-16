using GGG.Tool;
using HealthData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterComboData;

namespace Health
{
    #region 新攻击系统接口

    public interface IDamaged
    {
        void CharacterNormalDamaged(string hitName, string parryName, float damage, Transform attack, DamageType damageType);
    }

    #endregion
    public abstract class CharacterNewHealthBase : MonoBehaviour, IHealth, IDamaged
    {
        //角色和玩家，共同都有的变量函数，都写在基类这里
        //有受伤函数、格挡、生命值相关

        #region 变量相关

        protected Animator animator;

        protected Transform _currentAttack;//当前的攻击者，也就是谁打的

        [SerializeField, Header("角色生命值模板")] protected CharacterHealthInfo _healthInfo;

        protected CharacterHealthInfo _characterHealthInfo;

        #endregion

        #region 生命周期函数

        protected virtual void Awake()
        {
            animator = this.GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            _characterHealthInfo.InitCharacterHealthInfo();
        }

        protected virtual void Update()
        {
            OnHitParryLookAttacker();
        }

        #endregion

        #region 函数相关

        #region 受伤函数

        /// <summary>
        /// 角色受伤行为
        /// </summary>
        /// <param name="hitName">受伤动画</param>
        /// <param name="parryName">格挡动画</param>
        protected virtual void CharacterHitAction(float damage, string hitName, string parryName)
        {
            //让子类去重写这个函数
            //不同的对象 受伤行为可能不同

            //这个函数用来处理 执行动画 的逻辑
        }

        #endregion

        #region 检测攻击者

        /// <summary>
        /// 设置当前的攻击者
        /// </summary>
        /// <param name="attacker"></param>
        private void SettingAttakcer(Transform attacker)
        {
            //这个函数 用来处理 寻找攻击者（也就是玩家）逻辑
            if (_currentAttack == null || _currentAttack != attacker)
            {
                _currentAttack = attacker;
            }
        }

        #endregion

        #region 伤害检测

        protected virtual void TakeDamage(float damage, bool hasParry = false)
        {
            _characterHealthInfo.Damage(damage, hasParry);
        }

        #endregion

        #region 受伤事件

        #region 普通攻击受伤

        private void OnCharacterHitEventHandler(float damage, string hitName, string parryName, Transform attacker,
    Transform self)
        {
            //这个函数 用来处理 被玩家攻击后 逻辑
            //被攻击后，检测被攻击者是不是自己，调用其他受伤函数

            //传入 造成的伤害、受伤动画名、格挡动画名、以及攻击者位置（判断是不是玩家打的），以及自己的位置
            //可能有多个敌人，但玩家只打了一个，self 判断被攻击者是不是自己

            //如果传进来的 self 不是自己，那么不执行事件
            if (self != this.transform)
            {
                return;
            }

            SettingAttakcer(attacker);//玩家正在攻击该敌人

            CharacterHitAction(damage, hitName, parryName);//调用动画执行函数，执行动画

        }

        #endregion

        #endregion

        #region 处决

        private void OnCharacterFinishEventHandler(string hitName, Transform attacker, Transform self)
        {
            if (self != transform)
            {
                return;
            }

            SettingAttakcer(attacker);
            animator.Play(hitName);
        }

        private void TriggerDamageEventHandler(float damage, Transform self)
        {
            if (self != transform)
            {
                return;
            }

            TakeDamage(damage);

            GamePoolManager.MainInstance.TryGetOnePoolItem("HITSound", transform.position, Quaternion.identity);

        }

        #endregion

        #region 受击/格挡 看向

        //受击时看着攻击者
        private void OnHitParryLookAttacker()
        {
            if (_currentAttack == null)
            {
                return;
            }

            if (animator.AnimationAtTag("Hit") || animator.AnimationAtTag("Parry")
                    && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            {
                this.transform.Look(_currentAttack.transform.position, 50f);
            }

        }

        #endregion

        #region 当前目标死亡后移除当前目标

        #region 第一种
        //在Update中调用CheackEnemyIsDie 去检测生命值，有没有死亡
        //但这样做有些浪费资源，毕竟是每一帧都去检测
        //通过接口 IHealth 来监听目标生命值

        public bool OnDie() => _characterHealthInfo.IsDie;
        //PlayerCombat 中还有 CheackEnemyIsDie();

        #endregion

        #region 第二种
        //通过事件监听 
        //PlayerCombat 、 EnemyCombatControl 中添加事件
        //同一个事件，但要注册两个函数
        //在 EnemyHealthControl 中呼叫事件

        protected virtual void PlayDeadAnimation()
        {
            //如果 处决执行过后 敌人就会死亡然后播放死亡动画
            //那么这里就需要使用 if 语句 
            //否则就会 播放两次 一次为处决 一次为正常死亡
            //FinisHit 为 被处决动画 的标签 敌人被处决时播放这个动作
            if (!animator.AnimationAtTag("FinishHit"))
            {
                animator.SetBool(AnimationID.IsDieID, true);
            }

        }

        #endregion

        #endregion

        #endregion

        #region 新攻击输入系统接口实现

        //外部可以通过接口来调用这个函数
        //函数内部又会 调用 角色受击函数
        public void CharacterNormalDamaged(string hitName, string parryName, float damage, Transform attack, DamageType damageType)
        {

            CharacterHitAction(damage, hitName, parryName);
        }

        #endregion

    }
}


