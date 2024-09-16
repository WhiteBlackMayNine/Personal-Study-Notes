using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyCombatControl : CharacterCombatBase
    {
        //AI�Ĺ���ָ������AI������ָ�ɵģ���AI�������Ϊ
        //AI���յ�����ָ���Ҫ�ж������������������Ƿ�������ָ��
        //�����ܻ��������в�����
        //������Ҳ�ϣ��ȥ����

        #region �������

        [SerializeField] private bool _attackCommand;//����ָ���Ƿ񼤻�

        /// <summary>
        /// ��ȡAI����ָ��״̬
        /// </summary>
        /// <returns></returns>
        public bool GetAttackCommand() => _attackCommand;

        #endregion

        #region �������ں���

        private new void Start()
        {
            _canAttackInput = true;
            _currentEnemy = EnemyManager.MainInstance.GetMainPlayer();
        }

        private void OnEnable()
        {
            GameEventManager.MainInstance.AddEventListening<Transform>("��������", OnEnemyDead);
        }

        private void OnDisable()
        {
            GameEventManager.MainInstance.RemoveEvent<Transform>("��������", OnEnemyDead);
        }

        #endregion

        #region �������

        #region �Ƿ�������ܹ���ָ��

        /// <summary>
        /// ��⵱ǰAI������״���Ƿ�������չ���ָ��
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

        #region ����ָ��״̬

        /// <summary>
        /// ��ȡAI�Ĺ���ָ��״̬
        /// </summary>
        /// <param name="command"></param>
        public void SetAttackCommand(bool command)
        {
            //ͨ�������� EnemyManager �����������
            //�ж��������
            if (!CheckAIAttackState())
            {
                ResetAttackCommand();
                return;
            }

            _attackCommand = command;
        }

        #endregion

        #region ���ù���ָ��
        private void ResetAttackCommand()
        {
            _attackCommand = false;
        }

        #endregion

        #region AI��ͨ����

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

        #region ����ʱ��ͣ����

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

        #region ��������

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


