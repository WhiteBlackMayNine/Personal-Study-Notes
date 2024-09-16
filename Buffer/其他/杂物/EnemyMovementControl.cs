using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace Movement
{
    public class EnemyMovementControl : CharacterMoveBase
    {
        //�����Ŀ���
        //���ƶ���������ʱ��Ӧ���� AI ������ҷ���
        //�ڷ��ƶ�״̬�£����ǾͰ���������������ƶ��ĵ�ֵ����Ϊ0

        #region �������

        private bool _applyMovement;

        #endregion

        #region �������ں���

        protected override void Start()
        {
            base.Start();
            SetApplyMovement(true);
        }


        protected override void Update()
        {
            base.Update();
            LookTargetDirection();
            DrawDirection();
        }

        #endregion

        #region �������

        /// <summary>
        /// ����Ŀ��
        /// </summary>
        private void LookTargetDirection()
        {
            //ֻϣ��AI���ƶ���ʱ��ſ�����ҵķ���
            //ͨ�������� Tag ������ 
            if (_animator.AnimationAtTag("Motion"))
            {
                transform.Look(EnemyManager.MainInstance.GetMainPlayer().position, 500f);
            }
        }

        //�����ⲿ���� �ⲿ�����ܷ�����ƶ�
        public void SetApplyMovement(bool apply)
        {
            _applyMovement = apply;
        }

        //���� �������� ֮��ľ���
        private void DrawDirection()
        {
            Debug.DrawRay(transform.position + (transform.up * 0.7f),
                (EnemyManager.MainInstance.GetMainPlayer().position - transform.position), color: Color.yellow);
        }

        /// <summary>
        /// ���ö����ƶ�����
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
        public void SetAnimatorMovementValue(float horizontal, float vertical)
        {
            if (_applyMovement)
            {
                _animator.SetBool(AnimationID.HasInputID, true);
                _animator.SetFloat(AnimationID.HorizontalID, horizontal, 0.2f, Time.deltaTime);
                _animator.SetFloat(AnimationID.VerticalID, vertical, 0.2f, Time.deltaTime);
            }
            else
            {
                _animator.SetBool(AnimationID.HasInputID, false);
                _animator.SetFloat(AnimationID.HorizontalID, 0f, 0.2f, Time.deltaTime);
                _animator.SetFloat(AnimationID.VerticalID, 0f, 0.2f, Time.deltaTime);
            }
        }

        public void EnableCharacterController(bool enable)
        {
            _characterController.enabled = enable;
        }

        #endregion


    }
}

