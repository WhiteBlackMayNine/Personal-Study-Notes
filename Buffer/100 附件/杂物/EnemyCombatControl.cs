using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyCombatControl : CharacterCombatBase
    {
        //AI的攻击指令是由AI管理器指派的，非AI自身的行为
        //AI在收到攻击指令，需要判断自身的情况，来决定是否接收这个指令
        //比如受击、处决中不接收
        //或者玩家不希望去接收

        #region 变量相关

        [SerializeField] private bool _attackCommand;//攻击指令是否激活

        /// <summary>
        /// 获取AI攻击指令状态
        /// </summary>
        /// <returns></returns>
        public bool GetAttackCommand() => _attackCommand;

        #endregion

        #region 生命周期函数

        private new void Start()
        {
            _canAttackInput = true;
            _currentEnemy = EnemyManager.MainInstance.GetMainPlayer();
        }

        private void OnEnable()
        {
            GameEventManager.MainInstance.AddEventListening<Transform>("敌人死亡", OnEnemyDead);
        }

        private void OnDisable()
        {
            GameEventManager.MainInstance.RemoveEvent<Transform>("敌人死亡", OnEnemyDead);
        }

        #endregion

        #region 函数相关

        #region 是否允许接受攻击指令

        /// <summary>
        /// 检测当前AI的自身状况是否允许接收攻击指令
        /// </summary>
        /// <returns></returns>
        private bool CheckAIAttackState()
        {
            if (_animator.AnimationAtTag("Hit"))
            {
                return false;
            }
            if (_animator.AnimationAtTag("Parry"))
            {
                return false;
            }
            if (_animator.AnimationAtTag("Attack"))
            {
                return false;
            }
            if (_animator.AnimationAtTag("FinishHit"))
            {
                return false;
            }
            if (_attackCommand)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 攻击指令状态

        /// <summary>
        /// 获取AI的攻击指令状态
        /// </summary>
        /// <param name="command"></param>
        public void SetAttackCommand(bool command)
        {
            //通过管理器 EnemyManager 调用这个函数
            //判断自身情况
            if (!CheckAIAttackState())
            {
                ResetAttackCommand();
                return;
            }

            _attackCommand = command;
        }

        #endregion

        #region 重置攻击指令
        private void ResetAttackCommand()
        {
            _attackCommand = false;
        }

        #endregion

        #region AI普通攻击

        public void AIBaseAttackInput()
        {
            if (!_canAttackInput)
            {
                return;
            }
            ChangeComboData(_baseCombo);
            ExecuteComboAction();
        }

        #endregion

        #region 处决时暂停攻击

        public void StopAllAction()
        {
            if (_attackCommand)
            {
                ResetAttackCommand();
            }
            if (_animator.AnimationAtTag("Attack"))
            {
                _animator.Play("ldie", 0, 0f);
                ResetAttackCommand();
            }
        }

        #endregion

        #region 敌人死亡

        private void OnEnemyDead(Transform enemy)
        {
            if (enemy == this.transform)
            {
                ResetAttackCommand();
                ResetComboInfo();
                _canAttackInput = false;
            }
        }

        #endregion

        #endregion

    }
}


