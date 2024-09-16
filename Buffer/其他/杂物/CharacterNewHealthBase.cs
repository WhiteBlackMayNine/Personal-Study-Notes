using GGG.Tool;
using HealthData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterComboData;

namespace Health
{
    #region �¹���ϵͳ�ӿ�

    public interface IDamaged
    {
        void CharacterNormalDamaged(string hitName, string parryName, float damage, Transform attack, DamageType damageType);
    }

    #endregion
    public abstract class CharacterNewHealthBase : MonoBehaviour, IHealth, IDamaged
    {
        //��ɫ����ң���ͬ���еı�����������д�ڻ�������
        //�����˺������񵲡�����ֵ���

        #region �������

        protected Animator animator;

        protected Transform _currentAttack;//��ǰ�Ĺ����ߣ�Ҳ����˭���

        [SerializeField, Header("��ɫ����ֵģ��")] protected CharacterHealthInfo _healthInfo;

        protected CharacterHealthInfo _characterHealthInfo;

        #endregion

        #region �������ں���

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

        #region �������

        #region ���˺���

        /// <summary>
        /// ��ɫ������Ϊ
        /// </summary>
        /// <param name="hitName">���˶���</param>
        /// <param name="parryName">�񵲶���</param>
        protected virtual void CharacterHitAction(float damage, string hitName, string parryName)
        {
            //������ȥ��д�������
            //��ͬ�Ķ��� ������Ϊ���ܲ�ͬ

            //��������������� ִ�ж��� ���߼�
        }

        #endregion

        #region ��⹥����

        /// <summary>
        /// ���õ�ǰ�Ĺ�����
        /// </summary>
        /// <param name="attacker"></param>
        private void SettingAttakcer(Transform attacker)
        {
            //������� �������� Ѱ�ҹ����ߣ�Ҳ������ң��߼�
            if (_currentAttack == null || _currentAttack != attacker)
            {
                _currentAttack = attacker;
            }
        }

        #endregion

        #region �˺����

        protected virtual void TakeDamage(float damage, bool hasParry = false)
        {
            _characterHealthInfo.Damage(damage, hasParry);
        }

        #endregion

        #region �����¼�

        #region ��ͨ��������

        private void OnCharacterHitEventHandler(float damage, string hitName, string parryName, Transform attacker,
    Transform self)
        {
            //������� �������� ����ҹ����� �߼�
            //�������󣬼�ⱻ�������ǲ����Լ��������������˺���

            //���� ��ɵ��˺������˶��������񵲶��������Լ�������λ�ã��ж��ǲ�����Ҵ�ģ����Լ��Լ���λ��
            //�����ж�����ˣ������ֻ����һ����self �жϱ��������ǲ����Լ�

            //����������� self �����Լ�����ô��ִ���¼�
            if (self != this.transform)
            {
                return;
            }

            SettingAttakcer(attacker);//������ڹ����õ���

            CharacterHitAction(damage, hitName, parryName);//���ö���ִ�к�����ִ�ж���

        }

        #endregion

        #endregion

        #region ����

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

        #region �ܻ�/�� ����

        //�ܻ�ʱ���Ź�����
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

        #region ��ǰĿ���������Ƴ���ǰĿ��

        #region ��һ��
        //��Update�е���CheackEnemyIsDie ȥ�������ֵ����û������
        //����������Щ�˷���Դ���Ͼ���ÿһ֡��ȥ���
        //ͨ���ӿ� IHealth ������Ŀ������ֵ

        public bool OnDie() => _characterHealthInfo.IsDie;
        //PlayerCombat �л��� CheackEnemyIsDie();

        #endregion

        #region �ڶ���
        //ͨ���¼����� 
        //PlayerCombat �� EnemyCombatControl ������¼�
        //ͬһ���¼�����Ҫע����������
        //�� EnemyHealthControl �к����¼�

        protected virtual void PlayDeadAnimation()
        {
            //��� ����ִ�й��� ���˾ͻ�����Ȼ�󲥷���������
            //��ô�������Ҫʹ�� if ��� 
            //����ͻ� �������� һ��Ϊ���� һ��Ϊ��������
            //FinisHit Ϊ ���������� �ı�ǩ ���˱�����ʱ�����������
            if (!animator.AnimationAtTag("FinishHit"))
            {
                animator.SetBool(AnimationID.IsDieID, true);
            }

        }

        #endregion

        #endregion

        #endregion

        #region �¹�������ϵͳ�ӿ�ʵ��

        //�ⲿ����ͨ���ӿ��������������
        //�����ڲ��ֻ� ���� ��ɫ�ܻ�����
        public void CharacterNormalDamaged(string hitName, string parryName, float damage, Transform attack, DamageType damageType)
        {

            CharacterHitAction(damage, hitName, parryName);
        }

        #endregion

    }
}


