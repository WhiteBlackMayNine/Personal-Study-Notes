using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class PlayerHealth : CharacterHealthBase
    {

        #region �������ں���

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

        #region �������

        #region ��ɫ�ܻ��߼�

        protected override void CharacterHitAction(float damage, string hitName, string parryName)
        {
            if (animator.AnimationAtTag("Finish"))//�������ڴ������ˣ���ô�������κ��˺���Ϣ
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

        #region ������

        private void PlayerParryInput()
        {
            if (animator.AnimationAtTag("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.35f)
            {
                return;//�ܻ��󣬶������ų���һ��ʱ�����������
            }
            if (animator.AnimationAtTag("FinishHit"))
            {
                return;
            }
            animator.SetBool(AnimationID.ParryID, GameInputManager.MainInstance.Parry);//��ס�ո�Ϊtrue
        }

        #endregion

        #endregion

    }
}
