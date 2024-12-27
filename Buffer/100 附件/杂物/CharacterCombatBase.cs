using GGG.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CharacterCombatBase : MonoBehaviour
    {
        #region ע��

        //��Һ�AI���ǵĹ����¼��������˺�����Ҳ����ͬ��
        //������ϼ�Ҳ�ǲ���
        //��ϼ���ϢҲ�����Ƶ�
        //�Ѳ��Ķ���ȡ����������

        #endregion

        #region �������

        protected Animator _animator;

        #region ����/�ܻ����

        [SerializeField, Header("��ɫ���б�")] protected CharacterComboSO _baseCombo;
        [SerializeField, Header("��ɫ���б�")] protected CharacterComboSO _changeCombo;
        protected CharacterComboSO _currentCombo;//��ǰʹ�õ�����

        protected int _currentComboIndex;//��ǰ���е�����ֵ
        protected float _maxColdTime;//���е������ȴʱ��
        protected bool _canAttackInput;//�Ƿ��������й�������
        protected int _hitIndex;//�ܻ�����ֵ

        protected int _currentComboCount;//��ǰ��������

        #endregion

        #region ���˼��
        //һЩ������Ҫ�������

        [SerializeField, Header("��ǰ����")] protected Transform _currentEnemy;//��ǰ����λ�� ���ڵ��˼����

        #endregion

        #region ���� ��ɱ

        [SerializeField, Header("����")] protected CharacterComboSO _finishCombo;
        [SerializeField, Header("��ɱ")] protected CharacterComboSO _assassinationCombo;

        //����/��ɱ  ��ֹ����ͨ����Ϊ����ǰ���д���ʱ�׳��� ��������ֵ ����
        protected int _FinishComboIndex;//����������������������ɸ�
        protected bool _canFinish;//�Ƿ���Խ��д��� ���統����ֵ/����ֵ����һ��ֵʱ

        #endregion

        #endregion

        #region �������ں���

        protected virtual void Awake()
        {
            _animator = this.GetComponent<Animator>();
        }

        protected void Start()
        {
            _canAttackInput = true;
            _currentCombo = _baseCombo;
        }

        protected virtual void Update()
        {
            LookTargetOnAttack();
            MatchPosition();
            OnEndAttack();
        }


        #endregion

        #region �������

        #region λ��ͬ��

        protected virtual void MatchPosition()
        {
            if (_currentEnemy == null)
            {
                return;
            }

            if (!_animator)
            {
                return;
            }

            //��������ڲ�����������ʱ ͻȻ��ǰ�ƶ���
            //������Ϊ�ڲ��Ź�������ʱ������Ҹ������ˣ������˹����¼���������λ��ƥ��
            //���԰� _animator.MatchTarget ��ע����

            if (_animator.AnimationAtTag("Attack"))
            {
                //�Զ���ʱ���һ�� 0 �� 1 ��һ��ֵ
                var timer = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                //���Ž��ȴ�Լ 35% ����
                if (timer > 0.35f)
                {
                    return;
                }

                //������� ����
                if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 2f)
                {
                    return;
                }

                //����ƥ��״̬  ͬʱ���ڹ�����
                if (!_animator.isMatchingTarget && !_animator.IsInTransition(0))
                {
                    _animator.MatchTarget(_currentEnemy.position + (-transform.forward * _currentCombo.TryGetComboPositionOffset(_currentComboIndex)),
                    Quaternion.identity, AvatarTarget.Body, new MatchTargetWeightMask(Vector3.one, 0f), 0, 0.35f);
                }
            }
        }

        protected void RunningMatch(CharacterComboSO comboSO, int index, float startTime = 0f,
            float endTime = 0.01f)
        {
            //����ƥ��״̬  ͬʱ���ڹ�����
            if (!_animator.isMatchingTarget && !_animator.IsInTransition(0))
            {
                _animator.MatchTarget(_currentEnemy.position +
                    (-transform.forward * comboSO.TryGetComboPositionOffset(index)),
                Quaternion.identity, AvatarTarget.Body, new MatchTargetWeightMask(Vector3.one, 0f), startTime, endTime);
            }
        }

        #endregion

        #region �����¼�

        //����������ڴ��������¼�
        //�����Ҳ����� �����������š��˺���
        protected void ATK()
        {
            TriggerDamager();
            UpdateHitIndex();
            GamePoolManager.MainInstance.TryGetOnePoolItem("ATKSound", this.transform.position, Quaternion.identity);
        }

        #endregion

        #region �˺�����

        protected void TriggerDamager()
        {
            //Ҫȷ����Ŀ��
            if (_currentEnemy == null)
            {
                return;
            }
            //Ҫȷ�����˴������ǿɴ����˺��ľ���ͽǶ�
            //����ǽǶ�
            if (Vector3.Dot(transform.forward, DevelopmentToos.DirectionForTarget(transform, _currentEnemy)) < 0.9f)
            {
                return;
            }
            //����Ǿ���
            if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 1.3f)
            {
                return;
            }
            //ȥ�����¼��У����ô����˺��������
            if (_animator.AnimationAtTag("Attack"))
            {
                GameEventManager.MainInstance.CallEvent("�����˺�", _currentCombo.TryGetComboDamage(_currentComboIndex),
                    _currentCombo.TryGetOneHitAction(_currentComboIndex, _hitIndex),
                    _currentCombo.TryGetOneParryAction(_currentComboIndex, _hitIndex),
                    transform, _currentEnemy);//�˺�ֵ ���˶����� �񵲶����� ������ ��ǰ��������
                                              //���д������Ͳ��ö���
                                              //���ﴫ������˶����ǵ���Ƭ��
            }
            else
            {
                //������Ǵ�������ɱ����������  ��Ҫ��������ĺ���
                //������һ�������ı����������������ڶ����ڼ���ܻ��ж�δ����˺�

                GameEventManager.MainInstance.CallEvent("�����˺�", _finishCombo.TryGetComboDamage(_FinishComboIndex)
                    , _currentEnemy);
            }
        }

        #endregion

        #region ��������Ŀ��

        protected void LookTargetOnAttack()
        {
            //Ҫȷ����һ������Ŀ��
            if (_currentEnemy == null)
            {
                return;
            }
            //ȷ�������Ŀ��֮��ľ���ҪС��һ������
            if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 5f)
            {
                return;
            }
            //��ȡ��ǰ����״̬����ϸ��Ϣ
            if (_animator.AnimationAtTag("Attack") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            {
                transform.Look(_currentEnemy.position, 1000f);
            }
        }

        #endregion

        #region ��������

        protected virtual void CharacterBaseAttackInput()
        {

        }

        #endregion

        #region �������ж�����Ϣ

        protected virtual void UpdataComboInfo()
        {
            _currentComboIndex++;

            if (_currentComboIndex == _currentCombo.TryGetComboMaxCount())
            {
                //���ִ�е������һ������
                _currentComboIndex = 0;
            }
            _maxColdTime = 0f;
            _canAttackInput = true;
        }

        protected void UpdateHitIndex()
        {
            _hitIndex++;
            if (_hitIndex == _currentCombo.TryGetHitAndParryMaxCount(_currentComboIndex))
            {
                _hitIndex = 0;
            }
        }

        #endregion

        #region �������ж�����Ϣ

        protected void ResetComboInfo()
        {
            _currentComboIndex = 0;
            _maxColdTime = 0f;
            _hitIndex = 0;
        }

        protected void OnEndAttack()
        {
            //�ƶ����ͷ��ʼ���й���
            if (_animator.AnimationAtTag("Motion") && _canAttackInput)
            {
                ResetComboInfo();
            }
        }

        #endregion

        #region �ı���ϼ�

        protected void ChangeComboData(CharacterComboSO characterComboSO)
        {
            if (_currentCombo != characterComboSO)
            {
                _currentCombo = characterComboSO;
                ResetComboInfo();
            }
        }

        #endregion

        #region ִ�ж���
        protected virtual void ExecuteComboAction()
        {
            _currentComboCount += (_currentCombo == _baseCombo) ? 1 : 0;
            _hitIndex = 0;
            if (_currentComboIndex == _currentCombo.TryGetComboMaxCount())
            {
                //���ִ�е������һ������
                _currentComboIndex = 0;
            }

            _maxColdTime = _currentCombo.TryGetComboColdTime(_currentComboIndex);
            _animator.CrossFadeInFixedTime(_currentCombo.TryGetOneComboAction(_currentComboIndex), 0.15f, 0, 0);
            GameTimeManager.MainInstance.TryGetTimer(_maxColdTime, UpdataComboInfo);
            _canAttackInput = false;
        }

        #endregion

        #endregion


    }
}
