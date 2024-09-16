using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;
using Movement;

namespace Health
{
    public class EnemyHealthControl : CharacterHealthBase //CharacterNewHealthBase 新动作系统继承新的
    {
        //受到攻击后，先判断能不能格挡，再去判断是否受伤
        //也就是先判断 体力值 是否足够去进行格挡
        //或者 如果一次伤害值 高于一定数值 就认为是破防攻击
        #region 生命周期函数

        protected override void Awake()
        {
            base.Awake();

            //利用 Instantiate 创建一个新的 
            //如果不这么做，就会导致多个敌人共用一个生命配置模板时 生命值、体力值 是相通的
            _characterHealthInfo = ScriptableObject.Instantiate(_healthInfo);

            EnemyManager.MainInstance.AddEnemyUnit(this.gameObject);
        }

        #endregion

        #region 函数相关

        #region 造成伤害

        //这里额外添加了检测敌人生命值是否小于0
        //如果小于0 呼叫事件
        //也可以把 if 语句 放到 下面的角色受击函数中 else 中 TakeDamage 下面
        protected override void TakeDamage(float damage, bool hasParry = false)
        {
            base.TakeDamage(damage, hasParry);
            if (_characterHealthInfo.CurrentHP <= 0)
            {
                GameEventManager.MainInstance.CallEvent("敌人死亡", transform);
                PlayDeadAnimation();
                EnemyManager.MainInstance.RemovedEnemyUnit(this.gameObject);
                this.gameObject.GetComponent<EnemyMovementControl>().enabled = false;
            }
        }

        #endregion

        #region 角色受击

        protected override void CharacterHitAction(float damage, string hitName, string parryName)
        {
            if (_characterHealthInfo.StrengthFull && damage < 30f)
            {
                //格挡
                //如果敌人不在攻击状态下
                if (!animator.AnimationAtTag("Attack"))
                {
                    animator.Play(parryName, 0, 0f);
                    TakeDamage(damage, true);
                    //播放音效
                    GamePoolManager.MainInstance.TryGetOnePoolItem("BLOCKSound", this.transform.position, Quaternion.identity);


                    //如果 _characterHealthInfo.StrengthFull = false
                    //即体力值被清空 那么就需要通知玩家，现在可以进行处决
                    if (!_characterHealthInfo.StrengthFull)
                    {
                        GameEventManager.MainInstance.CallEvent<bool>("激活处决", true);
                    }
                }
            }
            else
            {
                //当生命值低于一定值时，那么就需要通知玩家，现在可以进行处决
                if (_characterHealthInfo.CurrentHP < 20f)
                {
                    GameEventManager.MainInstance.CallEvent<bool>("激活处决", true);
                }
                //受击
                animator.Play(hitName, 0, 0f);
                //播放音效
                GamePoolManager.MainInstance.TryGetOnePoolItem("HITSound", this.transform.position, Quaternion.identity);
                TakeDamage(damage);
            }
        }

        #endregion

        #region 死亡动画

        protected override void PlayDeadAnimation()
        {
            base.PlayDeadAnimation();
            DevelopmentToos.WTF(this.gameObject.name + "已死亡");
        }

        #endregion

        #endregion

    }
}

