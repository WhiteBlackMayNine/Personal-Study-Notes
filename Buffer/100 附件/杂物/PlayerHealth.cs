using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class PlayerHealth : CharacterHealthBase
    {

        #region 生命周期函数

        protected override void Awake()
        {
            base.Awake();
            _characterHealthInfo = ScriptableObject.Instantiate(_healthInfo);
        }

        protected override void Update()
        {
            base.Update();
            PlayerParryInput();
        }

        #endregion

        #region 函数相关

        #region 角色受击逻辑

        protected override void CharacterHitAction(float damage, string hitName, string parryName)
        {
            if (animator.AnimationAtTag("Finish"))//如果玩家在处决敌人，那么不接受任何伤害信息
            {
                return;
            }


            if (animator.GetBool(AnimationID.ParryID) && damage < 30f)
            {
                animator.Play(parryName, 0, 0f);
                GamePoolManager.MainInstance.TryGetOnePoolItem("BLOCKSound", transform.position, Quaternion.identity);
            }
            else
            {
                animator.Play(hitName, 0, 0f);
                GamePoolManager.MainInstance.TryGetOnePoolItem("HITSound", transform.position, Quaternion.identity);

            }
            TakeDamage(damage, animator.GetBool(AnimationID.ParryID));
        }

        #endregion

        #region 格挡输入

        private void PlayerParryInput()
        {
            if (animator.AnimationAtTag("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.35f)
            {
                return;//受击后，动画播放超过一定时间后才能允许格挡
            }
            if (animator.AnimationAtTag("FinishHit"))
            {
                return;
            }
            animator.SetBool(AnimationID.ParryID, GameInputManager.MainInstance.Parry);//按住空格为true
        }

        #endregion

        #endregion

    }
}
