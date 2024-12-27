using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class PlayerNewHealth : CharacterNewHealthBase
    {

        #region ???????????

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

        #region ???????

        #region ?????????

        //???? ????? ???????и??? EnemyHealthControl
        protected override void CharacterHitAction(float damage, string hitName, string parryName)
        {
            if (animator.AnimationAtTag("Finish"))//??????????????????????????κ???????
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

        #region ??????

        private void PlayerParryInput()
        {
            if (animator.AnimationAtTag("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.35f)
            {
                return;//???????????????????????????????
            }
            if (animator.AnimationAtTag("FinishHit"))
            {
                return;
            }
            animator.SetBool(AnimationID.ParryID, GameInputManager.MainInstance.Parry);//???????true
        }

        #endregion

        #endregion

    }
}
