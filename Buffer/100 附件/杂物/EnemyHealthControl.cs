using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;
using Movement;

namespace Health
{
    public class EnemyHealthControl : CharacterHealthBase //CharacterNewHealthBase �¶���ϵͳ�̳��µ�
    {
        //�ܵ����������ж��ܲ��ܸ񵲣���ȥ�ж��Ƿ�����
        //Ҳ�������ж� ����ֵ �Ƿ��㹻ȥ���и�
        //���� ���һ���˺�ֵ ����һ����ֵ ����Ϊ���Ʒ�����
        #region �������ں���

        protected override void Awake()
        {
            base.Awake();

            //���� Instantiate ����һ���µ� 
            //�������ô�����ͻᵼ�¶�����˹���һ����������ģ��ʱ ����ֵ������ֵ ����ͨ��
            _characterHealthInfo = ScriptableObject.Instantiate(_healthInfo);

            EnemyManager.MainInstance.AddEnemyUnit(this.gameObject);
        }

        #endregion

        #region �������

        #region ����˺�

        //������������˼���������ֵ�Ƿ�С��0
        //���С��0 �����¼�
        //Ҳ���԰� if ��� �ŵ� ����Ľ�ɫ�ܻ������� else �� TakeDamage ����
        protected override void TakeDamage(float damage, bool hasParry = false)
        {
            base.TakeDamage(damage, hasParry);
            if (_characterHealthInfo.CurrentHP <= 0)
            {
                GameEventManager.MainInstance.CallEvent("��������", transform);
                PlayDeadAnimation();
                EnemyManager.MainInstance.RemovedEnemyUnit(this.gameObject);
                this.gameObject.GetComponent<EnemyMovementControl>().enabled = false;
            }
        }

        #endregion

        #region ��ɫ�ܻ�

        protected override void CharacterHitAction(float damage, string hitName, string parryName)
        {
            if (_characterHealthInfo.StrengthFull && damage < 30f)
            {
                //��
                //������˲��ڹ���״̬��
                if (!animator.AnimationAtTag("Attack"))
                {
                    animator.Play(parryName, 0, 0f);
                    TakeDamage(damage, true);
                    //������Ч
                    GamePoolManager.MainInstance.TryGetOnePoolItem("BLOCKSound", this.transform.position, Quaternion.identity);


                    //��� _characterHealthInfo.StrengthFull = false
                    //������ֵ����� ��ô����Ҫ֪ͨ��ң����ڿ��Խ��д���
                    if (!_characterHealthInfo.StrengthFull)
                    {
                        GameEventManager.MainInstance.CallEvent<bool>("�����", true);
                    }
                }
            }
            else
            {
                //������ֵ����һ��ֵʱ����ô����Ҫ֪ͨ��ң����ڿ��Խ��д���
                if (_characterHealthInfo.CurrentHP < 20f)
                {
                    GameEventManager.MainInstance.CallEvent<bool>("�����", true);
                }
                //�ܻ�
                animator.Play(hitName, 0, 0f);
                //������Ч
                GamePoolManager.MainInstance.TryGetOnePoolItem("HITSound", this.transform.position, Quaternion.identity);
                TakeDamage(damage);
            }
        }

        #endregion

        #region ��������

        protected override void PlayDeadAnimation()
        {
            base.PlayDeadAnimation();
            DevelopmentToos.WTF(this.gameObject.name + "������");
        }

        #endregion

        #endregion

    }
}

