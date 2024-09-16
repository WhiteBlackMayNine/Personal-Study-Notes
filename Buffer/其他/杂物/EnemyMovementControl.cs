using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace Movement
{
    public class EnemyMovementControl : CharacterMoveBase
    {
        //动画的控制
        //在移动动画播放时，应该让 AI 看着玩家方向
        //在非移动状态下，我们就把这个动画控制器移动的的值设置为0

        #region 变量相关

        private bool _applyMovement;

        #endregion

        #region 生命周期函数

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

        #region 函数相关

        /// <summary>
        /// 看着目标
        /// </summary>
        private void LookTargetDirection()
        {
            //只希望AI在移动的时候才看着玩家的方向
            //通过动画的 Tag 来控制 
            if (_animator.AnimationAtTag("Motion"))
            {
                transform.Look(EnemyManager.MainInstance.GetMainPlayer().position, 500f);
            }
        }

        //用于外部调用 外部调控能否进行移动
        public void SetApplyMovement(bool apply)
        {
            _applyMovement = apply;
        }

        //画出 玩家与敌人 之间的距离
        private void DrawDirection()
        {
            Debug.DrawRay(transform.position + (transform.up * 0.7f),
                (EnemyManager.MainInstance.GetMainPlayer().position - transform.position), color: Color.yellow);
        }

        /// <summary>
        /// 设置动画移动参数
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

